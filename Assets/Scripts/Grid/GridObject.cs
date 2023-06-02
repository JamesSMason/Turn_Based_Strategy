using System.Collections.Generic;

public class GridObject
{
    private GridSystemHex<GridObject> gridSystem;
    private GridPosition gridPosition;
    private IInteractable interactable;

    private List<Unit> unitList;

    public GridObject(GridSystemHex<GridObject> gridSystem, GridPosition gridPosition)
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

    public bool HasAnyUnit()
    {
        return unitList.Count > 0;
    }

    public Unit GetUnit()
    {
        if (HasAnyUnit())
        {
            return GetUnitList()[0];
        }
        else
        {
            return null;
        }
    }

    public IInteractable GetInteractable()
    {
        return interactable;
    }

    public void SetInteractable(IInteractable interactable)
    {
        this.interactable = interactable;
    }
}