using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;

[System.Serializable]
public class StandarDifficultydValues
{
    public int minSpawnRate, maxSpawnRate, minBoltsLimit, maxBoltsLimit;
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
        _level.spawnRate = Random.Range(standVal.minSpawnRate + 5*difficulty, standVal.maxSpawnRate + 5*difficulty);
        _level.boltsLimit = Random.Range(standVal.minBoltsLimit - 2*difficulty, standVal.maxBoltsLimit - 3*difficulty);
        _level.speedDeduction = Random.Range(standVal.minSpeedDeduction + 10*difficulty, standVal.maxSpeedDeduction + 15*difficulty);

        SaveLoadSystem.SaveLevel(_level);
        return _level;
    }

    public void RunLevel(int levelNumber)
    {
        Debug.Log(levelNumber);
        Debug.Log(levels.Length);
        LoadLevelInfo.boltsLimit = levels[levelNumber].boltsLimit;
        LoadLevelInfo.spawnRate = levels[levelNumber].spawnRate;
        LoadLevelInfo.speedDeduction = levels[levelNumber].speedDeduction;
        SceneManager.LoadScene("Level");
    }
}
