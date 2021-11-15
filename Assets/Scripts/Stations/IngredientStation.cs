using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientStation : MasterStation
{

    // Variables

    public GameObject ingrediensPrefab;
    public Ingredient ingredient;
    public GameObject ingredienticon;

    // Hidkald Prefab

    private void OnValidate()
    {
        ingredienticon.GetComponent<RawImage>().texture = ingredient.icon;
    }

    public override void Activate()
    {
        GameObject NyIngredient = Instantiate(ingrediensPrefab);
        NyIngredient.GetComponent<IngrediensInfo>().Ingredient = ingredient;
        spillerref.GetComponent<PlayerControls>().objekthold = NyIngredient;
        spillerref.GetComponent<PlayerControls>().SamlOp();        
    }

    
}
