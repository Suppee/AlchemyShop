using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecipeInfo : MonoBehaviour
{

    public Recipes currentrecipe;
    public GameObject stationRef;
    public GameObject productRef;
    public GameObject first_ingredientRef;
    public GameObject second_ingredientRef;
    public GameObject third_ingredientRef;

    // Start is called before the first frame update
    public void OnBegin()
    {
        stationRef.GetComponent<RawImage>().texture = currentrecipe.stationIcon;
        productRef.GetComponent<RawImage>().texture = currentrecipe.productIcon;
        first_ingredientRef.GetComponent<RawImage>().texture = currentrecipe.ingredients[0].icon;
        second_ingredientRef.GetComponent<RawImage>().texture = currentrecipe.ingredients[1].icon;
        third_ingredientRef.GetComponent<RawImage>().texture = currentrecipe.ingredients[2].icon;
    }    
}
