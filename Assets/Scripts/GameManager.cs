using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

    
public class GameManager : MonoBehaviour
{
    // This sets up the game manager to be accessible by other scripts
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is Null!!!");

            return _instance;
        }
    }

    bool GameHasEnded = false; 
    public float restartDelay = 0;
    public int money;
    private Timer tid;
    public bool End = false;
    public int WinningMoney = 100;
    public GameObject victoryUI;
    public GameObject LoseUI;
    public List<Order> currentorders;
    public GameObject gameHUDRef;

    private void Awake()
    {
        _instance = this;
    }

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

