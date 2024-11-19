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

    //public TextMeshProUGUI text;
    public TextMeshProUGUI text;

    string conversationText;

   // private TalkManager talkManager;

    // 会話全体のリストのインデックス
    // どの会話のリストを選んでるかをこの変数を変えることで変更できる
    public int taklEventIndex = 0;
    // 会話ごとのリストのインデックス
    public int actionEventIndex = 0;

    //public int talknum=0;
    [SerializeField]
    public  int listnum = 0;


    // 会話が終了しているのかのフラグ
    [SerializeField]
    private bool finishtalk = false; 

    public bool testFlag = false;

    string characterName;

    // Start is called before the first frame update
    void Start()
    {
        // Canvasの取得
        canvas = FindObjectOfType<Canvas>();

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
        exclamationMarkClone.transform.position = Camera.main.WorldToScreenPoint(ActionObject.transform.position + offset);
        //if (Input.GetKeyDown(KeyCode.Space)&&testFlag==true)
        //{
        //    actionEventIndex += 1;
        //    listnum= actionEventIndex;
        //    conversationText = TestTalkManager.Instance.GetTalk(taklEventIndex, actionEventIndex);
        //    Debug.Log("index:" + taklEventIndex + actionEventIndex);
        //    Debug.Log(conversationText);
        //    text.text = conversationText;
        //    Debug.Log("space押した");
        //    Debug.Log("listnnum" + listnum);    
        //}

        // 会話を進める
        if (Input.GetKeyDown(KeyCode.Space) && testFlag && !finishtalk)
        {
            string nextConversation = TestTalkManager.Instance.GetTalk(taklEventIndex, actionEventIndex + 1);

            if (string.IsNullOrEmpty(nextConversation))
            {
                Debug.Log("会話終了");
                finishtalk = true; // 会話終了フラグを立てる
                text.text = "会話が終了しました";
            }
            else
            {
                actionEventIndex++;
                conversationText = nextConversation;
                Debug.Log("会話内容: " + conversationText);
                text.text = conversationText;
            }
        }
    }

    // 当たり判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            exclamationMarkClone.SetActive(true);
            Debug.Log("！マーク表示");
            // 会話データのインデックスを取得して会話内容表示
            conversationText = TestTalkManager.Instance.GetTalk(taklEventIndex, actionEventIndex);
            text.text = conversationText;
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
            if (exclamationMarkClone == true)
            {
                exclamationMarkClone.SetActive(false);
            }
            text.text = "";
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
