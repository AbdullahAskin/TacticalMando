using System;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "ScriptableObjects/LevelDatas", order = 1)]

public class LevelScriptableObject : ScriptableObject
{
    public Level level;

    #region Level structs
    [Serializable]
    public struct Level
    {
        public Round[] rounds;
    }

    [Serializable]
    public struct Round
    {
        public WaveOnPoint[] waveOnPoints;
    }


    [Serializable]
    public struct WaveOnPoint
    {
        public int pointIndex;
        public int meeleFighterAmount;
        public int healerAmount;
        public int avatarAmount;
    }
    #endregion
}
