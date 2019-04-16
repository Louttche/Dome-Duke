using System.Collections;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateDayList
{
    [MenuItem("Assets/Create/Day List")]
    public static DayList  Create()
    {
        // Get file from local files
        string path = Path.Combine(Application.streamingAssetsPath, "days.json");
        // Read the file and get the JSON
        string json = File.ReadAllText(path);
        DayList asset = JsonUtility.FromJson<DayList>(json);
        //DayList asset = ScriptableObject.CreateInstance<DayList>();

        AssetDatabase.CreateAsset(asset, "Assets/DayList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
