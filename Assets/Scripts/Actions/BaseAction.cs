using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected Unit unit;
    protected bool isActive = false;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }
}