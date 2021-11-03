using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class KundeOrder : MasterStation
{
    public bool orderIgang;
    public List<Recipes> opskriftListe;
    public GameObject[] vare;
    public List<Recipes> aktivorder;
    public float mintid;
    public float maxtid;
    

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

            Invoke("SkabNyOrdre", 2);
    }

    public override void Activate()
    {
        if(aktivorder.Count > 0 && orderIgang == true)
        {
            if (spillerref.holderObjekt == true)
            {
                for (int i = 0; i < aktivorder.Count; i++)
                { 
                    if (spillerref.objekthold.GetComponent<ProductInfo>().Opskrift.name.Equals(aktivorder[i].name))
                    {
                        aktivorder.RemoveAt(i);
                        Debug.Log("Produkt godkendt");
                        vare[i].GetComponent<RawImage>().texture = null;
                        Destroy();
                        //Ret UI
                        for (int v = 0; v <= vare.Length; v++)
                        {
                            vare[v].GetComponent<RawImage>().texture = null;
                            vare[v].GetComponent<RawImage>().texture = aktivorder[v].texture;
                        }
                        //Check om orderen er færdig
                        if (aktivorder.Count == 0)
                        {
                            orderIgang = false;
                            StartCoroutine("KundePause");
                        }                         
                        return;
                    }
                }
            }
            else
                Debug.Log("Produkt ikke godkendt");
        }
        else if (aktivorder.Count > 0 && orderIgang == false && spillerref.holderObjekt == false)
        {
            //Ingen order i gang
            print("Tag en order");
            orderIgang = true;
        }
        
    }

    public void SkabNyOrdre()
    {
        //Reset UI
        foreach (GameObject v in vare)
        {
            v.GetComponent<RawImage>().texture = null;
        }
        // Fjern alle elementer fra aktiv order 
        aktivorder.Clear();
        //Find order st�rrelse
        int orderstroelse = Random.Range(1, 2);
        for(int i = 0; i <= orderstroelse; i++)
        {
            int index = Random.Range(0, opskriftListe.Count);
            vare[i].GetComponent<RawImage>().texture = opskriftListe[index].texture;
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
