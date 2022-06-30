using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 200f;
    [SerializeField] TrailRenderer trailRenderer = null;
    [SerializeField] Transform bulletHitVFXPrefab = null;

    private Vector3 targetPosition;

    private void Update()
    {
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        float distanceBeforeMoving = Vector3.Distance(targetPosition, transform.position);

        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        float distanceAfterMoving = Vector3.Distance(targetPosition, transform.position);

        if (distanceBeforeMoving < distanceAfterMoving)
        {
            transform.position = targetPosition;

            trailRenderer.transform.parent = null;

            Destroy(gameObject);

            Instantiate(bulletHitVFXPrefab, targetPosition, Quaternion.identity);
        }
    }

    public void Setup(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}