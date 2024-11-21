using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    public EventEnemyMove eventEnemyMove;  // �ǐՂ���G��AI�X�N���v�g�������N

    // �v���C���[���g���K�[�]�[���ɓ��������ɌĂ΂��
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �v���C���[���g���K�[�]�[���ɓ�������G�ɒǐՂ��J�n������
            eventEnemyMove.StartChasing();  // �ǐՊJ�n���\�b�h���Ăяo��
        }
    }
}
