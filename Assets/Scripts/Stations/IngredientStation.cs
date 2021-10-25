using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientStation : MasterStation
{

    // Variables

    public GameObject ingrediensPrefab;
    public Ingredient ingredient;
    
    // Hidkald Prefab

    public override void Activate()
    {
        GameObject NyIngredient = Instantiate(ingrediensPrefab);
        NyIngredient.GetComponent<IngrediensInfo>().Ingredient = ingredient;
        spillerref.GetComponent<PlayerControls>().objekthold = NyIngredient;
        spillerref.GetComponent<PlayerControls>().SamlOp();        
    }

    
}
