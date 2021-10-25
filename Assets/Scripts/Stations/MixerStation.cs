using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerStation : MasterStation

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
            // Tilføj ingrediens til listen
            blanding.Add(spillerref.objekthold.GetComponent<IngrediensInfo>().Ingredient);
            Destroy(spillerref.objekthold);
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
                    spillerref.GetComponent<PlayerControls>().objekthold = nyProdukt;
                    spillerref.GetComponent<PlayerControls>().SamlOp();
                }
            }
            
            
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
