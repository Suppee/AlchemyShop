using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientStation : BaseStation
{
    // Variables
    GameObject ingrediensPrefab;
    public IngredientItem ingredient;

    public void OnRenderObject()
    {
        gameObject.transform.GetChild(2).GetComponent<MeshFilter>().mesh = ingredient.model;
        gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material = ingredient.material;
    }

    public void Start()
    {
        ingrediensPrefab = Resources.Load<GameObject>("Prefabs/PF_Item") as GameObject;        
    }

    // Hidkald Prefab
    public override void Activate()
    {
        GameObject NyIngredient = Instantiate(ingrediensPrefab);
        NyIngredient.GetComponent<ItemInfo>().itemRef = ingredient;
        spillerref.GetComponent<BasePlayer>().heldObj = NyIngredient;
        spillerref.GetComponent<BasePlayer>().SamlOp();        
    }
}
