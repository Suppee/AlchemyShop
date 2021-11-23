using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KundeOrder : MasterStation
{    
    public List<Recipes> opskriftListe;    
    public List<Recipes> neworder;
    public float mintid;
    public float maxtid;
    public int PengeOrder = 10;
    public GameObject CanvasPrefab;
    public GameObject sliderref;
    public float mintimetillnextcustomer;
    public float maxtimetillnextcustomer;
    public GameObject OrderCanvas;
    public GameObject customer;
    bool neworderavailable;
    
    // Start is called before the first frame update
    void Start()
    {

        //Find alle opskrifter
        string[] opskriftlokationer = AssetDatabase.FindAssets("t:scriptableobject", new[] { "Assets/Scripts/Recipes" });
        opskriftListe.Clear();
        foreach (string opskriftstreng in opskriftlokationer)
        {
            var opskriftSti = AssetDatabase.GUIDToAssetPath(opskriftstreng);
            var opskrift = AssetDatabase.LoadAssetAtPath<Recipes>(opskriftSti);
            opskriftListe.Add(opskrift);
        }
        this.GetComponent<MeshRenderer>().material.color = Color.red;
        Invoke("SkabNyOrdre", 3);
    }
    
    public override void Activate()
    {
        if(spillerref.holderObjekt == true)
        {
            for (int i = 0; i < neworder.Count; i++)
            { 
                if (spillerref.objekthold.GetComponent<ProductInfo>().Opskrift.name.Equals(neworder[i].name))
                {
                    neworder.RemoveAt(i);                    
                    Debug.Log("Produkt godkendt");                  
                    Destroy();
                    //Ret UI               
                    Destroy(this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(i+1).gameObject);

                    //Check om orderen er færdig
                    if (neworder.Count == 0)                    
                    {                        
                        this.GetComponent<MeshRenderer>().material.color = Color.red;                        
                        KundePause();
                        GameObject.Find("Ur_Penge").GetComponent<Penge>().gold += PengeOrder;
                       
                    }                        
                    return;
                }
                else
                    Debug.Log("Produkt ikke godkendt");
            }
        }
        else if (neworderavailable == true && spillerref.holderObjekt == false)
        {
            print("Tag en order");
            StartNewOrder();
        }
      
    }

    //Method to create a new order.
    public void SkabNyOrdre()
    {
        customer.SetActive(true);

        for (int i = 0; i < neworder.Count; i++)
        {
            Destroy(this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(i + 1).gameObject);
        }
        neworder.Clear();

        //Find order size by choosing a random number between 1 as the minimum order size and the largest order size + 1 because of the loop following.
        int orderstroelse = Random.Range(1,4);

        //Loops an amount of time equal to the order size, then adds one random recipe to the order for each time.
        for(int i = 0; i < orderstroelse; i++)
        {
            //Finds a random recipe in the list of all recipes by choosing a random number between 0 and the number of recipes in the list of all recipes. This number serves as the indes in a search.
            int index = Random.Range(0, opskriftListe.Count);

            //Add the recipe at the given index from before into the list of recipes in the current active order, which stores the current order.
            neworder.Add(opskriftListe[index]);
           
        }
        neworderavailable = true;
    }

    public void StartNewOrder()
    {
        GameObject Currentorder = Instantiate(CanvasPrefab, OrderCanvas.transform, true);
        Currentorder.GetComponent<OrderSetupScript>().order = neworder;
        Currentorder.GetComponent<OrderSetupScript>().Initiate();
    }

    public void KundePause()
    {              
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.GetComponent<MeshRenderer>().material.color = Color.red;
        for (int i = 0; i < neworder.Count; i++)
        {
            Destroy(this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(i + 1).gameObject);
        }
        neworder.Clear();
        Invoke("SkabNyOrdre", Random.Range(mintimetillnextcustomer, maxtimetillnextcustomer));
    }
    
}
