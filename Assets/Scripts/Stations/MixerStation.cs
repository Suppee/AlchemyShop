using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerStation : MasterStation 
    // Ejer de ting MasterStation script har (eller inherit)

{
    // Variabler
    public List<ScriptableObject> blanding;
    public List<ScriptableObject> Opskrifter;
    public GameObject produktPrefab;

    public bool HarTingIHaanden;
    public string blandingskode;
    private bool Blander;

    // Aktiver station
    
    public override void Activate()
    {
               
        Debug.Log("Aktiveret" + this);
        if (spillerref.holderObjekt == true)
        {
            // Tilf�j ingrediens til listen
            blanding.Add(spillerref.objekthold.GetComponent<IngrediensInfo>().Ingredient);
            Destroy();           

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
