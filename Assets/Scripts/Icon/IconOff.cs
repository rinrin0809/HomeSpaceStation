using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconOff : MonoBehaviour
{
    //�����I�u�W�F�N�g
    [SerializeField] private GameObject GameObj;
    //�����I�u�W�F�N�g�̐F
    [SerializeField] private Color ObjColor;
    //�����I�u�W�F�N�g�̃C���[�W
    Image ObjImage;
    //�����x
    private float Alpha = 1.0f;
    //���X�ɏ�������
    private float AlphaTime = 2.0f;
    void Start()
    {
        //������
        ObjColor = new Color(0.0f,0.0f,0.0f,1.0f);
        //�����I�u�W�F�N�g�̃C���[�W�擾
        ObjImage = GameObj.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //�����x���X�V
        ObjImage.color = ObjColor;

        //�v���C���[�������Ă��鎞�܂��̓��j���[���J���Ă��鎞
        if (Player.Instance.GetisMoving())
        {
            if (ObjColor.a < 0.0f)
            {
                ObjColor.a = 0.0f;
            }

            else
            {
                ObjColor.a -= AlphaTime * Time.deltaTime;
            }
        }

        else
        {
            if(ObjColor.a > 1.0f)
            {
                ObjColor.a = 1.0f;
            }

            else
            {
                ObjColor.a += AlphaTime * Time.deltaTime;
            }
        }

        if (MenuManager.Instance.GetOpenMenuFlg())
        {
            GameObj.SetActive(false);
        }

        else
        {
            GameObj.SetActive(true);
        }
    }
}
