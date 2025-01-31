using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkRender : MonoBehaviour
{
    public GameObject exclamationMark;
    public Transform ActionObject;
    public Vector3 offset = new Vector3(0, 1, 0);
    private GameObject exclamationMarkClone; // �N���[���Q��
    [SerializeField]
    private Canvas canvas; // canvas �Q��
    float scale = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        if (canvas != null)
        {
            exclamationMarkClone = Instantiate(exclamationMark, canvas.transform);
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
        }
    }

    // �����蔻�肪�O�ꂽ���̏���
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (exclamationMarkClone == true)
            {
                exclamationMarkClone.SetActive(false);
            }
        }
    }
}
