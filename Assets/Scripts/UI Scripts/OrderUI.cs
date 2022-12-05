using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderUI : MonoBehaviour
{
    // UI element references
    public GameObject productArea;
    public TMP_Text ordernumber;

    // Slider Variables
    public Slider timeSlider;
    public GameObject fill;
    public Gradient sliderGradient;

    public void Start()
    {
        productArea = transform.Find("ProductArea").gameObject;
        ordernumber = transform.Find("OrderNumber").gameObject.GetComponent<TMP_Text>();
        timeSlider = transform.Find("TimeSlider").gameObject.GetComponent<Slider>();
        fill = transform.GetChild(1).GetChild(0).Find("Fill").gameObject;

        // Slider Setup
        sliderGradient = new Gradient();
        GradientColorKey[] colorKey = new GradientColorKey[3];
        colorKey[0].color = Color.red;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.yellow;
        colorKey[1].time = 0.5f;
        colorKey[2].color = Color.green;
        colorKey[2].time = 1.0f;
        var alphaKey = new GradientAlphaKey[0];
        sliderGradient.SetKeys(colorKey, alphaKey);
    }

    // Setup Script
    public void SetupOrderUI(Order order)
    {
        // Setup Order Number Text Element
        ordernumber.text = (transform.GetSiblingIndex() +1).ToString();

        // Add Product Prefab for each product in order
        foreach (ProductRecipe product in order.products)
        {
            GameObject currentProduct = Instantiate(UIManager.Instance.ProductUIRef, productArea.transform);
            currentProduct.GetComponent<ProductUI>().OnBegin(product);
            Canvas.ForceUpdateCanvases();
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.gameObject.transform.parent.GetComponent<RectTransform>());

        // Setup Order Time Slider
        timeSlider.GetComponent<RectTransform>().sizeDelta = new Vector2(100 * order.products.Count, 20);
        timeSlider.maxValue = order.orderTime;
        timeSlider.value = order.orderTime;
        Canvas.ForceUpdateCanvases();
        StartCoroutine(SliderUpdate(order));
    }

    // Order Time  Slider Update 
    IEnumerator SliderUpdate(Order order)
    {
        while(order.orderTime > 0)
        {
            fill.GetComponent<Image>().color = sliderGradient.Evaluate(timeSlider.value / timeSlider.maxValue);
            timeSlider.value = order.orderTime;
        }
        return null;
    }

    // Product Completed
    public void ProductComplete(int p)
    {
        productArea.transform.GetChild(p).GetComponent<ProductUI>().productfinishedscreen.SetActive(true);
    }
}

