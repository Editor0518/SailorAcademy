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
        DefaultSave();
        PlayerPrefs.SetString("OneRecord", "");//���� �ʱ�ȭ
        LoadNamedScene(scenes[0]);
    }

    public void DefaultSave() {
        PlayerPrefs.SetString("DftSave", "0");
    }

    public void UpdateDefaultSave(int index) {
        PlayerPrefs.SetString("DftSave", index.ToString());
    }


    public void DefaultLoad() {
        LoadNamedScene(scenes[int.Parse(PlayerPrefs.GetString("DftSave", "0"))]);
    }

    string[] scenes = { "Prologue", "Chapter1", "Chapter2" };
    //���̺� �ε� â���� �ε��ϱ�
    public void ContinueFromLast(int index) {
        //string[] scene = PlayerPrefs.GetString("saveload", "-1,-2,-3,-4,-5,-6,-7,-8,-9,-10,-11,-12,-13,-14,-15,-16,-17,-18,-19,-20,-21,-22,-23,-24").Split(",");
        //if(int.Parse(scene[index])<0) LoadNamedScene(scenes[0]);//�ƹ��͵� �������� �ʾ����� ���ѷα�
        //else LoadNamedScene(scenes[int.Parse(scene[index])]);//else ����� �� ��ȣ ����
        LoadNamedScene(scenes[0]);
    }
    /*
    public void LoadMainMenu() {
        //SaveData data = new SaveData
        //{
        //    sentence = 0,
        //    prevScenes = null
        //};

        //SaveManager.SaveGame(data);
        SceneManager.LoadScene(0);
    }*/

    public void QuitGame() {
        Debug.Log("���� ����");
        Application.Quit();
    }
}
