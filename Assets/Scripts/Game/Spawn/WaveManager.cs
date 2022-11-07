using UnityEngine;
using static LevelScriptableObject;

[System.Serializable]
public class WaveManager : MonoBehaviour
{
    [Header("Spawn Places")]
    public GameObject[] m_spawnPlaces;
    public GameObject[] m_enemyPrefabs;

    public void CreateWave(Round round, int additionalMonster)
    {
        for (int index = 0; index < round.waveOnPoints.Length; index++)
        {
            CreateAvatar(m_spawnPlaces[index], round.waveOnPoints[index].avatarAmount + additionalMonster);
            CreateHealer(m_spawnPlaces[index], round.waveOnPoints[index].healerAmount + additionalMonster);
            CreateMeeleFighter(m_spawnPlaces[index], round.waveOnPoints[index].meeleFighterAmount + additionalMonster);
        }
    }

    public void CreateAvatar(GameObject parent, int count)
    {
        for (int index = 0; index < count; index++)
        {
            Instantiate(m_enemyPrefabs[0], parent.transform, false);
        }
    }

    public void CreateHealer(GameObject parent, int count)
    {
        for (int index = 0; index < count; index++)
        {
            Instantiate(m_enemyPrefabs[1], parent.transform, false);
        }
    }

    public void CreateMeeleFighter(GameObject parent, int count)
    {
        for (int index = 0; index < count; index++)
        {
            Instantiate(m_enemyPrefabs[2], parent.transform, false);
        }
    }
}
