using System.Collections.Generic;

public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;

    private List<Unit> unitList;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        unitList = new List<Unit>();
    }

    public override string ToString()
    {
        string units = "\n";
        foreach (Unit unit in unitList)
        {
            units += unit.ToString() + "\n";
        }
        return gridPosition.ToString() + units;
    }

    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }
}