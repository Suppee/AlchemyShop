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
        victoryUIRef = Resources.Load<GameObject>("Prefabs/Menus/Victory");
        LoseUIRef = Resources.Load<GameObject>("Prefabs/Menus/Lose");
        HUDRef = Resources.Load<GameObject>("Prefabs/Menus/HUD");
        PauseMenuRef = Resources.Load<GameObject>("Prefabs/Menus/PauseMenu");
        OrderUIRef = Resources.Load<GameObject>("Prefabs/UI/PF_OrderUI");
        ProductUIRef = Resources.Load<GameObject>("Prefabs/UI/PF_ProductUI");
    }

    public void HUD()
    {
        HUDRef = Instantiate(HUDRef);
    }
    public void EndScreen()
    {
        LoseUIRef = Instantiate(LoseUIRef);
    }
    public void PauseMenu()
    {
        PauseMenuRef = Instantiate(PauseMenuRef);
    }

   
}
