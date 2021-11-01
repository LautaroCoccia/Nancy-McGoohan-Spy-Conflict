using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadAsync : MonoBehaviour
{
    public Slider slider;
    public GameObject bar;
    public static string nextScene;
    public static string fromScene;
    public float waitToAppearButton;
    public float waitToAppearBar;
    bool buttonInput;
    public GameObject button;
    const float barProgressFix = 0.9f;
    public void StartNextScene()
    {
        buttonInput = true;
    }
    public IEnumerator Start()
    {
        Time.timeScale = 1.0f;
        yield return SceneManager.UnloadSceneAsync(fromScene);
        buttonInput = false;
        slider.value = 0.0f;
        bar.SetActive(true);
        yield return new WaitForSeconds(waitToAppearBar);
        Debug.Log("nextScene");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene);
        asyncLoad.allowSceneActivation = false;
        while (asyncLoad.progress < barProgressFix)
        {
            Debug.Log(asyncLoad.progress);
            slider.value = asyncLoad.progress;
            yield return null;
        }
        slider.value = 1.0f;
        yield return new WaitForSeconds(waitToAppearButton);
        Debug.Log("bar false");
        bar.SetActive(false);
        button.SetActive(true);
        yield return null;
        yield return new WaitUntil(() =>buttonInput);
        asyncLoad.allowSceneActivation = true;
    }
}
