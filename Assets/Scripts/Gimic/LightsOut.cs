using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LightsOut : MonoBehaviour
{

    public GameObject panelprefab;
    public Button undoButton; // もとに戻すボタン

    public int gridSize = 4;

    private Button[,] buttons;
    private bool[,] isOn;
    private List<GameObject> newPrefab = new List<GameObject>();

    public GameObject GimmickPanel;
    private int ClearCount = 0;
    [SerializeField]
    GridLayoutGroup gridCount;
    public int CountMax;
    public GameObject TextBox;

    public Button DebugButton;

    void Start()
    {
      
        CreateGrid();
        // もとに戻すボタンにリスナーを追加
        undoButton.onClick.AddListener(ClearAllLights);
        
        // debug用
        if(DebugButton != null)
        {
            DebugButton.onClick.AddListener(DebugMode);
        }
       
    }

   

    private void DebugMode()
    {
        foreach (GameObject obj in newPrefab)
        {
            Destroy(obj); // ゲームオブジェクトを削除
        }
        newPrefab.Clear();
        gridCount.constraintCount++;
        gridSize++;
        CreateGrid();
        ClearCount++;
    }

    void CreateGrid()
    {
        buttons = new Button[gridSize, gridSize];
        isOn = new bool[gridSize, gridSize];


        // グリッドにボタンを生成
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                GameObject newButton = Instantiate(panelprefab, transform);
                newPrefab.Add(newButton);
                int xPos = x;
                int yPos = y;

                buttons[x, y] = newButton.GetComponent<Button>();
                buttons[x, y].onClick.AddListener(() => ToggleLights(xPos, yPos));
                UpdateButtonColor(x, y);
            }
        }
    }

  

    private void Update()
    {
        AreAllButtonsOn();
        if(AreAllButtonsOn() == true)
        {
            ClearCount++;
            Debug.Log("クリア回数:" + ClearCount);
            foreach (GameObject obj in newPrefab)
            {
                Destroy(obj); // ゲームオブジェクトを削除
            }
            newPrefab.Clear();
            gridCount.constraintCount++;
            gridSize++;
            CreateGrid();
        }
        if (ClearCount == CountMax)
        {
            LevelClear();
        }
     
    }
    bool AreAllButtonsOn()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if (!isOn[x, y]) // 一つでもfalseがあればfalseを返す
                {
                    return false;
                }
            }
        }
        return true; // 全てtrueならtrueを返す
    }

    // ボタンを押した時に状態を切り替える
    void ToggleLights(int x, int y)
    {
        ToggleLight(x, y);           // 押したボタン
        ToggleLight(x - 1, y);       // 上のボタン
        ToggleLight(x + 1, y);       // 下のボタン
        ToggleLight(x, y - 1);       // 左のボタン
        ToggleLight(x, y + 1);       // 右のボタン
    }

    // 指定された座標のライトを切り替える
    void ToggleLight(int x, int y)
    {
        if (x >= 0 && x < gridSize && y >= 0 && y < gridSize)
        {
            isOn[x, y] = !isOn[x, y];
            UpdateButtonColor(x, y);
        }
    }

    // ボタンの色を状態に応じて変更
    void UpdateButtonColor(int x, int y)
    {
        Color color = isOn[x, y] ? Color.gray : Color.yellow;
        buttons[x, y].GetComponent<Image>().color = color;
       
    }

    // すべてのライトをクリア
    void ClearAllLights()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                isOn[x, y] = false; // すべての状態をオフに
                UpdateButtonColor(x, y); // 見た目を更新
               
            }
        }
    }
 
    public void LevelClear()
    {
        Hide();
        ClearAllLights();
        
    }

    public void Hide()
    {
        GimmickPanel.gameObject.SetActive(false);
        TextBox.gameObject.SetActive(true);
    }

    public void Show()
    {
        GimmickPanel.gameObject.SetActive(true);
    }
}
