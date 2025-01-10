using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightsOut : MonoBehaviour
{

    public GameObject panelprefab;

    public int gridSize = 4;

    private Button[,] buttons;

    private bool[,] isOn;



    // Start is called before the first frame update
    void Start()
    {
        buttons = new Button[gridSize, gridSize];
        isOn = new bool[gridSize, gridSize];

        // グリッドにボタンを生成
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                GameObject newButton = Instantiate(panelprefab, transform);
                int xPos = x;
                int yPos = y;

                buttons[x, y] = newButton.GetComponent<Button>();
                buttons[x, y].onClick.AddListener(() => ToggleLights(xPos, yPos));
                UpdateButtonColor(x, y);
            }
        }
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
        Color color = isOn[x, y] ? Color.yellow : Color.gray;
        buttons[x, y].GetComponent<Image>().color = color;
    }
}
