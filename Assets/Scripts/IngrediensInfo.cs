using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngrediensInfo : MonoBehaviour
{

    public Ingredient Ingredient;

   // public MeshFilter mesh;


    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<MeshFilter>().mesh = Ingredient.ingrediensModel;
        this.GetComponent<MeshRenderer>().material = Ingredient.material;
    }
}
