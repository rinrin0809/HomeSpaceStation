using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    private CharacterPost.Post charaPost;

    public string CharacterName;

    public SpriteRenderer sprite;



    [field: TextArea]
    [SerializeField]
    private string CharaDirection; // �V��
    [SerializeField]
    private Sprite image;

}

[System.Serializable]
public class CharacterPost
{
    public enum Post 
    {
       
        syujinkou,  // ��l��
        syujinkou_gal, // ��l���̔ޏ�
        frendboy, // ��l���̗F�B
        frendGarl, // �F�B�̔ޏ�
        okami,   // ����


        Azki,   // �����������(��)
    }



}