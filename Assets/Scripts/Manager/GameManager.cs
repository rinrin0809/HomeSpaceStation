using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InventryData Inventory;

    // Update is called once per frame
    void Update()
    {
        ChangeSceneName("Title","Over");
    }

    //Nameシーンへ遷移
    public void ChangeSceneName(string Name, string Name2)
    {
        if (!MenuManager.Instance.GetOpenMenuFlg())
        {
            // Bキーが押されたか確認 (KeyCode.B はBキー)
            if (Input.GetKeyDown(KeyCode.B))
            {
                Inventory.ResetInventory();
                //Nameシーンに遷移
                SceneManager.LoadScene(Name);
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                Inventory.ResetInventory();
                //Nameシーンに遷移
                SceneManager.LoadScene(Name2);
            }
        }
    }
}
