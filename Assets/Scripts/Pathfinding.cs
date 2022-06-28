using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] private Transform gridDebugObjectPrefab = null;

    private int width;
    private int height;
    private int cellSize;

    GridSystem<PathNode> gridSystem;

    private void Start()
    {
        gridSystem = new GridSystem<PathNode>(LevelGrid.Instance.GetGridWidth(), 
                                                LevelGrid.Instance.GetGridHeight(), 
                                                LevelGrid.Instance.GetGridCellSize(), 
                                                (GridSystem<PathNode> g, GridPosition gridPosition) => new PathNode(gridPosition));
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }
}