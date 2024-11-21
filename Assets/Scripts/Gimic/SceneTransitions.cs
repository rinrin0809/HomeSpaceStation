using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransitions : MonoBehaviour
{
    public string sceneToLoad;        //�V�[�����őJ�ڐ攻��
    public Vector2 playerPosition;    //�V�[���J�ڌ�̃v���C���[�ʒu
    public VectorValue playerStorage; //�v���C���[�̈ʒu��ۑ�

    //�V�[���J��
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            //���O����������starPosition�Ɉړ�����t���O��true
            if (!LoadManager.Instance)
            {
                LoadManager.Instance.NewGamePushFlg = true;
            }
            Debug.Log(LoadManager.Instance.NewGamePushFlg);
            //// �v���C���[�̌�����ۑ�
            //Player player = collision.GetComponent<Player>();
            //if (player != null)
            //{
            //    player.SavePlayerRotation(); // �v���C���[�̌�����ۑ�
            //}

            // �v���C���[�̈ʒu��ۑ����A�V�[����؂�ւ�
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}