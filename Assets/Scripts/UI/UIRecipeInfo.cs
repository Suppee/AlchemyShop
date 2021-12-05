using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecipeInfo : MonoBehaviour
{
    //References to recipe and all icon locations.
    public ProductRecipe currentrecipe;
    public RawImage stationRef;
    public RawImage productRef;
    public RawImage first_ingredientRef;
    public RawImage second_ingredientRef;
    public RawImage third_ingredientRef;
    public GameObject productfinishedscreen;

    // OnBegin is called by the Order Setup Script on the OrderUI Prefab to setup the individual products icons from its recipe reference.
    public void OnBegin()
    {
        stationRef.texture = currentrecipe.stationIcon;
        productRef.texture = currentrecipe.productIcon;
        first_ingredientRef.texture = currentrecipe.ingredients[0].icon;
        second_ingredientRef.texture = currentrecipe.ingredients[1].icon;
        third_ingredientRef.texture = currentrecipe.ingredients[2].icon;
    }    
}
