using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding Instance { get; private set; }

    [SerializeField] private Transform gridDebugObjectPrefab = null;
    [SerializeField] private float raycastOffsetDistance = 5f;
    [SerializeField] LayerMask obstaclesLayerMask;

    private int width;
    private int height;
    private int cellSize;

    GridSystem<PathNode> gridSystem;

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log($"There are multiple Pathfinding objects in this scene. {transform} - {Instance}.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Setup(int width, int height, int cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridSystem = new GridSystem<PathNode>(width, height, cellSize, (GridSystem<PathNode> g, GridPosition gridPosition) => new PathNode(gridPosition));
        //gridSystem.CreateDebugObjects(gridDebugObjectPrefab);


        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Vector3 worldPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
                if (Physics.Raycast(worldPosition + Vector3.down * raycastOffsetDistance, Vector3.up, out RaycastHit hit, 20f, obstaclesLayerMask))
                {
                    GetNode(x, z).SetIsWalkable(false);
                }
            }
        }
    }

    public List<GridPosition> FindPath(GridPosition startGridPosition, GridPosition endGridPosition, out int pathLength)
    {
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        PathNode startNode = gridSystem.GetGridObject(startGridPosition);
        PathNode endNode = gridSystem.GetGridObject(endGridPosition);

        openList.Add(startNode);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                PathNode pathNode = gridSystem.GetGridObject(gridPosition);
                pathNode.SetGCost(int.MaxValue);
                pathNode.SetHCost(0);
                pathNode.CalculateFCost();
                pathNode.ResetCameFromPathNode();
            }
        }

        startNode.SetGCost(0);
        startNode.SetHCost(CalculateDistance(startGridPosition, endGridPosition));
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostPathNode(openList);

            if (currentNode == endNode)
            {
                pathLength = endNode.GetFCost();
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourPathNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourPathNode))
                {
                    continue;
                }

                if (!neighbourPathNode.GetIsWalkable())
                {
                    closedList.Add(neighbourPathNode);
                    continue;
                }

                int tentativeGCost = currentNode.GetGCost() + CalculateDistance(currentNode.GetGridPosition(), neighbourPathNode.GetGridPosition());
                if (tentativeGCost < neighbourPathNode.GetGCost())
                {
                    neighbourPathNode.SetCameFromPathNode(currentNode);
                    neighbourPathNode.SetGCost(tentativeGCost);
                    neighbourPathNode.SetHCost(CalculateDistance(neighbourPathNode.GetGridPosition(), endGridPosition));
                    neighbourPathNode.CalculateFCost();
                }

                if (!openList.Contains(neighbourPathNode))
                {
                    openList.Add(neighbourPathNode);
                }
            }
        }

        pathLength = 0;
        return null;
    }

    public int CalculateDistance(GridPosition gridPositionA, GridPosition gridPositionB)
    {
        GridPosition gridPostionDistance = gridPositionA - gridPositionB;
        int xDistance = Mathf.Abs(gridPostionDistance.x);
        int zDistance = Mathf.Abs(gridPostionDistance.z);
        int diagonalDistance = Mathf.Min(xDistance, zDistance);
        return diagonalDistance * MOVE_DIAGONAL_COST + Mathf.Abs(xDistance - zDistance) * MOVE_STRAIGHT_COST;
    }

    private PathNode GetLowestFCostPathNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostPathNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].GetFCost() < lowestFCostPathNode.GetFCost())
            {
                lowestFCostPathNode = pathNodeList[i];
            }
        }
        return lowestFCostPathNode;
    }

    private PathNode GetNode(int x, int z)
    {
        return gridSystem.GetGridObject(new GridPosition(x, z));
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();
        GridPosition currentGridPosition = currentNode.GetGridPosition();

        if (currentGridPosition.x - 1 >= 0)
        {
            // Left
            neighbourList.Add(GetNode(currentGridPosition.x - 1, currentGridPosition.z + 0));

            if (currentGridPosition.z + 1 < height)
            {
                //  Up Left
                neighbourList.Add(GetNode(currentGridPosition.x - 1, currentGridPosition.z + 1));
            }
            if (currentGridPosition.z - 1 >= 0)
            {
                // Down Left
                neighbourList.Add(GetNode(currentGridPosition.x - 1, currentGridPosition.z - 1));
            }
        }

        if (currentGridPosition.x + 1 < width)
        {
            // Right
            neighbourList.Add(GetNode(currentGridPosition.x + 1, currentGridPosition.z + 0));

            if (currentGridPosition.z + 1 < height)
            {
                // Up Right
                neighbourList.Add(GetNode(currentGridPosition.x + 1, currentGridPosition.z + 1));
            }
            if (currentGridPosition.z - 1 >= 0)
            {
                // Down Right
                neighbourList.Add(GetNode(currentGridPosition.x + 1, currentGridPosition.z - 1));
            }
        }

        if (currentGridPosition.z + 1 < height)
        {
            // Up
            neighbourList.Add(GetNode(currentGridPosition.x + 0, currentGridPosition.z + 1));
        }

        if (currentGridPosition.z - 1 >= 0)
        {
            // Down
            neighbourList.Add(GetNode(currentGridPosition.x + 0, currentGridPosition.z - 1));
        }

        return neighbourList;
    }

    private List<GridPosition> CalculatePath(PathNode endNode)
    {
        List<PathNode> pathNodeList = new List<PathNode>();
        PathNode currenPathNode = endNode;
        pathNodeList.Add(currenPathNode);

        while (currenPathNode.GetCameFromPathNode() != null)
        {
            pathNodeList.Add(currenPathNode.GetCameFromPathNode());
            currenPathNode = currenPathNode.GetCameFromPathNode();
        }

        pathNodeList.Reverse();

        List<GridPosition> gridPositionList = new List<GridPosition>();

        for (int i = 0; i < pathNodeList.Count; i++)
        {
            gridPositionList.Add(pathNodeList[i].GetGridPosition());
        }

        return gridPositionList;
    }

    public void SetIsWalkableGridPosition(GridPosition gridPosition, bool isWalkable)
    {
        PathNode pathNode = gridSystem.GetGridObject(gridPosition);
        pathNode.SetIsWalkable(isWalkable);
    }

    public bool IsWalkableGridPosition(GridPosition gridPosition)
    {
        PathNode pathNode = gridSystem.GetGridObject(gridPosition);
        return pathNode.GetIsWalkable();
    }

    public bool HasPath(GridPosition startGridPosition, GridPosition endGridPosition)
    {
        return FindPath(startGridPosition, endGridPosition, out int pathLength) != null;
    }

    public int GetPathLength(GridPosition startGridPosition, GridPosition endGridPosition)
    {
        FindPath(startGridPosition, endGridPosition, out int pathLength);
        return pathLength;
    }
}