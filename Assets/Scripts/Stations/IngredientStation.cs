using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientStation : MasterStation
{
    // Variables
    GameObject ingrediensPrefab;
    public Ingredient ingredient;

    public void OnValidate()
    {
        
    }

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
        spillerref.GetComponent<Mover>().objekthold = NyIngredient;
        spillerref.GetComponent<Mover>().SamlOp();        
    }
}
