using System.Collections;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public float maxDistance = 10f; // 最大で足音が聞こえる距離
    public float minVolume = 0f; // 最小音量
    public float maxVolume = 1f; // 最大音量

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying) audioSource.Play(); // 足音をループ再生
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= maxDistance)
        {
            float volume = Mathf.Lerp(maxVolume, minVolume, distance / maxDistance);
            audioSource.volume = volume;
        }
        else
        {
            audioSource.volume = 0f; // 範囲外なら音を消す
        }
    }
}
