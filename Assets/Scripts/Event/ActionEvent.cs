using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionEvent : MonoBehaviour
{
    public GameObject exclamationMark; // !�}�[�N 
    public Transform ActionObject;
    public Vector3 offset = new Vector3(0, 1, 0);

    private GameObject exclamationMarkClone; // �N���[���Q��
    [SerializeField]
    private Canvas canvas; // canvas �Q��
    float scale = 0.5f;

    //�C�x���g
    public EventData Event;

    //public TextMeshProUGUI text;
    public TextMeshProUGUI text;
    public TextMeshProUGUI nametext;

    string conversationText;
    string nameText;

    public GameObject textBox;
    
    //string nextConversation;
    //string nextName;

   // private TalkManager talkManager;

    // ��b�S�̂̃��X�g�̃C���f�b�N�X
    // �ǂ̉�b�̃��X�g��I��ł邩�����̕ϐ���ς��邱�ƂŕύX�ł���
    public int talkEventIndex = 0;
    // ��b���Ƃ̃��X�g�̃C���f�b�N�X
    public int actionEventIndex = 0;

    //public int talknum=0;
    public  int listnum = 0;


    // ��b���I�����Ă���̂��̃t���O
    public bool finishtalk = false; 

    public bool testFlag = false;

    string characterName;

    //��b�C�x���g�������̃Z�b�g����ׂ̃t���O�i���j
    [SerializeField]
    private bool InitilizeFlg = true;

    //private void Awake()
    //{
    //    Event.SetEventActionEventFlg("�����Ƃ̉�b", true);
    //    Debug.Log("Set true");
    //}

    // Start is called before the first frame update
    void Start()
    {
        // Canvas�̎擾
        canvas = FindObjectOfType<Canvas>();

        textBox.gameObject.SetActive(false);

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
        if(InitilizeFlg)
        {
            Event.SetEventActionEventFlg("�����Ƃ̉�b", true);
            InitilizeFlg = false;
        }

        exclamationMarkClone.transform.position = Camera.main.WorldToScreenPoint(ActionObject.transform.position + offset);
      

        // ��b��i�߂�
        if (Input.GetKeyDown(KeyCode.Space) && testFlag && !finishtalk)
        {
            //string nextConversation = TestTalkManager.Instance.GetTalk(taklEventIndex, actionEventIndex + 1);
            string nextConversation = TestTalkManager.Instance.GetTalk(talkEventIndex, actionEventIndex+1);
            string nextName = TestTalkManager.Instance.GetTalkName(talkEventIndex, actionEventIndex+1);

            actionEventIndex++;
            if (string.IsNullOrEmpty(nextConversation))
            {
                Debug.Log("��b�I��");
                finishtalk = true; // ��b�I���t���O�𗧂Ă�
                //�����̕ǔ���폜
                Event.SetEventActionEventFlg("�����Ƃ̉�b", false);
                text.text = "";
            }

            else
            {
                conversationText = nextConversation;
                Debug.Log("��b���e: " + conversationText);
                text.text = conversationText;
            }

            if (string.IsNullOrEmpty(nextName))
            {
                nametext.text = "";
                Debug.Log("���O�N���A");
            }
            else
            {
                //conversationText = nextName;
                nametext.text = nextName;
            }
        }
    }

    // �����蔻��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("actionIndex : " + talkEventIndex);
            textBox.gameObject.SetActive(true);
            exclamationMarkClone.SetActive(true);
            Debug.Log("�I�}�[�N�\��");
            // ��b�f�[�^�̃C���f�b�N�X���擾���ĉ�b���e�\��
            conversationText = TestTalkManager.Instance.GetTalk(talkEventIndex, actionEventIndex);
            nameText = TestTalkManager.Instance.GetTalkName(talkEventIndex, actionEventIndex);
            text.text = conversationText;
            nametext.text = nameText;
            testFlag = true;
            Debug.Log("flag" + testFlag);
            UpdatefinishFlag();
        }
    }

    // �����蔻�肪�O�ꂽ���̏���
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //talkListnum =talknum;
            //talkListindex = listnum;
            if (textBox != null)
            {
                textBox.gameObject.SetActive(false);
                Debug.Log("�e�L�X�g�{�b�N�X��\��");
            }
            
            if (exclamationMarkClone == true)
            {
                exclamationMarkClone.SetActive(false);
            }
            text.text = "";
            nametext.text = "";
            //text.text = conversationText;
            Debug.Log("�I�}�[�N��\��");
            testFlag = false;
            Debug.Log("flag" + testFlag);
            UpdatefinishFlag();
        }
    }


    private void UpdatefinishFlag()
    {
        if (finishtalk)
        {
            actionEventIndex = 0;
            text.text = "";
            finishtalk = false;
            Debug.Log("fisish: " + finishtalk+ "actionIndex: "+actionEventIndex);
        }
    }
}

[System.Serializable]
public class ConversEntry
{
    public TestActionTalkData CharacterName;
}
