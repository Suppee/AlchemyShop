using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class KundeOrder : MasterStation
{
    //[HideInInspector]
    public Recipes[] fullproductlist;
    [HideInInspector]
    public List<Recipes> neworder;
    public int moneyEarnedPerOrder = 10;
    bool neworderavailable;
    public float mintimetillnextcustomer;
    public float maxtimetillnextcustomer;
    public GameObject orderPrefab;    
    public GameObject customercharacter;
    GameObject currentOrdersCanvas;


    // Start is called before the first frame update
    void Start()
    {
        currentOrdersCanvas = GameObject.Find("CurrentOrdersUI").gameObject;
        fullproductlist = Resources.LoadAll("Recipes", typeof(Recipes)).Cast<Recipes>().ToArray();
        StartCoroutine(KundePause());
    }
    
    public override void Activate()
    {
        if(spillerref.holderObjekt == true && spillerref.objekthold.tag.Contains("Product"))
        {
            for (int o = 0; o < currentOrdersCanvas.transform.childCount; o++)
            {
                List<Recipes> CurrentOrder = currentOrdersCanvas.transform.GetChild(o).gameObject.GetComponent<OrderSetupScript>().order;                

                for (int p = 0; p < CurrentOrder.Count; p++)
                {
                    if (spillerref.objekthold.GetComponent<ProductInfo>().Opskrift.name.Equals(CurrentOrder[p].name))
                    {
                        Debug.Log("Produkt godkendt");                        
                        GameObject.Find("Ur_Penge").GetComponent<Penge>().gold += moneyEarnedPerOrder;
                        Destroy();
                        //Ret UI               
                        currentOrdersCanvas.transform.GetChild(o).gameObject.transform.GetChild(2).gameObject.transform.GetChild(p).GetComponent<UIRecipeInfo>().productfinishedscreen.SetActive(true);
                        CurrentOrder.RemoveAt(p);

                        //Check om orderen er færdig
                        if (CurrentOrder.Count == 0)
                        {
                            KundePause();
                            GameObject.Find("Ur_Penge").GetComponent<Penge>().gold += moneyEarnedPerOrder;
                            currentOrdersCanvas.transform.GetChild(o).gameObject.GetComponent<OrderSetupScript>().DeleteOrder();
                        }
                        return;
                    }
                    else
                        Debug.Log("Produkt ikke godkendt");
                }                
            }
        }
        else if (neworderavailable == true && spillerref.holderObjekt == false)
        {
            print("Order Taget");
            StartNewOrder();
            neworderavailable = false;
            customercharacter.SetActive(false);
        }
      
    }

    //Method to create a new order.
    public void SkabNyOrdre()
    {
        neworder.Clear();

        //Find order size by choosing a random number between 1 as the minimum order size and the largest order size + 1 because of the loop following.
        int orderstroelse = Random.Range(1,4);

        //Loops an amount of time equal to the order size, then adds one random recipe to the order for each time.
        for(int i = 0; i < orderstroelse; i++)
        {
            //Finds a random recipe in the list of all recipes by choosing a random number between 0 and the number of recipes in the list of all recipes. This number serves as the indes in a search.
            int index = Random.Range(0, fullproductlist.Length);

            //Add the recipe at the given index from before into the list of recipes in the current active order, which stores the current order.
            neworder.Add(fullproductlist[index]);
           
        }
        neworderavailable = true;
        customercharacter.SetActive(true);
    }

    public void StartNewOrder()
    {
        GameObject Currentorder = Instantiate(orderPrefab, currentOrdersCanvas.transform, true);
        Currentorder.GetComponent<OrderSetupScript>().order = neworder;
        Currentorder.GetComponent<OrderSetupScript>().Initiate();
    }

    IEnumerator KundePause()
    {
        while (true)
        {
            Invoke("SkabNyOrdre", 0f);
           // Debug.Log("CUSTOMER");
            yield return new WaitForSeconds(Random.Range(mintimetillnextcustomer, maxtimetillnextcustomer));
        }
    }    
}
