using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // This sets up the game manager to be accessible by other scripts
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is Null!!!");

            return _instance;
        }
    }

    public GameObject victoryUIRef;
    public GameObject LoseUIRef;
    public GameObject HUDRef;
    public GameObject PauseMenuRef;
    public GameObject OrderUIRef;
    public GameObject ProductUIRef;


    private void Awake()
    {
        _instance = this;
        //GameManager.OnGameStateChanged += 
        
    }
    // Start is called before the first frame update
    void Start()
    {
        OrderUIRef = Resources.Load<GameObject>("Menus & UI/PF_OrderUI");
        ProductUIRef = Resources.Load<GameObject>("Menus & UI/PF_ProductUI");
    }

    public void HUD()
    {
        HUDRef = Instantiate(Resources.Load<GameObject>("Menus & UI/HUD"));
    }
    public void EndScreen()
    {
        LoseUIRef = Instantiate(Resources.Load<GameObject>("Menus & UI/Lose"));
    }
    public void PauseMenu()
    {
        PauseMenuRef = Instantiate(Resources.Load<GameObject>("Menus & UI/PauseMenu"));
    }

   
}
