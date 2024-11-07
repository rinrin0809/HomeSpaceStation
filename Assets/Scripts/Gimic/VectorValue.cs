using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject
{
    public Vector2 initialValue;
    public bool isInitialPositionSet = false; // 初期ポジション設定済みフラグ

}
