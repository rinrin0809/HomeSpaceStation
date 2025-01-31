using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_hit_number : MonoBehaviour
{
    public InputNumber inputumber;
    public GameObject gimmick;
    public GameObject kabe;
    // �v���C���[���R���C�_�ɓ��������ǂ����𔻒肷��t���O
    private bool isPlayerInRange = false;

    // �v���C���[���R���C�_�ɓ������Ƃ��̏���
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �v���C���[���R���C�_�ɓ������Ƃ�
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("�v���C���[���R���C�_�ɓ������I");
        }
    }

    // �v���C���[���R���C�_����o���Ƃ��̏���
    private void OnTriggerExit2D(Collider2D other)
    {
        // �v���C���[���R���C�_����o���Ƃ�
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("�v���C���[���R���C�_����o���I");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("�X�y�[�X�L�[�������ꂽ�I�M�~�b�N���A�N�e�B�u�ɂ��܂��B");

            // �M�~�b�N���A�N�e�B�u�ɂ���
            if (gimmick != null)
            {
                gimmick.SetActive(true);
            }
        }
        if (/*inputumber != null &&*/ inputumber.Ans || Input.GetKeyDown(KeyCode.Backspace))
        {
            Time.timeScale = 1;
            gimmick.SetActive(false); // �M�~�b�N���\���ɂ���
            kabe.SetActive(false);
        }
    }
}
