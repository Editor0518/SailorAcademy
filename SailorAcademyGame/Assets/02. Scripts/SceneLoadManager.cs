using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public GameObject blackFade;

    public void LoadNamedScene(string name) {
        SceneManager.LoadScene(name);
    }

    public void LoadFromFirst() {
        if (blackFade != null) blackFade.SetActive(true);
        Invoke("Prologue", 1f);

    }

    void Prologue() {
        LoadingSceneManager.LoadScene("Prologue");

    }

    public void LoadMainMenu() {
        SaveData data = new SaveData
        {
            sentence = 0,
            prevScenes = null
        };

        SaveManager.SaveGame(data);
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
