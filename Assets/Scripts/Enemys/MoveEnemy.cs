using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public Grid grid; // �O���b�h�ւ̎Q��
    public float speed = 5f; // �G�̈ړ����x
    private int targetIndex = 0; // ���݂̃^�[�Q�b�g�m�[�h�̃C���f�b�N�X
    private List<Node> path; // ���݂̒ǐՌo�H

    private void Start()
    {
        // �����p�X�̐ݒ�
        if (grid.FinalPath != null && grid.FinalPath.Count > 0)
        {
            path = grid.FinalPath;
            targetIndex = 0;
        }
    }

    private void Update()
    {
        // �p�X�����݂��邩���m�F
        if (path != null && path.Count > 0)
        {
            // ���݂̃^�[�Q�b�g�m�[�h�Ɍ������Ĉړ�
            Vector2 targetPosition = path[targetIndex].Position;
            Vector2 currentPosition = transform.position;

            // �^�[�Q�b�g�m�[�h�ւ̈ړ�
            Vector2 direction = (targetPosition - currentPosition).normalized;
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

            // �m�[�h�ɓ��B�����玟�̃m�[�h�ֈړ�
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetIndex++; // ���̃m�[�h��

                // �Ō�̃m�[�h�ɓ��B������ǐՂ��I�����邩���[�v���J��Ԃ�
                if (targetIndex >= path.Count)
                {
                    targetIndex = 0; // ���[�v���čĂэŏ��̃m�[�h��
                }
            }
        }
        else
        {
            Debug.Log("No path to follow.");
        }
    }

    // �O������p�X���X�V���郁�\�b�h
    public void UpdatePath(List<Node> newPath)
    {
        path = newPath;
        targetIndex = 0; // �p�X�̍ŏ�����ăX�^�[�g
    }
}
