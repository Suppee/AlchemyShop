using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderSetupScript : MonoBehaviour
{
    public GameObject productCanvas;
    public Slider timeslider;
    public GameObject ordernumber;
    public List<Recipes> order;
    public GameObject productarea;
    //public List<List<Recipes>> orders;
   // [System.Serializable]
    public class serializableClass
    {
        public List<Recipes> SingleOrder;
    }
    public List<serializableClass> activeOrders; // = new List<serializableClass>();
    

    public void Initiate()
    {
        foreach (Recipes product in order)
        {
            GameObject currentProduct = Instantiate(productCanvas, productarea.transform);
            currentProduct.GetComponent<UIRecipeInfo>().currentrecipe = product;
            currentProduct.GetComponent<UIRecipeInfo>().OnBegin();
            Canvas.ForceUpdateCanvases();
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.gameObject.transform.parent.GetComponent<RectTransform>());

        timeslider.GetComponent<SliderTime>().gameTime = 30 * order.Count;
        timeslider.GetComponent<SliderTime>().OnStart();
       // activeOrders.
    }

    
}
