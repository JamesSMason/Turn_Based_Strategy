using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private bool invert;

    private Transform mainCameraTransform;

    private void Awake()
    {
        mainCameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (invert)
        {
            Vector3 directionToCamera = (mainCameraTransform.position - transform.position).normalized;
            transform.LookAt(transform.position - directionToCamera);
        }
        else
        {
            transform.LookAt(mainCameraTransform.position);
        }
    }
}