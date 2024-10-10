using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMoveText : MonoBehaviour
{
    public Transform character;
    public TextMeshProUGUI actionTextTMP;

    public Vector3 offset = new Vector3(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        actionTextTMP.transform.position = Camera.main.WorldToScreenPoint(character.position+offset);
    }
}
