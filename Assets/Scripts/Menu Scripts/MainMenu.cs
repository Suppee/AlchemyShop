using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.Instance.StartRound(GameMode.Duel, "Level 0");
    }

    public void Tutorial()
    {
        GameManager.Instance.StartRound(GameMode.Tutorial, "Tutorial Level");
    }

    public void QuitGame()
    {
        GameManager.Instance.EndGame();
    }

}
