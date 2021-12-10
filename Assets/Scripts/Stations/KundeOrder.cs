using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class KundeOrder : MasterStation
{
    //[HideInInspector]
    public ProductRecipe[] fullproductlist;
    [HideInInspector]
    public Order neworder;
    public int moneyEarnedPerOrder = 10;
    bool neworderavailable = false;
    public float mintimetillnextcustomer;
    public float maxtimetillnextcustomer;
    public GameObject orderPrefab;    
    public GameObject customercharacter;


    // Start is called before the first frame update
    void Start()
    {
        fullproductlist = Resources.LoadAll("Recipes", typeof(ProductRecipe)).Cast<ProductRecipe>().ToArray();
        Invoke("SkabNyOrdre", Random.Range(mintimetillnextcustomer, maxtimetillnextcustomer));
    }
    
    public override void Activate()
    {
        if(spillerref.holderObjekt == true && spillerref.objekthold.tag.Contains("Product"))
        {
            CheckItem(spillerref.objekthold);           
        }
        else if (neworderavailable == true && spillerref.holderObjekt == false)
        {
            print("Order Taget");
            StartNewOrder();
        }      
    }

    //Method to create a new order.
    public void SkabNyOrdre()
    {
        neworder = new Order();
        neworder.products = new List<ProductRecipe>();
        neworder.finishedproducts = new List<bool>();
        GameManager.Instance.currentorders.Add(neworder);

        //Find order size by choosing a random number between 1 as the minimum order size and the largest order size + 1 because of the loop following.
        int orderstroelse = Random.Range(1,4);

        //Loops an amount of time equal to the order size, then adds one random recipe to the order for each time.
        for(int i = 0; i < orderstroelse; i++)
        {
            //Finds a random recipe in the list of all recipes by choosing a random number between 0 and the number of recipes in the list of all recipes. This number serves as the indes in a search.
            int index = Random.Range(0, fullproductlist.Length);

            //Add the recipe at the given index from before into the list of recipes in the current active order, which stores the current order.            
            neworder.products.Add(fullproductlist[index]);
            neworder.finishedproducts.Add(false);


        }
        neworderavailable = true;
        customercharacter.SetActive(true);
    }

    public void StartNewOrder()
    {
        neworderavailable = false;
        GameObject orderUI = Resources.Load<GameObject>("Prefabs/PF_OrderUI");
        GameObject Currentorder = Instantiate(orderUI, GameManager.Instance.gameHUDRef.transform.GetChild(0), true);
        neworder.orderUI = Currentorder.GetComponent<OrderSetupScript>();
        Currentorder.GetComponent<OrderSetupScript>().order = neworder;
        Currentorder.GetComponent<OrderSetupScript>().Initiate();
    }

    IEnumerator KundePause()
    {
            yield return new WaitForSeconds(Random.Range(mintimetillnextcustomer, maxtimetillnextcustomer));
            Invoke("SkabNyOrdre", 0f);
           // Debug.Log("CUSTOMER");
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Contains("PickUp") && other.gameObject.transform.parent == null)
        {
            CheckItem(other.gameObject);
        }
        
    }

    public void CheckItem(GameObject item)
    {
        for (int o = 0; o < GameManager.Instance.currentorders.Count; o++)
        {
            List<ProductRecipe> CurrentOrder = GameManager.Instance.currentorders[o].products;

            for (int p = 0; p < CurrentOrder.Count; p++)
            {
                if (item.GetComponent<ItemInfo>().itemRef.name.Equals(CurrentOrder[p].name) && !neworder.finishedproducts[p])
                {
                    // Debug.Log("Produkt godkendt");
                    GameManager.Instance.gameHUDRef.GetComponent<Penge>().gold += moneyEarnedPerOrder;
                    Destroy();

                    // Ret UI
                    neworder.orderUI.ProductComplete(p);

                    // Check om orderen er færdig
                    if (CurrentOrder.Count == 0)
                    {
                        neworderavailable = true;
                        KundePause();
                        GameManager.Instance.gameHUDRef.GetComponent<Penge>().gold += moneyEarnedPerOrder;
                        neworder.orderUI.DeleteOrder();                        
                    }
                    return;
                }
                else
                {
                    Debug.Log("Produkt ikke godkendt");                    
                }                   
            }
        }
    }
}
