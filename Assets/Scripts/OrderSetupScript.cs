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
    public List<Recipes> order;
    public GameObject productarea;
   // [System.Serializable]

    public class serializableClass
    {
        public List<Recipes> SingleOrder;
    }
    public List<serializableClass> activeOrders; // = new List<serializableClass>();
    

    public void Initiate()
    {
        ordernumber.text = (transform.GetSiblingIndex() +1).ToString();
        foreach (Recipes product in order)
        {
            GameObject currentProduct = Instantiate(productCanvas, productarea.transform);
            currentProduct.GetComponent<UIRecipeInfo>().currentrecipe = product;
            currentProduct.GetComponent<UIRecipeInfo>().OnBegin();
            Canvas.ForceUpdateCanvases();
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.gameObject.transform.parent.GetComponent<RectTransform>());

        timeslider.GetComponent<SliderTime>().gameTime = 30 * order.Count;
        timeslider.GetComponent<RectTransform>().sizeDelta = new Vector2(100*order.Count, 20);
        timeslider.GetComponent<SliderTime>().OnStart();
        // for(int i = 0; i < this.gameObject.transform.parent.gameObject.transform.GetChildCount; i++)

        
       // activeOrders.
    }    
}
