using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static Vector2 playerPosition;

    public void UpdatePlayerPosition(Vector2 position)
    {
        playerPosition = position;
    }
}
