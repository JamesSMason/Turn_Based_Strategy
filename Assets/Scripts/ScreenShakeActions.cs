using System;
using UnityEngine;

public class ScreenShakeActions : MonoBehaviour
{
    private void Awake()
    {
        ShootAction.OnAnyShootStart += ShootAction_OnAnyShootStart;
    }

    private void ShootAction_OnAnyShootStart(object sender, ShootAction.OnShootEventArgs e)
    {
        ScreenShake.Instance.Shake();
    }
}