using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool GameHasEnded = false; 
    public float restartDelay = 0;
    public int money;
    private Timer tid;
    public bool End = false;
    public int WinningMoney = 100;
    public GameObject victoryUI;
    public GameObject LoseUI;

    public void Update()
    {
        if(End == true)
        {
            EndGame();
        }

    }

    public void EndGame()
    { 
        if( GameHasEnded == false )
        {
            GameHasEnded = true;
            Debug.Log("EndGame");
            
            if(money >= WinningMoney)
            {
                victoryUI.SetActive(true); 
                Invoke("Restart", restartDelay);
            }
            else
            {
                LoseUI.SetActive(true);  
                Invoke("Restart", restartDelay);
            }

        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}

