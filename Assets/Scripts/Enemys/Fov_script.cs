using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fov_script : MonoBehaviour
{
    public float fovAngle = 90f; // �ʏ펋��p
    public float reverseFovAngle = 90f; // ���Α��̎���p
    public Transform fovPoint;
    public float range = 8f; // ���m�͈�
    public float lookDelay = 2f; // ���Α��������܂ł̒x������

    private Transform target; // �v���C���[
    private string targetName = "Player";
    private bool isInReverseFov = false; // ���Α��̎�����ɂ��邩
    private Coroutine lookCoroutine = null; // �x�������R���[�`��

    // �V���ɒǉ�: �G���v���C���[�𔭌������ꍇ�ɓ������~����t���O
    public bool isPlayerInSight = false;

    [SerializeField]
    private GameOverFade gameover;

    void Start()
    {
        // �v���C���[�������ŒT��
        GameObject player = GameObject.FindGameObjectWithTag(targetName);
        if (player != null)
        {
            target = player.transform;
        }
    }

    void Update()
    {
        if (target == null) return;

        Vector2 dir = target.position - fovPoint.position;
        float distance = dir.magnitude;
        float angle = Vector2.Angle(-fovPoint.up, dir); // ����̔��Α����

        // �v���C���[�����Α��̎�����ɂ��邩�`�F�b�N
        if (distance <= range && angle < reverseFovAngle / 2)
        {
            if (!isInReverseFov)
            {
                isInReverseFov = true;
                if (lookCoroutine != null) StopCoroutine(lookCoroutine);
                lookCoroutine = StartCoroutine(LookAtAfterDelay(dir));
            }
        }
        else
        {
            isInReverseFov = false;
            if (lookCoroutine != null)
            {
                StopCoroutine(lookCoroutine);
                lookCoroutine = null;
            }
        }

        // �v���C���[�����E�ɓ����Ă��邩�ǂ����̔���
        if (distance <= range && angle <= fovAngle / 2)
        {
            isPlayerInSight = true;
            if (gameover != null) gameover.gameObject.SetActive(true);

        }
        else
        {
            isPlayerInSight = false;
        }
    }

    private IEnumerator LookAtAfterDelay(Vector2 dir)
    {
        yield return new WaitForSeconds(lookDelay);

        // �U���������
        Vector3 direction = new Vector3(dir.x, dir.y, 0).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        fovPoint.rotation = targetRotation;

        Debug.Log("���Α��̃v���C���[�����������܂���");
    }

    void OnDrawGizmos()
    {
        if (fovPoint == null) return;

        // �ʏ펋��̕`��
        Gizmos.color = Color.yellow;
        DrawFovGizmo(fovPoint.up, fovAngle);

        // ���Α�����̕`��
        Gizmos.color = Color.red;
        DrawFovGizmo(-fovPoint.up, reverseFovAngle);

        // �����̕`��: �v���C���[�����E���ɂ���ꍇ
        if (target != null && isPlayerInSight)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(fovPoint.position, target.position); // �v���C���[�Ǝ��_�����Ԑ�
        }

        // �w�㎋����̃v���C���[�����o�����ꍇ�A�M�Y���Ń��C�L���X�g��`��
        if (target != null && isInReverseFov)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(fovPoint.position, target.position); // �w�㎋����̃v���C���[�Ƃ̐ڑ���
        }
    }

    private void DrawFovGizmo(Vector3 direction, float angle)
    {
        // ����p�̍��E�̒[���v�Z
        Vector3 leftBoundary = Quaternion.Euler(0, 0, -angle / 2) * direction * range;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, angle / 2) * direction * range;

        // ����͈͂�`�悷�邽�߂̃��C����`��
        Gizmos.DrawLine(fovPoint.position, fovPoint.position + leftBoundary);
        Gizmos.DrawLine(fovPoint.position, fovPoint.position + rightBoundary);

        // ����͈͓����������߁A�M�Y���𔖂��`��i�Z�O�����g�ŕ`��j
        Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.2f);
        int segmentCount = 30; // �~�ʂ��\������Z�O�����g��
        Vector3 prevPoint = fovPoint.position + leftBoundary;

        for (int i = 1; i <= segmentCount; i++)
        {
            // �e�Z�O�����g�p�x���v�Z
            float segmentAngle = -angle / 2 + (angle / segmentCount) * i;
            Vector3 nextPoint = fovPoint.position + (Quaternion.Euler(0, 0, segmentAngle) * direction) * range;

            // �����ŃZ�O�����g���q����
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
    }

}
