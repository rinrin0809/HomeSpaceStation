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

    //Name�V�[���֑J��
    public void ChangeSceneName(string Name, string Name2)
    {
        if (!MenuManager.Instance.GetOpenMenuFlg())
        {
            // B�L�[�������ꂽ���m�F (KeyCode.B ��B�L�[)
            if (Input.GetKeyDown(KeyCode.B))
            {
                Inventory.ResetInventory();
                //Name�V�[���ɑJ��
                SceneManager.LoadScene(Name);
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                Inventory.ResetInventory();
                //Name�V�[���ɑJ��
                SceneManager.LoadScene(Name2);
            }
        }
    }
}
