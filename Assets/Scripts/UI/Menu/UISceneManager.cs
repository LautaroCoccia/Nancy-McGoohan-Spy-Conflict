using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UISceneManager : MonoBehaviour
{
    [SerializeField] string nextScene;
    public bool showStart = true;
    public void ChangeScene()
    {
        SceneManager.LoadScene(nextScene);
    }
    public void ChangeSceneAsync()
    {
        LoadAsync.nextScene = nextScene;
        LoadAsync.fromScene = SceneManager.GetActiveScene().name;
        LoadAsync.needCheckStart = showStart;
        SceneManager.LoadScene("Load",LoadSceneMode.Additive);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
