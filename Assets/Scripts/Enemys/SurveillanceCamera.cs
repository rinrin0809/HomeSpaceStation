using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveillanceCamera : MonoBehaviour
{
    //�^�[�Q�b�g�ݒ�
    private Transform target;
    private string targetname = "Player";

    //����
    private int angle = 180;
    private int anglecorrection = 360;
    public float fovAngle = 90f;
    public Transform fovPoint;
    public float range = 8f;
   
    //��]
    public float rotationSpeed = 30f; // ��]���x�i�x/�b�j
    public float minRotation = -45f;  // ��]�̍ŏ��p�x
    public float maxRotation = 45f;   // ��]�̍ő�p�x
    private bool rotatingClockwise = true; // ���v���̉�]�����ǂ���

    //�M�Y���p
    private int half = 2;            
    void Start()
    {
        // �v���C���[�������ŒT��
        GameObject player = GameObject.FindGameObjectWithTag(targetname);
        if (player != null)
        {
            target = player.transform;
        }
    }

    void Update()
    {
        // �I�u�W�F�N�g����]������
        RotateObject();

        if (target == null) return;

        // �^�[�Q�b�g�ւ̕����x�N�g�����v�Z
        Vector2 dir = target.position - fovPoint.position;
        float angle = Vector2.Angle(fovPoint.up, dir);

        // ���C�L���X�g�����s
        RaycastHit2D r = Physics2D.Raycast(fovPoint.position, dir.normalized, range);

        // �p�x�����E�͈͓����ǂ���
        if (angle < fovAngle / 2)
        {
            if (r.collider != null && r.collider.CompareTag("Player"))
            {
                // �v���C���[�𔭌��I
               // Debug.Log("�G�̎��E�ɓ���܂���");
                //Debug.DrawRay(fovPoint.position, dir.normalized * range, Color.red);
            }
            else
            {
                //Debug.Log("�G�̎��E����O��܂���");
            }
        }
        else
        {
           // Debug.Log("�G�̎��E�O�ł�");
        }
    }

    private void RotateObject()
    {
        // ���݂̃��[�J��Z���̉�]�p�x���擾
        float currentRotation = transform.localEulerAngles.z;

       
        // �p�x�� -180 �` 180 �ɕ␳
        if (currentRotation > angle)
        {
               currentRotation -= anglecorrection;
        }

        // ��]�����̐؂�ւ�
        if (rotatingClockwise && currentRotation >= maxRotation)
        {
            rotatingClockwise = false;
        }
        else if (!rotatingClockwise && currentRotation <= minRotation)
        {
            rotatingClockwise = true;
        }

        // ��]
        float rotationDelta = rotationSpeed * Time.deltaTime * (rotatingClockwise ? 1 : -1);
        transform.Rotate(0, 0, rotationDelta);
    }

    void OnDrawGizmos()
    {
        if (fovPoint == null) return;

        // �M�Y���̐F�ݒ�
        Gizmos.color = Color.green;

        // ���E�̒��S��
        Gizmos.DrawRay(fovPoint.position, fovPoint.up * range);

        // ��`��`��
        DrawFOVGizmo();
    }

    private void DrawFOVGizmo()
    {
        Vector3 leftBoundary = Quaternion.Euler(0, 0, -fovAngle / half) * fovPoint.up * range;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, fovAngle / half) * fovPoint.up * range;

        // ����͈̔͂��`�ŕ`��
        float transparency = 0.3f;//�����x
        Gizmos.color = new Color(0, 1, 0, transparency); // �������̗�
        Gizmos.DrawLine(fovPoint.position, fovPoint.position + leftBoundary);
        Gizmos.DrawLine(fovPoint.position, fovPoint.position + rightBoundary);

        // ��`�̕⏕�����ׂ����`��
        int segments = 20; // �Z�O�����g���i�����قǊ��炩�j
        float angleStep = fovAngle / segments;

        for (int i = 0; i <= segments; i++)
        {
            float angle = -fovAngle / half + angleStep * i;
            Vector3 segmentDir = Quaternion.Euler(0, 0, angle) * fovPoint.up * range;
            Gizmos.DrawLine(fovPoint.position, fovPoint.position + segmentDir);
        }
    }
}
