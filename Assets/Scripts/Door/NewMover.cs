using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMover : MonoBehaviour
{
    public GameObject door;  // ���̃Q�[���I�u�W�F�N�g
    public Vector3 openPosition;  // �����J�����Ƃ��̈ʒu
    public float openSpeed = 2f;  // �����J�����x
    public bool isOpening = false;
    public bool RockFlg = false;
    public InputNumber inputnumber;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("�J�n�� " + door.transform.localPosition);
        if (door == null)
        {
            Debug.LogError("Door not assigned in the inspector!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!RockFlg)
        {
            // �����J���鏈��
            if (isOpening == true)
            {
                door.transform.localPosition = Vector3.Lerp(door.transform.localPosition, openPosition, openSpeed * 0.033f);
            }
            if (isOpening == true && !inputnumber)
            {
                door.transform.localPosition = Vector3.Lerp(door.transform.localPosition, openPosition, openSpeed * 0.033f);
            }
        }
       
    }
}
