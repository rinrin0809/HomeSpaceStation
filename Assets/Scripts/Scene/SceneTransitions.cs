using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransitions : MonoBehaviour
{
    public string sceneToLoad;        //�V�[�����őJ�ڐ攻��
    public Vector2 playerPosition;    //�V�[���J�ڌ�̃v���C���[�ʒu
    public VectorValue playerStorage; //�v���C���[�̈ʒu��ۑ�

    private Player player;

    [SerializeField]
    public string ItemName = "";
    // �����K�v���ǂ����𔻕ʂ���t���O
    public bool requiresKey = false;

    //�V�[���ړ�
    private VectorValue startingPosition;

    void Start()
    {
        player = FindObjectOfType<Player>();
        playerStorage.initialValue = new Vector3(0.0f, -4.0f, 0.0f);
    }

    //�V�[���J��
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            // �����K�v���ǂ����`�F�b�N
            if (!requiresKey || (requiresKey && HasRequiredItem()))
            {
                // �v���C���[�̈ʒu��ۑ����A�V�[����؂�ւ�
                playerStorage.initialValue = playerPosition;
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.Log("���������Ă��Ȃ����߁A�V�[���J�ڂł��܂���B");
            }
        }
    }

    // �K�v�ȃA�C�e���������Ă��邩�m�F
    private bool HasRequiredItem()
    {
        if (player != null)
        {
            for (int i = 0; i < player.GetInventory().GetSize(); i++)
            {
                foreach (var item in player.GetInventory().GetCurrentInventoryState())
                {
                    if (item.Value.item.Name == ItemName)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}