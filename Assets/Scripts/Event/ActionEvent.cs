using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionEvent : MonoBehaviour
{
    public GameObject exclamationMark; // !マーク 
    public Transform ActionObject;
    public Vector3 offset = new Vector3(0, 1, 0);

    private GameObject exclamationMarkClone; // クローン参照
    [SerializeField]
    private Canvas canvas; // canvas 参照
    float scale = 0.5f;

    //イベント
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

    // 会話全体のリストのインデックス
    // どの会話のリストを選んでるかをこの変数を変えることで変更できる
    public int talkEventIndex = 0;
    // 会話ごとのリストのインデックス
    public int actionEventIndex = 0;

    //public int talknum=0;
    public  int listnum = 0;


    // 会話が終了しているのかのフラグ
    public bool finishtalk = false; 

    public bool testFlag = false;

    string characterName;

    //会話イベント発生時のセットする為のフラグ（仮）
    [SerializeField]
    private bool InitilizeFlg = true;

    //private void Awake()
    //{
    //    Event.SetEventActionEventFlg("女将との会話", true);
    //    Debug.Log("Set true");
    //}

    // Start is called before the first frame update
    void Start()
    {
        // Canvasの取得
        canvas = FindObjectOfType<Canvas>();

        textBox.gameObject.SetActive(false);

        // Canvasの存在確認
        if (canvas != null)
        {
            exclamationMarkClone = Instantiate(exclamationMark,canvas.transform);
            exclamationMarkClone.transform.localScale *= scale;
            exclamationMarkClone.SetActive(false);
        }
        else
        {
            Debug.Log("canvasがない");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(InitilizeFlg)
        {
            Event.SetEventActionEventFlg("女将との会話", true);
            InitilizeFlg = false;
        }

        exclamationMarkClone.transform.position = Camera.main.WorldToScreenPoint(ActionObject.transform.position + offset);
      

        // 会話を進める
        if (Input.GetKeyDown(KeyCode.Space) && testFlag && !finishtalk)
        {
            //string nextConversation = TestTalkManager.Instance.GetTalk(taklEventIndex, actionEventIndex + 1);
            string nextConversation = TestTalkManager.Instance.GetTalk(talkEventIndex, actionEventIndex+1);
            string nextName = TestTalkManager.Instance.GetTalkName(talkEventIndex, actionEventIndex+1);

            actionEventIndex++;
            if (string.IsNullOrEmpty(nextConversation))
            {
                Debug.Log("会話終了");
                finishtalk = true; // 会話終了フラグを立てる
                //透明の壁判定削除
                Event.SetEventActionEventFlg("女将との会話", false);
                text.text = "";
            }

            else
            {
                conversationText = nextConversation;
                Debug.Log("会話内容: " + conversationText);
                text.text = conversationText;
            }

            if (string.IsNullOrEmpty(nextName))
            {
                nametext.text = "";
                Debug.Log("名前クリア");
            }
            else
            {
                //conversationText = nextName;
                nametext.text = nextName;
            }
        }
    }

    // 当たり判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("actionIndex : " + talkEventIndex);
            textBox.gameObject.SetActive(true);
            exclamationMarkClone.SetActive(true);
            Debug.Log("！マーク表示");
            // 会話データのインデックスを取得して会話内容表示
            conversationText = TestTalkManager.Instance.GetTalk(talkEventIndex, actionEventIndex);
            nameText = TestTalkManager.Instance.GetTalkName(talkEventIndex, actionEventIndex);
            text.text = conversationText;
            nametext.text = nameText;
            testFlag = true;
            Debug.Log("flag" + testFlag);
            UpdatefinishFlag();
        }
    }

    // 当たり判定が外れた時の処理
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //talkListnum =talknum;
            //talkListindex = listnum;
            if (textBox != null)
            {
                textBox.gameObject.SetActive(false);
                Debug.Log("テキストボックス非表示");
            }
            
            if (exclamationMarkClone == true)
            {
                exclamationMarkClone.SetActive(false);
            }
            text.text = "";
            nametext.text = "";
            //text.text = conversationText;
            Debug.Log("！マーク非表示");
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
