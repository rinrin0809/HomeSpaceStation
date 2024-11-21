using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransitions : MonoBehaviour
{
    public string sceneToLoad;        //シーン名で遷移先判別
    public Vector2 playerPosition;    //シーン遷移後のプレイヤー位置
    public VectorValue playerStorage; //プレイヤーの位置を保存

    //シーン遷移
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            //名前が悪いけどstarPositionに移動するフラグをtrue
            if (!LoadManager.Instance)
            {
                LoadManager.Instance.NewGamePushFlg = true;
            }
            Debug.Log(LoadManager.Instance.NewGamePushFlg);
            //// プレイヤーの向きを保存
            //Player player = collision.GetComponent<Player>();
            //if (player != null)
            //{
            //    player.SavePlayerRotation(); // プレイヤーの向きを保存
            //}

            // プレイヤーの位置を保存し、シーンを切り替え
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}