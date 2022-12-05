using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Gamemodes
public enum GameMode { Coop, Duel, Tutorial }

public class RoundManager : MonoBehaviour
{
    // This sets up the round manager to be accessible by other scripts
    private static RoundManager _instance;
    public static RoundManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is Null!!!");

            return _instance;
        }
    }

    public GameMode gameMode;

    // Round Timer
    public float roundTime = 90;
    public int startCountdown = 3;

    // Point Score
    public int money;
    public int WinningMoney = 100;
    public int moneyEarnedPerOrder = 10;

    // Orders
    public List<Order> currentorders;
    // Endscreen & Restarting
    public float restartDelay = 0;    
    

    private void Awake()
    {
        _instance = this;
    }

    // Round Start Methods
    public void StartRound(GameMode gamemode, string LevelName)
    {
        gameMode = gamemode;
        StartCoroutine(StartingRound(LevelName));        
    }
    IEnumerator StartingRound(string LevelName)
    {
        yield return new WaitWhile(() =>SceneManager.GetActiveScene().name == LevelName);
        // SPAWN PLAYERS

        // Spawn HUD
        UIManager.Instance.HUD();

        // Start intro Countdown
        yield return StartCoroutine(StartCountdown());

        // Set Game State to playing (allows player control)
        GameManager.Instance.UpdateGameState(GameState.Playing);

        // Start Round Timer
        yield return StartCoroutine(RoundTimer());
    }
    IEnumerator StartCountdown()
    {
        while (startCountdown > 0)
        {
            startCountdown--;
            Debug.Log(startCountdown);
            yield return new WaitForSeconds(1);
        }
        startCountdown = 0;

    }
    IEnumerator RoundTimer()
    {
        while(roundTime > 0)
        {
            roundTime--;
            yield return new WaitForSeconds(1);
        }
        roundTime = 0;
        EndRound();
        
        yield return null;
    }
    
    // Order Methods
    public Order GenerateRandomOrder(int maxSize)
    {
        ProductRecipe[] fullproductlist = Resources.LoadAll("Recipes", typeof(ProductRecipe)).Cast<ProductRecipe>().ToArray();
        Order neworder = new Order();
        neworder.products = new List<ProductRecipe>();
        neworder.finishedproducts = new List<bool>();

        //Find order size by choosing a random number between 1 as the minimum order size and the largest order size + 1 because of the loop following.
        int orderSize = Random.Range(1, maxSize);

        //Loops an amount of time equal to the order size, then adds one random recipe to the order for each time.
        for (int i = 0; i < orderSize; i++)
        {
            //Finds a random recipe in the list of all recipes by choosing a random number between 0 and the number of recipes in the list of all recipes. This number serves as the indes in a search.
            int index = Random.Range(0, fullproductlist.Length);

            //Add the recipe at the given index from before into the list of recipes in the current active order, which stores the current order.            
            neworder.products.Add(fullproductlist[index]);
            neworder.finishedproducts.Add(false);
        }
        return neworder;
    }
    public void StartOrder(Order newOrder)
    {
        // Add order to current orders list
        currentorders.Add(newOrder);

        // Set order Time
        newOrder.orderTime = 30 * newOrder.products.Count;

        // Add Order UI Element to HUD
        UIManager.Instance.HUDRef.GetComponent<HUDController>().StartOrderUI(newOrder);
        
        // Start Order Timer
        StartCoroutine(OrderTimer(newOrder));
    }
    IEnumerator OrderTimer(Order order)
    {
        
        while (order.orderTime > 0)
        {
            Debug.Log(order.orderTime);
            order.orderTime -= Time.deltaTime;
        }
        order.orderTime = 0;
        FinishOrder(order);

        yield return null;
    }
    public void FinishProduct(Order order, int productIndex)
    {
        order.finishedproducts[productIndex] = true;
        order.orderUI.ProductComplete(productIndex);
        ScorePoints();
    }
    public void FinishOrder(Order order)
    {
        Destroy(order.orderUI);
        ScorePoints();
    }
    public void ScorePoints()
    {
        RoundManager.Instance.money += moneyEarnedPerOrder;
    }

    // Round End Methods
    public void EndRound()
    {
        StartCoroutine(EndingRound());
    }
    IEnumerator EndingRound()
    {
        switch(gameMode)
        {
            case GameMode.Duel:
                break;

            case GameMode.Tutorial:
                break;

            case GameMode.Coop:
                if (RoundManager.Instance.money >= WinningMoney)
                {
                    UIManager.Instance.victoryUIRef.SetActive(true);
                    Invoke("RestartRound", restartDelay);
                }
                else
                {
                    UIManager.Instance.LoseUIRef.SetActive(true);
                    Invoke("RestartRound", restartDelay);
                }
                break;

            default:
                break;
        }


        return null;
    }

    private void RestartRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartRound(gameMode, SceneManager.GetActiveScene().name);
    }
}