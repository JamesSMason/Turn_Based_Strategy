using System;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float detonationProximityToTarget = 0.2f;
    [SerializeField][Tooltip("The number of grid squares affected by the explosion.")] private float damageRadius = 2f;
    [SerializeField] private int grenadeDamage = 30;
    [SerializeField] private Transform grenadeExplodeVFXPrefab = null;
    [SerializeField] private TrailRenderer trailRenderer = null;
    [SerializeField] private AnimationCurve arcYAnimationCurve;
    [SerializeField] private float maxHeight = 4f;

    private Vector3 targetPosition;
    private float totalDistance;
    private Vector3 positionXZ;

    private Action onGrenadeBehaviourComplete;

    public static event EventHandler OnAnyGrenadeExploded;

    private void Update()
    {
        Vector3 moveDirection = (targetPosition - positionXZ).normalized;
        positionXZ += moveDirection * moveSpeed * Time.deltaTime;

        float distance = Vector3.Distance(positionXZ, targetPosition);
        float distanceNormalized = 1 - distance / totalDistance;
        float positionY = arcYAnimationCurve.Evaluate(distanceNormalized) * totalDistance / maxHeight;

        transform.position = new Vector3(positionXZ.x, positionY, positionXZ.z);

        if (Vector3.Distance(positionXZ, targetPosition) < detonationProximityToTarget)
        {
            Collider[] colliderArray = Physics.OverlapSphere(positionXZ, damageRadius * 2);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out Unit unit))
                {
                    unit.Damage(grenadeDamage);
                }
                if (collider.TryGetComponent(out DestructibleCrate destructibleCrate))
                {
                    destructibleCrate.Damage();
                }
            }

            OnAnyGrenadeExploded?.Invoke(this, EventArgs.Empty);

            trailRenderer.transform.parent = null;
            Instantiate(grenadeExplodeVFXPrefab, targetPosition + Vector3.up * 1f, Quaternion.identity);

            Destroy(gameObject);
            onGrenadeBehaviourComplete();
        }
    }

    public void Setup(GridPosition targetGridPosition, Action onGrenadeBehaviourComplete)
    {
        this.onGrenadeBehaviourComplete = onGrenadeBehaviourComplete;
        targetPosition = LevelGrid.Instance.GetWorldPosition(targetGridPosition);

        positionXZ = transform.position;
        positionXZ.y = 0;

        totalDistance = Vector3.Distance(positionXZ, targetPosition);
    }
}