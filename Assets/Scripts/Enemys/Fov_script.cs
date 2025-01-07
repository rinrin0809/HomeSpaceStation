using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fov_script : MonoBehaviour
{
    public float fovAngle = 90f;
    public Transform fovPoint;
    public float range = 8f;

    // �^�[�Q�b�g��Transform�͎����Ŏ擾
    private Transform target;

    [SerializeField]
    private GameOverFade gameover;

    private string targetname = "Player";
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
        if (target == null) return;

        // �^�[�Q�b�g�ւ̕����x�N�g�����v�Z
        Vector2 dir = target.position - fovPoint.position;
        float angle = Vector2.Angle(fovPoint.up, dir);

        // ���C�L���X�g�����s
        RaycastHit2D r = Physics2D.Raycast(fovPoint.position, dir.normalized, range);

        // �p�x�����E�͈͓����ǂ���
        if (angle < fovAngle / 2)
        {
            if (r.collider != null && r.collider.CompareTag(targetname))
            {
                // �v���C���[�𔭌��I
                gameover.gameObject.SetActive(true);
                //Debug.Log("�G�̎��E�ɓ���܂���");
                Debug.DrawRay(fovPoint.position, dir.normalized * range, Color.red);
            }
            else
            {
                //Debug.Log("�G�̎��E����O��܂���");
            }
        }
        else
        {
            //Debug.Log("�G�̎��E�O�ł�");
        }
    }


}
