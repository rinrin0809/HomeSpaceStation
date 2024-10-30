using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (/*player.GetItemByName("key") != null*/player.HasKeyFlg && player.GimicHitFlg)
        {
            gameObject.SetActive(false);
            player.HasKeyFlg = false;
            ///player.RemoveGameObjectByName(player.GetItemList);
        }
    }
}
