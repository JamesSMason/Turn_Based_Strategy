using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private DestructibleCrate[] crates;
    bool hasExploded = false;

    private void Start()
    {
        crates = FindObjectsOfType<DestructibleCrate>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            if (hasExploded) { return; }
            hasExploded = true;
            foreach (DestructibleCrate crate in crates)
            {
                crate.Damage();
            }
        }
    }
}