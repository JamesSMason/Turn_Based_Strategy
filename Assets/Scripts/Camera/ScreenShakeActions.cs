using System;
using UnityEngine;

public class ScreenShakeActions : MonoBehaviour
{
    private void Awake()
    {
        ShootAction.OnAnyShootStart += ShootAction_OnAnyShootStart;
        GrenadeProjectile.OnAnyGrenadeExploded += GrenadeProjectile_OnAnyGrenadeExploded;
    }

    private void GrenadeProjectile_OnAnyGrenadeExploded(object sender, EventArgs e)
    {
        ScreenShake.Instance.Shake(5);
    }

    private void ShootAction_OnAnyShootStart(object sender, ShootAction.OnShootEventArgs e)
    {
        ScreenShake.Instance.Shake();
    }
}