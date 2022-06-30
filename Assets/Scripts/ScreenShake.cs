using Cinemachine;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance;
    private CinemachineImpulseSource cinemachineImpulseSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log($"There are multiple ScreenShake objects in this scene. {transform} - {Instance}.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake(float intensity = 1f)
    {
        cinemachineImpulseSource.GenerateImpulse(intensity);
    }
}