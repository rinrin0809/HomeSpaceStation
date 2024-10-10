using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; set; } // アイテム名

    [field: SerializeField]
    [field: TextArea]
    public string Descripton { get; set; } // アイテム説明

    [field: SerializeField]
    public Sprite ItemSprite { get; set; } = null; // アイテム画像
}
