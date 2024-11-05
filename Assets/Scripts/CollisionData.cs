using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CollisionData : ScriptableObject
{
    //ƒ‰ƒxƒ‹
    public enum CollisionName
    {
        Entrance,
        EntranceStairs,
        EntranceLeft,
        EntranceRight
    }

    [SerializeField]
    private List<bool> CollisionFlg;

    public void SetCollisionFlg(int index)
    {
        //CollisionFlg[index] = CollisionFlg[CollisionName.Entrance];
    }

    private CollisionName Name = CollisionName.Entrance;
}
