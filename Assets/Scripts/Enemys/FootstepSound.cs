using System.Collections;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public Transform player; // �v���C���[��Transform
    public float maxDistance = 10f; // �ő�ő������������鋗��
    public float minVolume = 0f; // �ŏ�����
    public float maxVolume = 1f; // �ő剹��

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying) audioSource.Play(); // ���������[�v�Đ�
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
            audioSource.volume = 0f; // �͈͊O�Ȃ特������
        }
    }
}
