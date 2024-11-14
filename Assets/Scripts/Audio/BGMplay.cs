using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlaySE(SESoundData.SE.EnemyDamage);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
