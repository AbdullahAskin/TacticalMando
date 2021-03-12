using UnityEngine;

public class TurretUpgrader : MonoBehaviour
{

    public void TurretUpgrade(ITurretUpgrade _turretUpgradeScript)
    {
        _turretUpgradeScript.UpgradeTurret();
    }
}
