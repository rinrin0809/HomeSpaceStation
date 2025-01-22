using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenPosition : MonoBehaviour
{
    public GameObject OpenDoor;
    private bool isPlayerInRangeOpenDoor = false;
    public Mover move;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーがコライダに入ったとき
        if (other.CompareTag("Player"))
        {
            isPlayerInRangeOpenDoor = true;
            Debug.Log("プレイヤーがコライダに入った！");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // プレイヤーがコライダから出たとき
        if (other.CompareTag("Player"))
        {
            isPlayerInRangeOpenDoor = false;
            move.isMoving = false;
            Debug.Log("プレイヤーがコライダから出た！");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRangeOpenDoor && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("スペースキーが押された！ギミックをアクティブにします。");
            if(move != null)
            {
                move.isMoving = true;
            }
        }
    }
}
