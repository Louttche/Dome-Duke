using System.Collections;
using UnityEngine;
using UnityEditor;

public class CreateDayList
{
    [MenuItem("Assets/Create/Day List")]
    public static DayList  Create()
    {
        DayList asset = ScriptableObject.CreateInstance<DayList>();

        AssetDatabase.CreateAsset(asset, "Assets/DayList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
