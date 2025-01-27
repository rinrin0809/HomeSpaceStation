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
    string nameString;

    public GameObject textBox;

    

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

    private Coroutine displayTextCoroutine;

    //[SerializeField] 
    private float textSpeed = 0.05f;

    //�ǉ�����A�C�e��
    [SerializeField] 
    private GameObject AddItem = null;

    // �ꕶ���Â\�����鑁��
    void Start()
    {
        // Canvas�̎擾
        canvas = FindObjectOfType<Canvas>();

        textBox.gameObject.SetActive(false);

        // Canvas�̑��݊m�F
        //if (canvas != null)
        //{
        //    exclamationMarkClone = Instantiate(exclamationMark,canvas.transform);
        //    exclamationMarkClone.transform.localScale *= scale;
        //    exclamationMarkClone.SetActive(false);
        //}
        //else
        //{
        //    Debug.Log("canvas���Ȃ�");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(InitilizeFlg)
        {
            Event.SetEventActionEventFlg("������", true);
            InitilizeFlg = false;
        }

        //exclamationMarkClone.transform.position = Camera.main.WorldToScreenPoint(ActionObject.transform.position + offset);
      

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
                Event.SetEventActionEventFlg("������", false);

                if (AddItem != null)
                {
                    ItemDisplay itemHolder = AddItem.GetComponent<ItemDisplay>();
                    if (itemHolder != null && itemHolder.itemData != null && Player.Instance != null)
                    {
                        Player.Instance.GetItemList.Add(AddItem);
                        if (itemHolder.itemData == null)
                        {
                            Debug.Log("ItemData Null");
                        }

                        else
                        {
                            Player.Instance.GetItemDisplay.PickUpItem(itemHolder.itemData);
                        }
                    }
                }
                text.text = "";
            }

            else
            {
                conversationText = nextConversation;
                Debug.Log("��b���e: " + conversationText);
                text.text = conversationText;

                // �ꕶ�����\��
                if (displayTextCoroutine != null) StopCoroutine(displayTextCoroutine);
                displayTextCoroutine = StartCoroutine(DisplayText(conversationText, text));

                //if (!string.IsNullOrEmpty(nameText))
                //{
                //    if (displayNameCoroutine != null) StopCoroutine(displayNameCoroutine);
                //    displayNameCoroutine = StartCoroutine(DisplayTextCoroutine(nameText, nametext));
                //}
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
            //exclamationMarkClone.SetActive(true);
            Debug.Log("�I�}�[�N�\��");
            // ��b�f�[�^�̃C���f�b�N�X���擾���ĉ�b���e�\��
            conversationText = TestTalkManager.Instance.GetTalk(talkEventIndex, actionEventIndex);
            nameString = TestTalkManager.Instance.GetTalkName(talkEventIndex, actionEventIndex);

            // �ꕶ�����\��
            if (displayTextCoroutine != null) StopCoroutine(displayTextCoroutine);
            displayTextCoroutine = StartCoroutine(DisplayText(conversationText, text));
            Debug.Log(nameString);
            nametext.text = nameString;

            //if (!string.IsNullOrEmpty(nameText))
            //{

            //}

            //text.text = conversationText;
            //nametext.text = nameText;
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
            
            //if (exclamationMarkClone == true)
            //{
            //    exclamationMarkClone.SetActive(false);
            //}
           
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

    private IEnumerator DisplayText(string fullText,TextMeshProUGUI textUI)
    {
        Debug.Log("�Ăяo��");
        Debug.Log($"Current textSpeed: {textSpeed}");
        textUI.text = "";
        
        foreach(char c in fullText)
        {
            textUI.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}

[System.Serializable]
public class ConversEntry
{
    public TestActionTalkData CharacterName;
}
