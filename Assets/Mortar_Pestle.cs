using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar_Pestle : MixerStation
{
   public override void Activate()
   {
       Debug.Log("Aktiveret" + this);
        if (spillerref.holderObjekt == true && spillerref.objekthold.tag.Contains("Ingredient") && blanding.Count < maxIngridienser)
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
        else if (spillerref.holderObjekt == false)
        {
            //start mixer minigame
            foreach (Recipes opskrift in Opskrifter)
            {
                if(blandingskode.Equals(opskrift.Kode))
                {
                    GameObject nyProdukt = Instantiate(produktPrefab);
                    nyProdukt.GetComponent<ProductInfo>().Opskrift = opskrift;
                    spillerref.objekthold = nyProdukt;
                    spillerref.SamlOp();                    
                }
                else
                {
                    Debug.Log("Stop");
                }
            }
            foreach(GameObject spot in mixspots)
            {
                if(spot.transform.childCount > 0)
                Destroy(spot.transform.GetChild(0).gameObject);
            }                
            blanding.Clear();
            blandingskode="";
        }
        else 
        {
            Debug.Log("Ikke din");
        }
        spillerref = null;
   }
}
