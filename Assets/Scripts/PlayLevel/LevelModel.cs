using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public static class LevelModel
{

    public static int spawnRate {get; private set;}
    public static int fireRate {get; private set;}
    public static float speedDeduction {get; private set;}

    public static bool LevelPassed = false;

    public static ReactiveProperty<int> health = new ReactiveProperty<int>(3);

    public static void PassLevelValues(int _fireRate, int _spawnRate, float _speedDeduction)
    {
        spawnRate = _spawnRate;
        fireRate = _fireRate;
        speedDeduction = _speedDeduction;
    }

}
