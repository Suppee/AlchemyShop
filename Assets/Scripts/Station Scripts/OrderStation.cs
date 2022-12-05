using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class OrderStation : BaseStation
{
    public Order neworder;
    public bool neworderavailable = false;
    public float mintimetillnextcustomer;
    public float maxtimetillnextcustomer;
    public GameObject customercharacter;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("NewOrder", Random.Range(mintimetillnextcustomer, maxtimetillnextcustomer));
    }
    
    public override void Activate()
    {
        if(spillerref.holdingObj == true && spillerref.heldObj.tag.Contains("Product"))
        {
            CheckItem(spillerref.heldObj);           
        }
        else if (neworderavailable == true && spillerref.holdingObj == false)
        {
            //Debug.Log("Order Taget");
            StartNewOrder();
        }      
    }

    //Method to create a new order.
    public void NewOrder()
    {
        neworder = RoundManager.Instance.GenerateRandomOrder(2);
        neworderavailable = true;
        customercharacter.SetActive(true);
    }

    public void StartNewOrder()
    {
        neworderavailable = false;
        RoundManager.Instance.StartOrder(neworder);        
    }

    IEnumerator KundePause()
    {
            yield return new WaitForSeconds(Random.Range(mintimetillnextcustomer, maxtimetillnextcustomer));
            Invoke("NewOrder", 0f);
           // Debug.Log("CUSTOMER");
    }

    // Throw box product check
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Contains("PickUp") && other.gameObject.transform.parent == null)
        {
            CheckItem(other.gameObject);
        }
        
    }

    public void CheckItem(GameObject item)
    {
        for (int o = 0; o < RoundManager.Instance.currentorders.Count; o++)
        {
            List<ProductRecipe> CurrentOrder = RoundManager.Instance.currentorders[o].products;

            for (int p = 0; p < CurrentOrder.Count; p++)
            {
                // Check if product is accepted
                if (item.GetComponent<ItemInfo>().itemRef.name.Equals(CurrentOrder[p].name) && !RoundManager.Instance.currentorders[o].finishedproducts[p])
                {
                    // Product Accepted
                    RoundManager.Instance.FinishProduct(RoundManager.Instance.currentorders[o], p);
                    AcceptPickUp();                    

                    // Check if order is finished
                    bool finishedall = true;
                    bool finished = false;
                    for (int f = 1; f < neworder.finishedproducts.Count; f++)
                    {
                        
                        if (neworder.finishedproducts[f]) finished = true;
                        else
                        {
                            finished = false;
                            if (!finished) finishedall = false;

                        }
                       
                    }

                    // Run finish order
                    if (finishedall)
                    {
                        RoundManager.Instance.FinishOrder(RoundManager.Instance.currentorders[o]);
                        neworderavailable = true;
                        KundePause();                        
                    }
                    return;
                }
                else
                {
                    //Debug.Log("Produkt ikke godkendt");                    
                }                   
            }
        }
    }
}
