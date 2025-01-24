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
    private string CharaDirection; // 遊び
    [SerializeField]
    private Sprite image;

}

[System.Serializable]
public class CharacterPost
{
    public enum Post 
    {
       
        syujinkou,  // 主人公
        syujinkou_gal, // 主人公の彼女
        frendboy, // 主人公の友達
        frendGarl, // 友達の彼女
        okami,   // 女将


        Azki,   // あずきちゃん(仮)
    }



}