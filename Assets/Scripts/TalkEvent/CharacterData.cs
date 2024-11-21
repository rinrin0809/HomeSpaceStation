using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    private CharacterPost.Post charaPost;

    public string CharacterName;

    public SpriteRenderer sprite;



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
    }



}