using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("tutorial");
    }

    public void QuitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

}
