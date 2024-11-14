using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TalkManager;

public class ActionEvent : MonoBehaviour
{
    public GameObject exclamationMark; // !�}�[�N 
    public Transform ActionObject;
    public Vector3 offset = new Vector3(0, 1, 0);

    private GameObject exclamationMarkClone; // �N���[���Q��
    [SerializeField]
    private Canvas canvas; // canvas �Q��
    float scale = 0.5f;

    //public TextMeshProUGUI text;
    public TextMeshProUGUI text;

    string conversationText;

    private TalkManager talkManager;

    // ��b�S�̂̃��X�g�̃C���f�b�N�X
    // �ǂ̉�b�̃��X�g��I��ł邩�����̕ϐ���ς��邱�ƂŕύX�ł���
    [Header("��b���X�g��I�ԃC���f�b�N�X")]
    public int taklEventIndex = 0;
    // ��b���Ƃ̃��X�g�̃C���f�b�N�X
    [Header("��b���X�g�̒��̃C���f�b�N�X")]
    public int actionEventIndex = 0;

    public int talknum=0;
    public int listnum = 0;
    public bool testFlag = false;

    string characterName;

    // Start is called before the first frame update
    void Start()
    {
        // Canvas�̎擾
        canvas = FindObjectOfType<Canvas>();

        // Canvas�̑��݊m�F
        if (canvas != null)
        {
            exclamationMarkClone = Instantiate(exclamationMark,canvas.transform);
            exclamationMarkClone.transform.localScale *= scale;
            exclamationMarkClone.SetActive(false);
        }
        else
        {
            Debug.Log("canvas���Ȃ�");
        }

    }

    // Update is called once per frame
    void Update()
    {
        exclamationMarkClone.transform.position = Camera.main.WorldToScreenPoint(ActionObject.transform.position + offset);
        if (Input.GetKeyDown(KeyCode.Space)&&testFlag==true)
        {
            actionEventIndex += 1;
            conversationText = TestTalkManager.Instance.GetTalk(taklEventIndex, actionEventIndex);
            Debug.Log("index:" + taklEventIndex + actionEventIndex);
            Debug.Log(conversationText);
            text.text = conversationText;
            Debug.Log("space������");
        }
    }

    // �����蔻��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            

            exclamationMarkClone.SetActive(true);
            Debug.Log("�I�}�[�N�\��");
            // ��b�f�[�^�̃C���f�b�N�X���擾���ĉ�b���e�\��
            conversationText = TestTalkManager.Instance.GetTalk(taklEventIndex, actionEventIndex);
            text.text = conversationText;
            testFlag = true;
            Debug.Log("flag" + testFlag);

        }
    }

    // �����蔻�肪�O�ꂽ���̏���
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //talkListnum =talknum;
            //talkListindex = listnum;
            if (exclamationMarkClone == true)
            {
                exclamationMarkClone.SetActive(false);
            }

           text.text = "";
            //text.text = conversationText;
            Debug.Log("�I�}�[�N��\��");
            testFlag = false;
            Debug.Log("flag" + testFlag);
        }
    }
}

[System.Serializable]
public class ConversEntry
{
    public TestActionTalkData CharacterName;
}
