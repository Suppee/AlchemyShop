using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class KundeOrder : MasterStation
{
    public bool orderIgang;
    public List<Recipes> opskriftListe;    
    public List<Recipes> aktivorder;
    public float mintid;
    public float maxtid;
    public int PengeOrder = 10;
    public GameObject CanvasPrefab;
    public GameObject sliderref;
    public float mintimetillnextcustomer;
    public float maxtimetillnextcustomer;


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
        Invoke("SkabNyOrdre", Random.Range(mintimetillnextcustomer, maxtimetillnextcustomer));
    }

    public override void Activate()
    {
        if(aktivorder.Count > 0 && orderIgang == true && spillerref.holderObjekt == true)
        {
            for (int i = 0; i < aktivorder.Count; i++)
            { 
                if (spillerref.objekthold.GetComponent<ProductInfo>().Opskrift.name.Equals(aktivorder[i].name))
                {
                    aktivorder.RemoveAt(i);                    
                    Debug.Log("Produkt godkendt");                  
                    Destroy();
                    //Ret UI               
                    Destroy(this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(i+1).gameObject);

                    //Check om orderen er færdig
                    if (aktivorder.Count == 0)                    
                    {
                        orderIgang = false;
                        this.GetComponent<MeshRenderer>().material.color = Color.red;
                        SkabNyOrdre();                        
                        //StartCoroutine("KundePause");
                        GameObject.Find("Ur_Penge").GetComponent<Penge>().gold += PengeOrder;
                       
                    }                        
                    return;
                }
                else
                    Debug.Log("Produkt ikke godkendt");
            }
        }
        else if (aktivorder.Count > 0 && orderIgang == false && spillerref.holderObjekt == false)
        {
        //Ingen order i gang
            print("Tag en order");
            orderIgang = true;
            this.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }
      
    }

    //Method to create a new order.
    public void SkabNyOrdre()
    {
        // Remove all elements from the current active order, this is a failsafe to avoid left over items from the last order.
        sliderref.SetActive(false);

        for (int i = 0; i < aktivorder.Count; i++)
        {
            Destroy(this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(i + 1).gameObject);
        }
        aktivorder.Clear();

        //Find order size by choosing a random number between 1 as the minimum order size and the largest order size + 1 because of the loop following.
        int orderstroelse = Random.Range(1,3);

        //Loops an amount of time equal to the order size, then adds one random recipe to the order for each time.
        for(int i = 0; i < orderstroelse; i++)
        {
            //Finds a random recipe in the list of all recipes by choosing a random number between 0 and the number of recipes in the list of all recipes. This number serves as the indes in a search.
            int index = Random.Range(0, opskriftListe.Count);

            //Add the recipe at the given index from before into the list of recipes in the current active order, which stores the current order.
            aktivorder.Add(opskriftListe[index]);

            //Spawn UI element for the chosen recipes and adds the proper icons for the product and ingredients.
            GameObject thiscanvas = Instantiate(CanvasPrefab, this.gameObject.transform.GetChild(0));
            thiscanvas.GetComponent<UIRecipeInfo>().currentrecipe = opskriftListe[index];
            thiscanvas.GetComponent<UIRecipeInfo>().OnBegin();            
        }
        //Set slider visible and the time before this order disappears
        sliderref.SetActive(true);
        sliderref.GetComponent<SliderTime>().gameTime = 60;
        sliderref.GetComponent<SliderTime>().OnStart();
        
    }

    IEnumerator KundePause()
    {                                  
            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(Random.Range(mintid,maxtid));            
    }
    
}
