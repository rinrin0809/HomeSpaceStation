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

        // �O���b�h�Ƀ{�^���𐶐�
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

    // �{�^�������������ɏ�Ԃ�؂�ւ���
    void ToggleLights(int x, int y)
    {
        ToggleLight(x, y);           // �������{�^��
        ToggleLight(x - 1, y);       // ��̃{�^��
        ToggleLight(x + 1, y);       // ���̃{�^��
        ToggleLight(x, y - 1);       // ���̃{�^��
        ToggleLight(x, y + 1);       // �E�̃{�^��
    }

    // �w�肳�ꂽ���W�̃��C�g��؂�ւ���
    void ToggleLight(int x, int y)
    {
        if (x >= 0 && x < gridSize && y >= 0 && y < gridSize)
        {
            isOn[x, y] = !isOn[x, y];
            UpdateButtonColor(x, y);
        }
    }

    // �{�^���̐F����Ԃɉ����ĕύX
    void UpdateButtonColor(int x, int y)
    {
        Color color = isOn[x, y] ? Color.yellow : Color.gray;
        buttons[x, y].GetComponent<Image>().color = color;
    }
}
