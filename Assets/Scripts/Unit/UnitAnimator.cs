using System;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] Animator unitAnimator = null;

    [Header("Object References")]
    [SerializeField] Transform bulletProjectilePrefab = null;
    [SerializeField] Transform shootPointTransform = null;
    [SerializeField] Transform rifleTransform = null;
    [SerializeField] Transform swordTransform = null;

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
        if (TryGetComponent<SwordAction>(out SwordAction swordAction))
        {
            swordAction.OnSwordActionStarted += SwordAction_OnSwordActionStarted;
            swordAction.OnSwordActionCompleted += SwordAction_OnSwordActionCompleted;
        }
    }

    private void Start()
    {
        EquipRifle();
    }

    private void SwordAction_OnSwordActionCompleted(object sender, EventArgs e)
    {
        EquipRifle();
    }

    private void SwordAction_OnSwordActionStarted(object sender, EventArgs e)
    {
        EquipSword();
        unitAnimator.SetTrigger("SwordSlash");
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

    private void EquipRifle()
    {
        rifleTransform.gameObject.SetActive(true);
        swordTransform.gameObject.SetActive(false);
    }

    private void EquipSword()
    {
        rifleTransform.gameObject.SetActive(false);
        swordTransform.gameObject.SetActive(true);
    }
}