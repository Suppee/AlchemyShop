using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Order
{
    public List<ProductRecipe> products;
    public List<bool> finishedproducts;
    public OrderStation OrderStation;
    public OrderUI orderUI;
    public float orderTime;
}


