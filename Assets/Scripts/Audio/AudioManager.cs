using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource seAudioSource;

    [SerializeField] List<BGMSoundData> bgmSoundDatas;
    [SerializeField] private List<SESoundData> seSoundDatas;

    public float masterVolume = 1;
    public float bgmMasterVolume = 1;
    public float seMasterVolume = 1;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(BGMSoundData.BGM bgm)
    {
        BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
        bgmAudioSource.clip = data.audioClip;
        bgmAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
        bgmAudioSource.Play();
    }


    public void StopBGM(BGMSoundData.BGM bgm)
    {
        // Debug.Log("BGMをとめた" + bgm);
        bgmAudioSource.Stop();
    }

    public void PlaySE(SESoundData.SE se)
    {
        SESoundData data = seSoundDatas.Find(data => data.se == se);
        seAudioSource.volume = data.volume * seMasterVolume * masterVolume;
        seAudioSource.PlayOneShot(data.audioClip);
    }

}

[System.Serializable]
public class BGMSoundData
{
    public enum BGM
    {
        // これがラベルになる
        Title,   // タイトル
       
        // エンディングごとのBGM
        Good,   // 最高のエンディング
        Normal, // 普通のエンディング
        Bad,     // 最悪なエンディング

        // 部屋ごとのBGM
        B1RoomBgm,
        B2RoomBgm,
        B3RoomBgm,

        // ギミックごとのBGM（あれば）
        SkillCheckBgm,


    }

    public BGM bgm;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}

[CreateAssetMenu]
[System.Serializable]
public class SESoundData 
{
    public enum SE
    {
        // これがラベルになる
        DoorOpen,             // ドアが開くとき
        LightsOutPlaySE,    // LightsOutプレイ時
        PasswordSE,          // パスワードのボタンを押したとき
        CorrectSE,             // 正解時のSE
        WrongSE,              // 不正解時のSE
        AlarmSE,               // 警備員に見つかったときのアラーム音


    }

    public SE se;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}