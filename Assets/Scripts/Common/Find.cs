using System.Collections.Generic;
using UnityEngine;

public class Find
{

    public static Transform FindNearestEnemy(Transform currentPosition, float distance)
    {
        List<Transform> enemiesPosition = CommonData.GetEnemiesPosition();
        Transform currentNearestEnemy = null;
        float minDistance = float.MaxValue;

        foreach (var currentEnemy in enemiesPosition)
        {
            if (GameObject.ReferenceEquals(currentPosition, currentEnemy))// Nesneler aynimi diye bakar.
                continue;

            float currentDistance = Vector2.Distance(currentPosition.position, currentEnemy.position);
            if (minDistance > currentDistance && distance >= currentDistance)
            {
                minDistance = currentDistance;
                currentNearestEnemy = currentEnemy;
            }
        }

        if (enemiesPosition.Count == 0 || minDistance == float.MaxValue)
        {
            return null;
        }
        return currentNearestEnemy;
    }

    public static Transform FindAppropriateTargetForHealer(Transform currentPosition, float distance) // Cani 100 den az olan ve healer olmayan dusman varsa once onu bulucak
    {

        List<Transform> enemiesPosition = CommonData.GetEnemiesPosition();
        Transform currentNearestEnemy = null;

        //float minHp = float.MaxValue;
        float minDistance = float.MaxValue;


        foreach (var currentEnemy in enemiesPosition)
        {
            if ((GameObject.ReferenceEquals(currentPosition, currentEnemy) || currentEnemy.gameObject.layer == 18) || !currentEnemy.gameObject.activeSelf)// gameobject kendisimi ,healer mi ,aktif mi kontrolu yapilir.
                continue;

            // float currentHp = currentEnemy.GetComponent<EnemyProperty>().GetHp(); hp kontrolu daha sonra yapilicak.
            float currentDistance = Vector2.Distance(currentPosition.position, currentEnemy.position);

            if (minDistance > currentDistance && distance >= currentDistance)
            {
                minDistance = currentDistance;
                currentNearestEnemy = currentEnemy;
            }
        }

        return currentNearestEnemy;
    }


}
