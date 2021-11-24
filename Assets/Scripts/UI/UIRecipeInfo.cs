using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecipeInfo : MonoBehaviour
{
    //References to recipe and all icon locations.
    public Recipes currentrecipe;
    public RawImage stationRef;
    public RawImage productRef;
    public RawImage first_ingredientRef;
    public RawImage second_ingredientRef;
    public RawImage third_ingredientRef;
    public GameObject productfinishedscreen;

    //OnValidate does the same as onbegin but onValidate is called on any data change making the change visible without playing the game.
    public void OnValidate()
    {
        stationRef.texture = currentrecipe.stationIcon;
        productRef.texture = currentrecipe.productIcon;
        first_ingredientRef.texture = currentrecipe.ingredients[0].icon;
        second_ingredientRef.texture = currentrecipe.ingredients[1].icon;
        third_ingredientRef.texture = currentrecipe.ingredients[2].icon;
    }

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
