using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerStation : MasterStation 
    // Ejer de ting MasterStation script har (eller inherit)

{
    // Variabler
    //[HideInInspector]
    public List<ScriptableObject> blanding;
    public List<ScriptableObject> Opskrifter;
    public GameObject produktPrefab;

    public bool HarTingIHaanden;
    public string blandingskode;
    private bool Blander;
    public GameObject[] mixspots;

    // Aktiver station
    
    public override void Activate()
    {
               
        Debug.Log("Aktiveret" + this);
        if (spillerref.holderObjekt == true)
        {
            // Tilf�j ingrediens til listen
            blanding.Add(spillerref.objekthold.GetComponent<IngrediensInfo>().Ingredient);     
            spillerref.objekthold.transform.position = mixspots[blanding.Count -1].gameObject.transform.position;
            spillerref.objekthold.transform.parent = mixspots[blanding.Count -1].gameObject.transform;
            spillerref.holderObjekt = false;
            spillerref.objekthold = null;

            blandingskode = "";
            GenerereKode();
        }
        else
        {
            //start mixer minigame
            foreach (Recipes opskrift in Opskrifter)
            {
                if(blandingskode.Equals(opskrift.Kode))
                {
                    GameObject nyProdukt = Instantiate(produktPrefab);
                    nyProdukt.GetComponent<ProductInfo>().Opskrift = opskrift;
                    spillerref.GetComponent<Mover>().objekthold = nyProdukt;
                    spillerref.GetComponent<Mover>().SamlOp();                    
                }

            }
            foreach(GameObject spot in mixspots)
                Destroy(spot.transform.GetChild(0).gameObject);
            blanding.Clear();
        }
        spillerref = null;
    }

    public void GenerereKode()
    {
        blandingskode += blanding.Count.ToString();
        foreach (Ingredient ing in blanding)
        {
            blandingskode += ing.Kode;
        }
    }

}
