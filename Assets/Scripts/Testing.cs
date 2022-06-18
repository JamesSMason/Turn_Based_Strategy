using UnityEngine;

public class Testing : MonoBehaviour
{
    GridSystem gridSystem;

    private void Start()
    {
        gridSystem = new GridSystem(10, 10, 2);
    }

    private void Update()
    {
        Debug.Log(gridSystem.GetGridPosition(MouseWorld.GetPosition()));
    }
}