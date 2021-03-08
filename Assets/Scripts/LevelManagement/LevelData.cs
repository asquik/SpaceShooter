using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public bool isCreated = false;
    public bool isPassed = false;
    public int spawnRate;
    public int boltsLimit;
    public float speedDeduction;

    public LevelData(LevelInfo level)
    {
        isCreated = level.isCreated;
        isPassed = level.isPassed;
        spawnRate = level.spawnRate;
        boltsLimit = level.boltsLimit;
        speedDeduction = level.speedDeduction;
    }
}
