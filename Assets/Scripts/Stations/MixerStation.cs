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

    public int maxIngridienser;
    public bool HarTingIHaanden;
    public string blandingskode;
    private bool Blander;
    public GameObject[] mixspots;

    [SerializeField]
    private int index;

    // Aktiver station
    
    public override void Activate()
    {  
        
                    
        Debug.Log("Aktiveret" + this);
          if (spillerref.holderObjekt == true && spillerref.objekthold.tag.Contains("Ingredient") && spillerref.playerIndex == index && blanding.Count < maxIngridienser)
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

    public void GenerereKode()
    {
        blandingskode += blanding.Count.ToString();
        foreach (Ingredient ing in blanding)
        {
            blandingskode += ing.Kode;
        }
    }
}
