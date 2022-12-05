using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductUI : MonoBehaviour
{
    //References to recipe and all icon locations.
    public RawImage stationRef;
    public RawImage productRef;
    public RawImage first_ingredientRef;
    public RawImage second_ingredientRef;
    public RawImage third_ingredientRef;
    public GameObject productfinishedscreen;

    // OnBegin is called by the Order Setup Script on the OrderUI Prefab to setup the individual products icons from its recipe reference.
    public void OnBegin(ProductRecipe currentrecipe)
    {
        stationRef.texture = currentrecipe.product.stationIcon;
        productRef.texture = currentrecipe.product.icon;
        first_ingredientRef.texture = currentrecipe.ingredients[0].icon;
        second_ingredientRef.texture = currentrecipe.ingredients[1].icon;
        third_ingredientRef.texture = currentrecipe.ingredients[2].icon;
    }    
}
