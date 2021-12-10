using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderSetupScript : MonoBehaviour
{
    public GameObject productCanvas;
    public Slider timeslider;
    public TMP_Text ordernumber;
    public Order order;
    public GameObject productarea;

    public void Initiate()
    {
        ordernumber.text = (transform.GetSiblingIndex() +1).ToString();
        foreach (ProductRecipe product in order.products)
        {
            GameObject currentProduct = Instantiate(productCanvas, productarea.transform);
            currentProduct.GetComponent<UIRecipeInfo>().currentrecipe = product;
            currentProduct.GetComponent<UIRecipeInfo>().OnBegin();
            Canvas.ForceUpdateCanvases();
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.gameObject.transform.parent.GetComponent<RectTransform>());

        timeslider.GetComponent<SliderTime>().gameTime = 30 * order.products.Count;
        timeslider.GetComponent<RectTransform>().sizeDelta = new Vector2(100*order.products.Count, 20);
        timeslider.GetComponent<SliderTime>().OnStart();
    }    

    public void DeleteOrder()
    {
        Destroy(this.gameObject);
    }
}
