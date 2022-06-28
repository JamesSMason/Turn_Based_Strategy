using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            GridPosition startGridPosition = new GridPosition(0, 0);
            List<GridPosition> gridPositionList = Pathfinding.Instance.FindPath(startGridPosition, mouseGridPosition);
            for (int i = 1; i < gridPositionList.Count; i++)
            {
                Debug.DrawLine(LevelGrid.Instance.GetWorldPosition(gridPositionList[i - 1]),
                                LevelGrid.Instance.GetWorldPosition(gridPositionList[i]),
                                Color.white,
                                10f);
            }
        }
    }
}