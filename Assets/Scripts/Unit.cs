using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] Animator unitAnimator = null;

    [Header("Movement Variables")]
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float stoppingDistance = 0.1f;
    [SerializeField] private float rotateSpeed = 10f;

    private Vector3 targetPosition;

    private void Awake()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * movementSpeed * Time.deltaTime;

            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
            unitAnimator.SetBool("IsWalking", true);
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
        }
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}