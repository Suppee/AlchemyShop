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
        Invoke("NewOrderAvailable", Random.Range(mintimetillnextcustomer, maxtimetillnextcustomer));
    }
    
    public override void Activate()
    {
        if(spillerref.heldObj == true && spillerref.heldObj.tag.Contains("Product"))
        {
            CheckItem(spillerref.heldObj);           
        }
        else if (neworderavailable == true && spillerref.heldObj == null)
        {
            //Debug.Log("Order Taget");
            StartNewOrder();
        }      
    }

    //Method to create a new order.
    public void NewOrderAvailable()
    {
        neworder = RoundManager.Instance.GenerateRandomOrder(2);
        neworder.OrderStation = this;
        neworderavailable = true;
        customercharacter.SetActive(true);
    }

    public void StartNewOrder()
    {
        neworderavailable = false;
        RoundManager.Instance.StartOrder(neworder);
        neworder = null;
    }

    public void Reset()
    {
        customercharacter.SetActive(false);
        StartCoroutine(KundePause());
    }

    IEnumerator KundePause()
    {
            yield return new WaitForSeconds(Random.Range(mintimetillnextcustomer, maxtimetillnextcustomer));
            Invoke("NewOrderAvailable", 0f);
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
            Order OrderToCheck = RoundManager.Instance.currentorders[o];

            for (int p = 0; p < OrderToCheck.products.Count; p++)
            {
                // Check if product is accepted
                if (item.GetComponent<PickUpObject>().itemRef.name.Equals(OrderToCheck.products[p].name) && !OrderToCheck.finishedproducts[p])
                {
                    // Product Accepted
                    RoundManager.Instance.FinishProduct(OrderToCheck, p);
                    AcceptPickUp();                    

                    // Check if order is finished
                    bool finishedall = true;
                    for (int f = 0; f < OrderToCheck.finishedproducts.Count; f++)
                    {                        
                        if (!OrderToCheck.finishedproducts[f]) finishedall = false;                   
                    }

                    // Run finish order
                    if (finishedall) RoundManager.Instance.FinishOrder(OrderToCheck);
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
