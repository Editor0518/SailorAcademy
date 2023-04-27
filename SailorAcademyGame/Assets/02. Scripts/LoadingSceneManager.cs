using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] Image progressBar;

    private void Start() {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName) {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    IEnumerator LoadScene() {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone) {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f) {
                progressBar.color = new Color(progressBar.color.r, progressBar.color.g, progressBar.color.b, Mathf.Lerp(progressBar.color.a, op.progress, timer));
                //progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.color.a >= op.progress) {//if (progressBar.fillAmount >= op.progress) {

                    timer = 0f;
                }
            }
            else {
                progressBar.color = new Color(progressBar.color.r, progressBar.color.g, progressBar.color.b, Mathf.Lerp(progressBar.color.a, 1f, timer));//progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.color.a == 1.0f) {//if (progressBar.fillAmount == 1.0f) {//if(op.progress==1.0f){
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
