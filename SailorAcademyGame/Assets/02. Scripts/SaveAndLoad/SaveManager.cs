using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static string SAVED_GAME = "savedGame";

    public static void SaveGame(SaveData data) {
        PlayerPrefs.SetString(SAVED_GAME, JsonUtility.ToJson(data));
    }

    public static SaveData LoadGame() {
        return JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(SAVED_GAME));
    }

    public static bool IsGameSaved() {
        return PlayerPrefs.HasKey(SAVED_GAME);
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
