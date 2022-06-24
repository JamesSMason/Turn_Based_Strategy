using System;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] Animator unitAnimator = null;

    private void Awake()
    {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction))
        {
            moveAction.OnStartMoving += MoveAction_OnStartMoving;
            moveAction.OnStopMoving += MoveAction_OnStopMoving;
        }
        if (TryGetComponent<ShootAction>(out ShootAction shootAction))
        {
            shootAction.OnShootStart += ShootAction_OnShootStart;
        }
    }

    private void ShootAction_OnShootStart(object sender, EventArgs e)
    {
        unitAnimator.SetTrigger("IsShooting");
    }

    private void MoveAction_OnStartMoving(object sender, EventArgs e)
    {
        Debug.Log(unitAnimator.GetBool("IsWalking"));
        unitAnimator.SetBool("IsWalking", true);
    }

    private void MoveAction_OnStopMoving(object sender, EventArgs e)
    {
        unitAnimator.SetBool("IsWalking", false);
    }
}