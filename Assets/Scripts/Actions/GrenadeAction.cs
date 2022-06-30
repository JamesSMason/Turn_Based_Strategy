using System;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAction : BaseAction
{
    [SerializeField] private Transform grenadeProjectilePrefab = null;
    [SerializeField] private int maxThrowDistance = 7;
    //[SerializeField] private float offsetShoulderHeight = 1.7f;
    //[SerializeField] private LayerMask obstaclesLayerMask;

    private void Update()
    {
        if (!isActive)
        {
            return;
        }
    }

    public override string GetActionName()
    {
        return "Grenade";
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        EnemyAIAction enemyAIAction = new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 0
        };
        return enemyAIAction;
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPostion();

        for (int x = -maxThrowDistance; x <= maxThrowDistance; x++)
        {
            for (int z = -maxThrowDistance; z <= maxThrowDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > maxThrowDistance)
                {
                    continue;
                }

                // May need the code below if we want grenades to bounce of walls?

                //Vector3 unitWorldPosition = LevelGrid.Instance.GetWorldPosition(unitGridPosition);
                //Vector3 testWorldPosition = LevelGrid.Instance.GetWorldPosition(testGridPosition);
                //Vector3 shootDirection = (testWorldPosition - unitWorldPosition).normalized;

                //if (Physics.Raycast(unitWorldPosition + Vector3.up * offsetShoulderHeight,
                //                        shootDirection,
                //                        Vector3.Distance(unitWorldPosition, testWorldPosition),
                //                        obstaclesLayerMask
                //                        ))
                //{
                //    continue;
                //}

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        Transform grenadeProjectileTransform = Instantiate(grenadeProjectilePrefab, unit.GetWorldPosition(), Quaternion.identity);
        GrenadeProjectile grenadeProjectile = grenadeProjectileTransform.GetComponent<GrenadeProjectile>();
        grenadeProjectile.Setup(gridPosition, OnGrenadeBehaviourComplete);
        
        ActionStart(onActionComplete);
    }

    private void OnGrenadeBehaviourComplete()
    {
        ActionComplete();
    }
}