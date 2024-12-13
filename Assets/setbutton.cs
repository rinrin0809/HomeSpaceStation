using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setbutton : MonoBehaviour
{
    public InputNumber GimmickObj;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        IsToggle();
    }

    private void IsToggle()
    {
        if (GimmickObj.isActiveAndEnabled)
        {
            GimmickObj.Hide();

        }
        else
        {
            GimmickObj.Show();
        }
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
