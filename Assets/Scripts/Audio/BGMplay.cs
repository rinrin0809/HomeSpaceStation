using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMplay : MonoBehaviour
{
    [SerializeField] private BGMSoundData.BGM selectedBgm;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM(selectedBgm);
        
    }

     public void StopBGM()
    {
        AudioManager.Instance.StopBGM(selectedBgm);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
