using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraMoveSpeed = 10f;
    [SerializeField] private float cameraRotationSpeed = 100f;

    void Update()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);
        Vector3 inputRotationDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = 1f;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            inputRotationDir.y = 1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            inputRotationDir.y = -1f;
        }

        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += moveVector * cameraMoveSpeed * Time.deltaTime;
        transform.eulerAngles += inputRotationDir * cameraRotationSpeed * Time.deltaTime;
    }
}