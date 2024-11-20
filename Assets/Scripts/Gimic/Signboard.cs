using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Signboard : MonoBehaviour
{
    //看板のUI参照
    [SerializeField] GameObject SignboardUI;

    private bool HitFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        SignboardUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが当たった時かつスペースキーを押した時
        if(HitFlg && Input.GetKeyDown(KeyCode.Space))
        {
            if (SignboardUI.activeSelf)
            {
                SignboardUI.SetActive(false);
                Player.Instance.ExplainDisplayFlg = false;
            }

            else
            {
                SignboardUI.SetActive(true);
                Player.Instance.ExplainDisplayFlg = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HitFlg = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HitFlg = false;
        }
    }
}
