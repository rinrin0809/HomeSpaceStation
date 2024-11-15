using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMplay : MonoBehaviour
{
    [SerializeField] private SESoundData.SE selectedSe;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlaySE(selectedSe);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
