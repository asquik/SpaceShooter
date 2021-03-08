using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadSystem
{
    public static void SaveLevel(LevelInfo _level)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = PathName(_level.gameObject.name);
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData levelData = new LevelData(_level);

        formatter.Serialize(stream, levelData);
        stream.Close();
    }

    private static string PathName(string _levelName)
    {
        return Application.persistentDataPath + "/" + _levelName + ".info";
    }

    public static LevelData LoadData(string _levelName)
    {
        string path = PathName(_levelName);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData ;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("File is not found");
            return null;
        }
        
    }

    public static bool SavedFileExists(string levelName)
    {
        string path = PathName(levelName);
        return (File.Exists(path));
        
    }

    public static LevelInfo LoadLevel(LevelInfo level)
    {
        if (SaveLoadSystem.SavedFileExists(level.gameObject.name))
        {
            LevelData levelData = LoadData(level.gameObject.name);

            level.isCreated = levelData.isCreated;
            level.isPassed = levelData.isPassed;
            level.spawnRate = levelData.spawnRate;
            level.boltsLimit = levelData.boltsLimit;
            level.speedDeduction = levelData.speedDeduction;
        }
        return level;
    }

}
