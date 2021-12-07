using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerStation : MasterStation 
    // Ejer de ting MasterStation script har (eller inherit)
{
    // Station Variables
    [HideInInspector]
    public List<ScriptableObject> blanding;
    public List<MasterRecipe> Opskrifter;
    public GameObject productPrefab;
    public int maxIngredients;    
    public GameObject[] mixspots;

    // Player Station Interaction Variables
    public bool sharedStation;
    [SerializeField]
    private int playerIndex;
    [HideInInspector]
    public bool holdingObjects;

    public void Start()
    {
        productPrefab = Resources.Load<GameObject>("Prefabs/PF_Item") as GameObject;
    }

    // Aktiver station    
    public override void Activate()
    {                     
          //Debug.Log("Aktiveret" + this);
          if (spillerref.holderObjekt == true && spillerref.objekthold.tag.Contains("Ingredient") && blanding.Count < maxIngredients)
          {
            AddIngredient(spillerref.objekthold);
            spillerref.holderObjekt = false;
            spillerref.objekthold = null;
          }
          else if ((!sharedStation && spillerref.holderObjekt == false && spillerref.playerIndex == playerIndex) || (sharedStation && spillerref.holderObjekt == false))
          {
            //start mixer minigame
            foreach (MasterRecipe opskrift in Opskrifter)
            {
                var hasAll = true;
                var found = false;
                for(int i = 0; i < opskrift.ingredients.Length; i++)
                {
                    if (blanding.Contains(opskrift.ingredients[i])) found = true;
                    else
                    {
                        found = false;
                        if (!found) hasAll = false;
                    }
                }
                
                if (hasAll)
                {                                     
                    GameObject newProduct = Instantiate(productPrefab);
                    if (opskrift is ProductRecipe)
                    {
                        var opskriftref = opskrift as ProductRecipe;
                        newProduct.GetComponent<ItemInfo>().itemRef = opskriftref.product;
                        newProduct.tag = "Product PickUp";
                    }                        
                    else if (opskrift is CrossOverProduct)
                    {
                        var opskriftref = opskrift as CrossOverProduct;
                        newProduct.GetComponent<ItemInfo>().itemRef = opskriftref.ingredient;
                    }                        
                    
                    spillerref.objekthold = newProduct;
                    spillerref.SamlOp();
                }
            }
            foreach(GameObject spot in mixspots)
            {
                if(spot.transform.childCount > 0)
                Destroy(spot.transform.GetChild(0).gameObject);
            }                
            blanding.Clear();
            }
            else 
            {
            Debug.Log("Ikke din");
            }
        spillerref = null;
    }

    public void AddIngredient(GameObject item)
    {
        // Tilfoej ingrediens til listen
        blanding.Add(item.GetComponent<ItemInfo>().itemRef);
        item.transform.position = mixspots[blanding.Count - 1].gameObject.transform.position;
        item.transform.parent = mixspots[blanding.Count - 1].gameObject.transform;
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().isKinematic = true;


    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Ingredient") && other.gameObject.transform.parent == null)
        {
            AddIngredient(other.gameObject);
        }

    }
}
