using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    List<Unit> unitList;
    List<Unit> friendlyUnitList;
    List<Unit> enemyUnitList;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log($"There are multiple UnitManager objects in this scene.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        unitList = new List<Unit>();
        friendlyUnitList = new List<Unit>();
        enemyUnitList = new List<Unit>();
    }

    private void Start()
    {
        Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
    }

    private void Unit_OnAnyUnitSpawned(object sender, EventArgs e)
    {
        Unit selectedUnit = sender as Unit;

        unitList.Add(selectedUnit);

        if (selectedUnit.IsEnemy())
        {
            enemyUnitList.Add(selectedUnit);
        }
        else
        {
            friendlyUnitList.Add(selectedUnit);
        }
    }

    private void Unit_OnAnyUnitDead(object sender, EventArgs e)
    {
        Unit selectedUnit = sender as Unit;

        unitList.Remove(selectedUnit);

        if (selectedUnit.IsEnemy())
        {
            enemyUnitList.Remove(selectedUnit);
        }
        else
        {
            friendlyUnitList.Remove(selectedUnit);
        }
    }

    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    public List<Unit> GetEnemyUnitList()
    {
        return enemyUnitList;
    }

    public List<Unit> GetFriendlyUnitList()
    {
        return friendlyUnitList;
    }
}