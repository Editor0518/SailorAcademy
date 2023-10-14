using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static string SAVED_GAME = "savedGame";

    public static void SaveGame(SaveData data) {
        Debug.Log(JsonUtility.ToJson(data));
        PlayerPrefs.SetString(SAVED_GAME, JsonUtility.ToJson(data));
    }

    public static SaveData LoadGame() {
        SaveData data= JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(SAVED_GAME));

        return data;
    }

    public static bool IsGameSaved() {
        return PlayerPrefs.HasKey(SAVED_GAME);
    }

    public static void AddNewItemAndSave(string itemName) {
        SaveData data = LoadGame();
        data.items.Add(itemName);
        SaveGame(data);
    }

    public static void RemoveNewItemAndSave(string itemName)
    {
        SaveData data = LoadGame();
        if(data.items.IndexOf(itemName)!=-1) data.items.Remove(itemName);
        SaveGame(data);
    }


    public static void SaveChapter(string name, string value) {
        string data = PlayerPrefs.GetString(name);
        if (data != "") data += ",";
        data+=value;

        PlayerPrefs.SetString(name, data);
    }

    public static string[] ReturnChapterData(string name) {
        string[] data = PlayerPrefs.GetString(name).Split(",");
        Debug.Log(data[0]);
        return data;
    }

}
