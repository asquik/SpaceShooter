using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;

[System.Serializable]
public class StandarDifficultydValues
{
    public int minSpawnRate, maxSpawnRate, minFireRate, maxFireRate;
    public float minSpeedDeduction, maxSpeedDeduction;
}
public class LevelController : MonoBehaviour
{
    public LevelInfo[] levels;
    public Button[] playButtons;
    public StandarDifficultydValues standVal;

    void Start()
    {
        foreach (var button in playButtons)
        {
            button.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    RunLevel(System.Array.IndexOf(playButtons, button));
                }).AddTo(this);
        }

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i] = SaveLoadSystem.LoadLevel(levels[i]);
        }
        CheckLevelModel();

        if (!levels[0].isCreated)
        {
            levels[0] = CreateLevel(levels[0], 0);
        }
        else
        {
            for (int i = 1; i < levels.Length; i++)
            {
                
                if (!levels[i].isCreated && levels[i - 1].isPassed)
                {
                    levels[i] = CreateLevel(levels[i], i);
                }
            }
        }


    }


    private LevelInfo CreateLevel(LevelInfo _level, int difficulty)
    {
        _level.isCreated = true;
        _level.isPassed = false;
        _level.spawnRate = Random.Range(standVal.minSpawnRate - 200*difficulty,
                                        standVal.maxSpawnRate - 150*difficulty);

        _level.fireRate = Random.Range(standVal.minFireRate + 60*difficulty,
                                       standVal.maxFireRate + 80*difficulty);

        _level.speedDeduction = Random.Range(standVal.minSpeedDeduction + 10*difficulty,
                                             standVal.maxSpeedDeduction + 15*difficulty);

        SaveLoadSystem.SaveLevel(_level);
        return _level;
    }

    public void RunLevel(int levelNumber)
    { 

        LevelModel.PassLevelValues(
            levels[levelNumber].fireRate,
            levels[levelNumber].spawnRate,
            levels[levelNumber].speedDeduction
            );

        SceneManager.LoadScene("Level");
    }

    private void CheckLevelModel()
    {
        if (LevelModel.LevelPassed)
        {
            foreach (var level in levels)
            {
                if (!level.isPassed)
                {
                    level.isPassed = LevelModel.LevelPassed;
                    LevelModel.LevelPassed = false;
                    SaveLoadSystem.SaveLevel(level);
                    return;
                }
            }
        }    
    }
}
