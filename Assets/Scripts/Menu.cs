using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadScene(string scene)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
