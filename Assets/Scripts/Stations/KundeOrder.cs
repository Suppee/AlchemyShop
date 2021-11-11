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

        Invoke("SkabNyOrdre", 2);
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
                    Destroy(this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject);

                    //Check om orderen er færdig
                    if (aktivorder.Count == 0)                    
                    {
                        orderIgang = false;
                        this.GetComponent<MeshRenderer>().material.color = Color.red;
                        SkabNyOrdre();
                        Debug.Log("Hello");
                        //StartCoroutine("KundePause");
                        GameObject.Find("Ur_Penge").GetComponent<Penge>().gold += PengeOrder;
                        GameObject.Find("SliderTimer").GetComponent<SliderTime>().gameTime = 60;
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

    public void SkabNyOrdre()
    {
        // Fjern alle elementer fra aktiv order 
        aktivorder.Clear();
        //Find order st�rrelse
       
        int orderstroelse = Random.Range(0,5);
        for(int i = 0; i <= orderstroelse; i++)
        {
            int index = Random.Range(0, opskriftListe.Count);            
            GameObject thiscanvas = Instantiate(CanvasPrefab, this.gameObject.transform.GetChild(0));
            thiscanvas.GetComponent<UIRecipeInfo>().currentrecipe = opskriftListe[index];
            thiscanvas.GetComponent<UIRecipeInfo>().OnBegin();
            aktivorder.Add(opskriftListe[index]);
        }
    }

    IEnumerator KundePause()
    {        
            //Print the time of when the function is first called.
            Debug.Log("Started Coroutine at timestamp : " + Time.time);

            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(Random.Range(mintid,maxtid));

            //After we have waited 5 seconds print the time again.
            Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            SkabNyOrdre();
    }
    
}
