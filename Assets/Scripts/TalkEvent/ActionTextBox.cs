using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionTextBox : MonoBehaviour
{
    public GameObject TextBox;
    public TextMeshProUGUI text;
  
    string message;
    [SerializeField]
    private List<TextBoxFlags> textboxFlags = new List<TextBoxFlags>(); 

    // Start is called before the first frame update
    void Start()
    {
        if (TextBox != null  )
        {
            TextBox.gameObject.SetActive(false);
       
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        for(int i =0; i<textboxFlags.Count; i++)
        {
            if (textboxFlags[i].TextBox == true)
            {
                text.text = textboxFlags[i].text;
            }
        }

        if(TextBox==true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                TextBox.gameObject.SetActive(false);
            }
        }
    }


   
}

[System.Serializable]
public class TextBoxFlags
{
    public bool TextBox;
    public string text;
}

