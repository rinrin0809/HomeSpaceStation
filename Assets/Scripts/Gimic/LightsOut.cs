using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LightsOut : MonoBehaviour
{

    public GameObject panelprefab;
    public Button undoButton; // ���Ƃɖ߂��{�^��

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

    // �^�C�g���p
    [SerializeField]
    private Button Button3x3;
    [SerializeField]
    private Button  Button4x4;
    [SerializeField]
    private Button Button5x5;
  

    void Start()
    {
      
        CreateGrid();
        // ���Ƃɖ߂��{�^���Ƀ��X�i�[��ǉ�
        undoButton.onClick.AddListener(ClearAllLights);
        
        // debug�p
        if(DebugButton != null)
        {
            DebugButton.onClick.AddListener(DebugMode);
        }

        // �^�C�g���p
        if (Button3x3 != null) Button3x3.onClick.AddListener(Onclick3x3);
        if (Button4x4 != null) Button4x4.onClick.AddListener(Onclick4x4);
        if (Button5x5 != null) Button5x5.onClick.AddListener(Onclick5x5);

    }

   

    private void DebugMode()
    {
        foreach (GameObject obj in newPrefab)
        {
            Destroy(obj); // �Q�[���I�u�W�F�N�g���폜
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


        // �O���b�h�Ƀ{�^���𐶐�
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
            Debug.Log("�N���A��:" + ClearCount);
            foreach (GameObject obj in newPrefab)
            {
                Destroy(obj); // �Q�[���I�u�W�F�N�g���폜
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
                if (!isOn[x, y]) // ��ł�false�������false��Ԃ�
                {
                    return false;
                }
            }
        }
        return true; // �S��true�Ȃ�true��Ԃ�
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
        Color color = isOn[x, y] ? Color.gray : Color.yellow;
        buttons[x, y].GetComponent<Image>().color = color;
       
    }

    // ���ׂẴ��C�g���N���A
    void ClearAllLights()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                isOn[x, y] = false; // ���ׂĂ̏�Ԃ��I�t��
                UpdateButtonColor(x, y); // �����ڂ��X�V
               
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


    // �^�C�g���p
    private void Onclick3x3()
    {
        foreach (GameObject obj in newPrefab)
        {
            Destroy(obj); // �Q�[���I�u�W�F�N�g���폜
        }
        gridCount.constraintCount=3;
        gridSize=3;
        CreateGrid();

    }

    private void Onclick4x4()
    {
        foreach (GameObject obj in newPrefab)
        {
            Destroy(obj); // �Q�[���I�u�W�F�N�g���폜
        }
        gridCount.constraintCount = 4;
        gridSize = 4;
        CreateGrid();
    }

    private void Onclick5x5()
    {
        foreach (GameObject obj in newPrefab)
        {
            Destroy(obj); // �Q�[���I�u�W�F�N�g���폜
        }
        gridCount.constraintCount = 5;
        gridSize = 5;
        CreateGrid();
    }


}
