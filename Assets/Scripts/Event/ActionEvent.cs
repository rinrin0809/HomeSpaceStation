using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TalkManager;

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

    private TalkManager talkManager;

    // 会話全体のリストのインデックス
    // どの会話のリストを選んでるかをこの変数を変えることで変更できる
    public int taklEventIndex = 0;
    // 会話ごとのリストのインデックス
    public int actionEventIndex = 0;

    public int talknum=0;
    public int listnum = 0;
    public bool testFlag = false;

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
        if (Input.GetKeyDown(KeyCode.Space)&&testFlag==true)
        {
            actionEventIndex += 1;
            conversationText = TestTalkManager.Instance.GetTalk(taklEventIndex, actionEventIndex);
            Debug.Log("index:" + taklEventIndex + actionEventIndex);
            Debug.Log(conversationText);
            text.text = conversationText;
            Debug.Log("space押した");
        }
    }

    //public void test()
    //{
    //    talkListindex += 1;

    //    Debug.Log("呼び出し:" + talkListindex);

    //    talkListnum++;
    //}


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

            //TalkManager.Instance.StartConversation(ActionTalkData.Action.Reception, 0);
            //TalkManager.Instance.StartConversation(TalkManager.ConversationLabel.Reception, TalkManager.ConversationEntryLabel.Greeting);
            //text.text = conversationText;

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
           
            //conversationText = TestTalkManager.Instance.GetTalk(talkListnum,listnum);
            //TalkManager.Instance.StartConversation(ActionTalkData.Action.Reception, 1);
            //TalkManager.Instance.StartConversation(TalkManager.ConversationLabel.Reception, TalkManager.ConversationEntryLabel.Warning);
            text.text = "";
            //text.text = conversationText;
            Debug.Log("！マーク非表示");
            testFlag = false;
            Debug.Log("flag" + testFlag);
        }
    }
}
