using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        ChangeSceneName("Title","Over");
    }

    //Nameシーンへ遷移
    public void ChangeSceneName(string Name, string Name2)
    {
        // Bキーが押されたか確認 (KeyCode.B はBキー)
        if (Input.GetKeyDown(KeyCode.B))
        {
            //Nameシーンに遷移
            SceneManager.LoadScene(Name);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            //Nameシーンに遷移
            SceneManager.LoadScene(Name2);
        }
    }
}
