using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Order
{
    public bool orderStarted;
    public List<ProductRecipe> products;
    public List<bool> finishedproducts;
    public OrderUI orderUI;
    public float orderTime;
}


