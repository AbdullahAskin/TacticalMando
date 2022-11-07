using System.Collections.Generic;
using UnityEngine;

public class
    CommonData : MonoBehaviour
{
    public static List<GameObject> turrets = new List<GameObject>();
    public static List<Transform> enemiesPosition = null;


    public void OnEnable()
    {
        turrets = null;
        enemiesPosition = null;
    }

    public static void AddEnemy(Transform _transform)
    {
        if (enemiesPosition == null)
            enemiesPosition = new List<Transform>();
        enemiesPosition.Add(_transform);
    }

    public static void RemoveEnemy(Transform _transform)
    {
        enemiesPosition.Remove(_transform);
    }

    public static void AddTurret(GameObject _turret)
    {
        if (turrets == null)
            turrets = new List<GameObject>();
        turrets.Add(_turret);
    }

    public static void RemoveTurret(GameObject _turret)
    {
        turrets.Remove(_turret);
    }


    public static List<Transform> GetEnemiesPosition()
    {
        if (enemiesPosition == null)
            enemiesPosition = new List<Transform>();
        return enemiesPosition;
    }

    public static List<GameObject> GetTurrets()
    {
        if (turrets == null)
            turrets = new List<GameObject>();
        return turrets;
    }

}
