using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterState : MonoBehaviour
{
    enum State
    {
        NoCore,
        Core,
        Max
    }

    public GameObject NoCoreObject;
    public GameObject CoreObject;

    public string ItemName = "";
    public string ChangeStateItemName = "";

    private State NowState = State.Max;

    public Animator animator;
    public RuntimeAnimatorController newAnimatorController;

    // Start is called before the first frame update
    void Start()
    {
        TransitionNoCoreState();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(NowState)
        {
            case State.NoCore:
                UpdateNoCoreState();
                break;

            case State.Core:
                UpdateCoreState();
                break;
        }
    }

    private void TransitionNoCoreState()
    {
        NowState = State.NoCore;
        NoCoreObject.SetActive(true);
        CoreObject.SetActive(false);
    }

    private void UpdateNoCoreState()
    {
        for (int i = 0; i < Player.Instance.GetInventory().GetSize(); i++)
        {
            foreach (var item in Player.Instance.GetInventory().GetCurrentInventoryState())
            {
                // インベントリのアイテムを取得して、アイテムの名前でチェック
                if (item.Value.item.Name == ItemName)
                {
                    TransitionCoreState();
                    break; // 一つ見つかったらループを抜ける
                }
            }
        }
    }

    private void TransitionCoreState()
    {
        NowState = State.Core;
        CoreObject.SetActive(true);
        NoCoreObject.SetActive(false);
    }

    private void UpdateCoreState()
    {
        for (int i = 0; i < Player.Instance.GetInventory().GetSize(); i++)
        {
            foreach (var item in Player.Instance.GetInventory().GetCurrentInventoryState())
            {
                // インベントリのアイテムを取得して、アイテムの名前でチェック
                if (item.Value.item.Name == ChangeStateItemName)
                {
                    ChangeController();
                }
            }
        }
    }

    private void ChangeController()
    {
        if (newAnimatorController != null)
        {
            animator.runtimeAnimatorController = newAnimatorController;
            Debug.Log("Animator Controller Changed!");
        }
        else
        {
            Debug.LogWarning("New Animator Controller is not assigned.");
        }
    }
}
