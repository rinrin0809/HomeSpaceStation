using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class CSRender : MonoBehaviour
{
    const string SHEET_ID = "1ONbbb-l-kNiNBFjVsrX8QuKk81k6PhHmy2OoqnYUuFA";
    const string SHEET_NAME = "�V�[�g1";

    private List<CharacterCSData> DialogueDataList;
    //private Dictionary<int, (string, string, int)> message = new Dictionary<int, (string, string, int)>();

    public TextMeshProUGUI text; // ��b�\���e�L�X�g

    void Start()
    {
        text.text = "";
        StartCoroutine(MEthod(SHEET_NAME));
    }

    IEnumerator MEthod(string _SHEET_NAME)
    {
        UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + SHEET_ID + "/edit?usp=sharing" + _SHEET_NAME);
        yield return request.SendWebRequest();

        if(request.isHttpError || request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            //List<string[]> characterDataArrayList = ConvertToArrayListFrom(request.downloadHandler.text);
            //foreach (string[] characterDataArray in characterDataArrayList)
            //{
            //    CharacterCSData characterData = new CharacterCSData(characterDataArray);
            //    characterData.DebugParametaView();
            //}
            Debug.Log(request.downloadHandler.text);
        }

        
    }

    List<string[]> ConvertToArrayListFrom(string _text)
    {
        List<string[]> characterDataList = new List<string[]>();
        StringReader render = new StringReader(_text);
        render.ReadLine(); // ��s�ڂ̓��x���Ȃ̂ŊO��
        while(render.Peek() != -1){
            string line = render.ReadLine(); // ��s���ǂݍ���
            string[] elements = line.Split(','); // �s�̃Z����,�ŋ�؂���
            for(int i=0; i < elements.Length; i++)
            {
                if (elements[i] == "\"\"")
                {
                    continue;
                }
                elements[i] = elements[i].TrimStart('"').TrimEnd('"');
            }
            characterDataList.Add(elements);
        }
        return characterDataList;
    }

    //public void ReLoadGoogleSheet()
    //{
    //    StartCoroutine(MEthod(SHEET_NAME));
    //}


}

