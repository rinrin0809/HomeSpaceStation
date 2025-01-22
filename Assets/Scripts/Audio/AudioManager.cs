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
        // Debug.Log("BGM���Ƃ߂�" + bgm);
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
        // ���ꂪ���x���ɂȂ�
        Title,   // �^�C�g��
       
        // �G���f�B���O���Ƃ�BGM
        Good,   // �ō��̃G���f�B���O
        Normal, // ���ʂ̃G���f�B���O
        Bad,     // �ň��ȃG���f�B���O

        // �������Ƃ�BGM
        B1RoomBgm,
        B2RoomBgm,
        B3RoomBgm,

        // �M�~�b�N���Ƃ�BGM�i����΁j
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
        // ���ꂪ���x���ɂȂ�
        DoorOpen,             // �h�A���J���Ƃ�
        LightsOutPlaySE,    // LightsOut�v���C��
        PasswordSE,          // �p�X���[�h�̃{�^�����������Ƃ�
        CorrectSE,             // ��������SE
        WrongSE,              // �s��������SE
        AlarmSE,               // �x�����Ɍ��������Ƃ��̃A���[����


    }

    public SE se;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}