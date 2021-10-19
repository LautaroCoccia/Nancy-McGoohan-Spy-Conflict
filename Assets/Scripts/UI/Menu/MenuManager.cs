using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] string nextScene;
    public void ChangeScene()
    {
        SceneManager.LoadScene(nextScene);
    }
    public void ChangeSceneAsync()
    {
        LoadAsync.nextScene = nextScene;
        LoadAsync.fromScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Load",LoadSceneMode.Additive);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
