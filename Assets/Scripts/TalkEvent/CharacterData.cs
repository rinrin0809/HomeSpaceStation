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
        protagonist,  // 主人公
        protagonistgarlfrend, // 主人公の彼女
        frendboy, // 主人公の友達
        frendGarl, // 友達の彼女
    }

}