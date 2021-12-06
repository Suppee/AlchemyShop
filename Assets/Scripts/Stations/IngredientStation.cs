using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientStation : MasterStation
{
    // Variables
    public GameObject ingrediensPrefab;
    public Ingredient ingredient;

    // Hidkald Prefab
    public override void Activate()
    {
        GameObject NyIngredient = Instantiate(ingrediensPrefab);
        NyIngredient.GetComponent<ItemInfo>().itemRef = ingredient;
        spillerref.GetComponent<Mover>().objekthold = NyIngredient;
        spillerref.GetComponent<Mover>().SamlOp();        
    }
}
