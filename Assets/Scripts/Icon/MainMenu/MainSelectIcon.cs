using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSelectIcon : MonoBehaviour
{
    //���ړ����̃t���O
    [SerializeField] private bool SideFlg;

    //UI�̈ʒu
    RectTransform RectTransformIns;
    //�A�C�R���i���j�̈ړ�
    IconMove IconMoveIns;
    //���j���[�̊Ǘ��N���X
    private MenuManager MenuManagerIns;

    //�{�^���̔z��
    public Button[] Buttons;

    void Start()
    {
        //UI�̈ʒu�擾
        RectTransformIns = gameObject.GetComponent<RectTransform>();
        //�A�C�R���̈ړ��擾
        IconMoveIns = gameObject.GetComponent<IconMove>();
        // MenuManager �̎擾
        if (MenuManagerIns == null)
        {
            MenuManagerIns = Object.FindAnyObjectByType<MenuManager>(); // �����擾
        }
    }

    // Update is called once per frame
    void Update()
    {
        //���݂̃��j���[�̎�ގ擾
        MenuType Type = MenuManagerIns.GetActiveMenu();
        
        //���C�����j���[�̎�
        if (Type == MenuType.MainMenu)
        {
            //�ړ�����
            RectTransformIns.position = IconMoveIns.Move(SideFlg, RectTransformIns.position);
            //�����ꂽ���̏���
            OnClick();
        }
    }

    //�����ꂽ���̏���
    void OnClick()
    {
        // �G���^�[�L�[�������ꂽ���m�F (KeyCode.Return �̓G���^�[�L�[)
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Enter key pressed");
            // ���ړ��̏ꍇ
            if (SideFlg)
            {
                int SideNum = IconMoveIns.GetSideNum();
                // �{�^�����ݒ肳��Ă���ꍇ�A�{�^����onClick�C�x���g���Ăяo��
                if (Buttons[SideNum] != null)
                {
                    Buttons[SideNum].onClick.Invoke(); // �{�^���̃N���b�N�C�x���g���Ăяo��
                }
            }

            else
            {
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
