using System;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] Animator unitAnimator = null;

    [Header("Object References")]
    [SerializeField] Transform bulletProjectilePrefab = null;
    [SerializeField] Transform shootPointTransform = null;

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

    private void ShootAction_OnShootStart(object sender, ShootAction.OnShootEventArgs e)
    {
        unitAnimator.SetTrigger("IsShooting");

        Transform bulletProjectileTransform = Instantiate(bulletProjectilePrefab, shootPointTransform.position, Quaternion.identity);
        BulletProjectile bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();
        Vector3 targetPosition = e.targetUnit.GetWorldPosition();
        targetPosition.y = shootPointTransform.position.y;
        bulletProjectile.Setup(targetPosition);
    }

    private void MoveAction_OnStartMoving(object sender, EventArgs e)
    {
        unitAnimator.SetBool("IsWalking", true);
    }

    private void MoveAction_OnStopMoving(object sender, EventArgs e)
    {
        unitAnimator.SetBool("IsWalking", false);
    }
}