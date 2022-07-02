using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera = null;
    [SerializeField] private float cameraMoveSpeed = 10f;
    [SerializeField] private float cameraRotationSpeed = 100f;
    [SerializeField] private float zoomFactor = 1f;
    [SerializeField] private float zoomSpeed = 5f;

    private CinemachineTransposer cinemachineTransposer;
    private Vector3 targetFollowOffset;

    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;

    private void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleMovement()
    {
        Vector2 inputMoveDir = InputManager.Instance.GetCameraMoveVector();

        Vector3 moveVector = transform.forward * inputMoveDir.y + transform.right * inputMoveDir.x;
        transform.position += moveVector * cameraMoveSpeed * Time.deltaTime;
    }

    private void HandleRotation()
    {
        Vector3 inputRotationDir = new Vector3(0, 0, 0);
        inputRotationDir.y = InputManager.Instance.GetCameraRotateAmount();

        transform.eulerAngles += inputRotationDir * cameraRotationSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        targetFollowOffset.y += InputManager.Instance.GetCameraZoomAmount() * zoomFactor;
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);

        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
    }
}