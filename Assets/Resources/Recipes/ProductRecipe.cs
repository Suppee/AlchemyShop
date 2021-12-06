using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Her laver vi referencen til filmenuen
[CreateAssetMenu(fileName = "New Product Recipe", menuName = "Product Recipe")]
public class ProductRecipe : MasterRecipe
{
    public ProductItem product;
}
