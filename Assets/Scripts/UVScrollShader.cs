using UnityEngine;

public class UVScrollShader : MonoBehaviour
{
    // �}�e���A���Q��
    [SerializeField] private Material material;

    [SerializeField] float SpeedX = 1.0f;
    [SerializeField] float SpeedY = 1.0f;

    [SerializeField] bool Upflg = false;

    void Start()
    {
        // Renderer����}�e���A�����擾
        //Renderer renderer = GetComponent<Renderer>();
        //material = renderer.material;

        // �����l��ݒ�
        material.SetFloat("_XSpeed", SpeedX); // X�����̃X�N���[�����x
        material.SetFloat("_YSpeed", SpeedY); // Y�����̃X�N���[�����x
    }

    void Update()
    {
        // ���x�𓮓I�ɕύX (��: �L�[���͂Œ���)
        if (Upflg)
        {
            material.SetFloat("_YSpeed", SpeedY); // Y�����̑��x�𑝉�
        }

        else
        {
            material.SetFloat("_XSpeed", SpeedX); // Y�����̑��x�𑝉�
        }
    }
}
