using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingText : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 1, 0);

    private GameObject exclamationMarkClone; // �N���[���Q��
    [SerializeField]
    private Canvas canvas; // canvas �Q��
    float scale = 0.5f;

    public EventData Event;

    public TextMeshProUGUI text;
    public TextMeshProUGUI nametext;

    string conversationText;
    string nameString;

    public GameObject textBox;

    public int talkEventIndex = 0;
    public int actionEventIndex = 0;

    public int listnum = 0;

    public bool finishtalk = false;

    public bool testFlag = false;

    string characterName;

    [SerializeField]
    private bool InitilizeFlg = true;

    private Coroutine displayTextCoroutine;

    private float textSpeed = 0.05f;

    public float interactionDistance = 2f; // �v���C���[�Ƃ̃C���^���N�V��������

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        textBox.gameObject.SetActive(false);
    }

    void Update()
    {
        if (InitilizeFlg)
        {
            Event.SetEventActionEventFlg("������", true);
            InitilizeFlg = false;
        }

        // ���������ȓ��Ȃ��b���J�n
        if (!testFlag)
        {
            textBox.gameObject.SetActive(true);
            conversationText = TestTalkManager.Instance.GetTalk(talkEventIndex, actionEventIndex);
            nameString = TestTalkManager.Instance.GetTalkName(talkEventIndex, actionEventIndex);

            if (displayTextCoroutine != null) StopCoroutine(displayTextCoroutine);
            displayTextCoroutine = StartCoroutine(DisplayText(conversationText, text));

            nametext.text = nameString;
            testFlag = true;
            Debug.Log("��b�J�n");
        }

        if (Input.GetKeyDown(KeyCode.Space) && testFlag && !finishtalk)
        {
            string nextConversation = TestTalkManager.Instance.GetTalk(talkEventIndex, actionEventIndex + 1);
            string nextName = TestTalkManager.Instance.GetTalkName(talkEventIndex, actionEventIndex + 1);

            actionEventIndex++;
            if (string.IsNullOrEmpty(nextConversation))
            {
                Debug.Log("��b�I��");
                finishtalk = true;
                Event.SetEventActionEventFlg("������", false);
                text.text = "";
            }
            else
            {
                conversationText = nextConversation;
                text.text = conversationText;

                if (displayTextCoroutine != null) StopCoroutine(displayTextCoroutine);
                displayTextCoroutine = StartCoroutine(DisplayText(conversationText, text));
            }

            if (string.IsNullOrEmpty(nextName))
            {
                nametext.text = "";
            }
            else
            {
                nametext.text = nextName;
            }
        }
    }

    private void UpdatefinishFlag()
    {
        if (finishtalk)
        {
            actionEventIndex = 0;
            text.text = "";
            finishtalk = false;
        }
    }

    private IEnumerator DisplayText(string fullText, TextMeshProUGUI textUI)
    {
        textUI.text = "";

        foreach (char c in fullText)
        {
            textUI.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
