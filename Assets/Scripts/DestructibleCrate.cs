using System;
using UnityEngine;

public class DestructibleCrate : MonoBehaviour
{
    private GridPosition gridPosition;

    public static event EventHandler OnAnyDestroyed;

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public void Damage()
    {
        Destroy(gameObject);

        OnAnyDestroyed?.Invoke(this, EventArgs.Empty);
    }
}