using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecipeInfo : MonoBehaviour
{

    public Recipes currentrecipe;
    public GameObject product;
    public GameObject first_ingredient;
    public GameObject second_ingredient;
    public GameObject third_ingredient;

    // Start is called before the first frame update
    public void OnBegin()
    {      
        product.GetComponent<RawImage>().texture = currentrecipe.texture;
        first_ingredient.GetComponent<RawImage>().texture = currentrecipe.ingredients[0].icon;
        second_ingredient.GetComponent<RawImage>().texture = currentrecipe.ingredients[1].icon;
        third_ingredient.GetComponent<RawImage>().texture = currentrecipe.ingredients[2].icon;
    }    
}
