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
    public void Quit()
    {
        Application.Quit();
    }
}
