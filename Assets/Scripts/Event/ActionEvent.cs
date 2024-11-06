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

    //public TextMeshProUGUI text;
    public TextMeshProUGUI text;

    string conversationText;

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
       
    }

    // �����蔻��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            exclamationMarkClone.SetActive(true);
            // ��b�f�[�^�̃C���f�b�N�X���擾���ĉ�b���e�\��
            conversationText = TalkManager.Instance.GetTalk(0);
            Debug.Log(conversationText);
            text.text = conversationText;
            Debug.Log("�I�}�[�N�\��");
        }
    }

    // �����蔻�肪�O�ꂽ���̏���
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            exclamationMarkClone.SetActive(false);
            conversationText = TalkManager.Instance.GetTalk(1);
            Debug.Log(conversationText);
            text.text = conversationText;
            Debug.Log("�I�}�[�N��\��");
        }
    }
}
