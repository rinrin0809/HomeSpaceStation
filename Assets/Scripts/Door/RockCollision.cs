using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollision : MonoBehaviour
{
    private Player player;

    private Mover mover;

    [SerializeField]
    public string ItemName = "";

    void Start()
    {
        player = FindObjectOfType<Player>();
        mover = this.gameObject.GetComponent<Mover>();
    }

    void Update()
    {
        if (ItemName == null) return;

        if (player.GimicHitFlg)
        {
            for (int i = 0; i < player.GetInventory().GetSize(); i++)
            {
                foreach (var item in player.GetInventory().GetCurrentInventoryState())
                {
                    // �C���x���g���̃A�C�e�����擾���āA�A�C�e���̖��O�Ń`�F�b�N
                    if (item.Value.item.Name == ItemName)
                    {
                        mover.RockFlg = false;
                        break; // ����������烋�[�v�𔲂���
                    }
                }
            }
        }
    }
}
