using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; set; } // �A�C�e����

    [field: SerializeField]
    [field: TextArea]
    public string Descripton { get; set; } // �A�C�e������

    [field: SerializeField]
    public Sprite ItemSprite { get; set; } = null; // �A�C�e���摜
}
