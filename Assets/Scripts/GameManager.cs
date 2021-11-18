using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool GameHasEnded = false; 
    public float restartDelay = 1f;
    public int monney;
    private Timer tid;
    public bool End = false;
    public int WinningMonney = 100;

    public void Update()
    {
        if(End == true)
        {
            Invoke("EndGame",restartDelay);
        }

    }

    public void EndGame()
    { 
        if( GameHasEnded == false )
        {
            GameHasEnded = true;
            Debug.Log("EndGame");
            
            if(monney >= WinningMonney)
            {
                Debug.Log("win");
                
                Invoke("Restart", restartDelay);
            }
            else
            {
                Debug.Log("Lose");
                Invoke("Restart", restartDelay);
            }

        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

