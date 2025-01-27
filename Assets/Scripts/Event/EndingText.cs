using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingText : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 1, 0);

    [SerializeField]
    private Canvas canvas; // canvas 参照
    float scale = 0.5f;

    public EventData Event;

    public TextMeshProUGUI text;
    public TextMeshProUGUI nametext;

    string conversationText;
    string nameString;

    public GameObject textBox;

    public Image textBoxImage;
    public Image nameBoxImage;
    public float textAlphaSpeed = 1.3f;


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

    public float interactionDistance = 2f; // プレイヤーとのインタラクション距離

    public float AlphaTime = 3.0f;

    public bool FinishSpaceFlg = false;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        textBox.gameObject.SetActive(false);
    }

    void Update()
    {
        if (InitilizeFlg)
        {
            Event.SetEventActionEventFlg("潜入後", true);
            InitilizeFlg = false;
        }

        // 距離が一定以内なら会話を開始
        if (!testFlag)
        {
            textBox.gameObject.SetActive(true);
            conversationText = TestTalkManager.Instance.GetTalk(talkEventIndex, actionEventIndex);
            nameString = TestTalkManager.Instance.GetTalkName(talkEventIndex, actionEventIndex);

            if (displayTextCoroutine != null) StopCoroutine(displayTextCoroutine);
            displayTextCoroutine = StartCoroutine(DisplayText(conversationText, text));

            nametext.text = nameString;
            testFlag = true;
            Debug.Log("会話開始");
        }

        if (Input.GetKeyDown(KeyCode.Space) && testFlag && !finishtalk)
        {
            string nextConversation = TestTalkManager.Instance.GetTalk(talkEventIndex, actionEventIndex + 1);
            string nextName = TestTalkManager.Instance.GetTalkName(talkEventIndex, actionEventIndex + 1);

            actionEventIndex++;
            if (string.IsNullOrEmpty(nextConversation))
            {
                Debug.Log("会話終了");
                finishtalk = true;
                Event.SetEventActionEventFlg("潜入後", false);
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

        if(finishtalk && Input.GetKeyDown(KeyCode.Space))
        {
            FinishSpaceFlg = true;
        }

        if(FinishSpaceFlg)
        {
            AlphaTime -= 2.0f * Time.deltaTime;
            if (AlphaTime <= 0.0f)
            {
                Color startColor = textBoxImage.color;
                startColor.a -= textAlphaSpeed * Time.deltaTime;
                textBoxImage.color = startColor;

                Color TextColor = text.color;
                TextColor.a -= textAlphaSpeed * Time.deltaTime;
                text.color = TextColor;

                Color nameColor = nametext.color;
                nameColor.a -= textAlphaSpeed * Time.deltaTime;
                nametext.color = nameColor;
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
