using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_hit : MonoBehaviour
{
    public GameObject gimmick;
    // �v���C���[���R���C�_�ɓ������Ƃ��ɃA�N�V���������s
    void OnTriggerEnter2D(Collider2D other)
    {
        // �v���C���[�̃^�O���m�F�i�v���C���[�ɂ� "Player" �^�O��t���Ă���O��j
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space))
        {
            // �v���C���[���R���C�_�ɓ������Ƃ��̃A�N�V����
            Debug.Log("�v���C���[���R���C�_�ɓ���܂����I");
            // �����ɃA�N�V������ǉ�����
            // ��: �����J����A�_���[�W��^����Ȃ�
            gimmick.gameObject.SetActive(true);
        }
    }

    // �I�v�V�����ŁA�v���C���[���R���C�_����o���Ƃ��ɃA�N�V���������s�������ꍇ
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("�v���C���[���R���C�_����o�܂����I");
            // �����ɏI�����̃A�N�V������ǉ�����
            gimmick.gameObject.SetActive(false);
        }
    }
}
