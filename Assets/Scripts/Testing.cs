using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] Transform gridObjectDebug = null;

    GridSystem gridSystem;

    private void Start()
    {
        gridSystem = new GridSystem(10, 10, 2);
        gridSystem.CreateDebugObjects(gridObjectDebug);
    }

    private void Update()
    {
        Debug.Log(gridSystem.GetGridPosition(MouseWorld.GetPosition()));
    }
}