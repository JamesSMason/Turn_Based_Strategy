using System;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float detonationProximityToTarget = 0.2f;
    [SerializeField][Tooltip("The number of grid squares affected by the explosion.")] private float damageRadius = 2f;
    [SerializeField] private int grenadeDamage = 30;

    private Vector3 targetPosition;

    private Action onGrenadeBehaviourComplete;

    private void Update()
    {
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPosition) < detonationProximityToTarget)
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, damageRadius * 2);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out Unit unit))
                {
                    unit.Damage(grenadeDamage);
                }
            }
            Destroy(gameObject);
            onGrenadeBehaviourComplete();
        }
    }

    public void Setup(GridPosition targetGridPosition, Action onGrenadeBehaviourComplete)
    {
        this.onGrenadeBehaviourComplete = onGrenadeBehaviourComplete;
        targetPosition = LevelGrid.Instance.GetWorldPosition(targetGridPosition);
    }
}