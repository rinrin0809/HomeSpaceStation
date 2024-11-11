using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleIconMove : MonoBehaviour
{
    //���ړ����̃t���O
    [SerializeField] private bool SideFlg;
    //UI�̈ʒu
    RectTransform RectTransformIns;
    //�A�C�R���i���j�̈ړ�
    IconMove IconMoveIns;
    //�A�C�R���̑傫��
    IconScaleUpDown IconScaleIns;
    //�^�C�g���V�[���̊Ǘ��N���X
    private TitleSceneManager SceneManagerIns;
    //�{�^���̔z��
    [SerializeField] private Button[] Buttons;

    void Start()
    {
        //UI�̈ʒu�擾
        RectTransformIns = gameObject.GetComponent<RectTransform>();
        //�A�C�R���̈ړ��擾
        IconMoveIns = gameObject.GetComponent<IconMove>();
        //�A�C�R���̑傫���擾
        IconScaleIns = gameObject.GetComponent<IconScaleUpDown>();
        // SceneManager �̎擾
        if (SceneManagerIns == null)
        {
            SceneManagerIns = Object.FindAnyObjectByType<TitleSceneManager>(); // �����擾
        }
    }

    // Update is called once per frame
    void Update()
    {
        //���݂̃V�[���̎�ގ擾
        SceneType Type = SceneManagerIns.GetSceneActiveMenu();
        //�^�C�g��
        if(Type == SceneType.TitleMenu)
        {
            //�ړ�����
            RectTransformIns.position = IconMoveIns.Move(SideFlg, RectTransformIns.position);
            //�����ꂽ���̏���
            OnClick();
            if (SideFlg)
            {
                int SideNum = IconMoveIns.GetSideNum();
                IconScaleIns.ScaleRaise(Buttons, SideNum);
            }

            else
            {
                //Debug.Log("Title Enter key pressed Load");

                int LengthNum = IconMoveIns.GetLengthNum();
                IconScaleIns.ScaleRaise(Buttons, LengthNum);
            }
            //Debug.Log("TitleIconMove");
        }
    }

    //�����ꂽ���̏���
    void OnClick()
    {
        // �G���^�[�L�[�������ꂽ���m�F (KeyCode.Return �̓G���^�[�L�[)
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // ���ړ��̏ꍇ
            if (SideFlg)
            {
                int SideNum = IconMoveIns.GetSideNum();
                // �{�^�����ݒ肳��Ă���ꍇ�A�{�^����onClick�C�x���g���Ăяo��
                if (Buttons[SideNum] != null)
                {
                    Buttons[SideNum].onClick.Invoke(); // �{�^���̃N���b�N�C�x���g���Ăяo��
                    IconScaleIns.ScaleRaise(Buttons, SideNum);
                }
            }

            else
            {
                //Debug.Log("Title Enter key pressed Load");

                int LengthNum = IconMoveIns.GetLengthNum();
                // �{�^�����ݒ肳��Ă���ꍇ�A�{�^����onClick�C�x���g���Ăяo��
                if (Buttons[LengthNum] != null)
                {
                    Buttons[LengthNum].onClick.Invoke(); // �{�^���̃N���b�N�C�x���g���Ăяo��
                }
            }
        }
    }
}
