using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public GameObject goldText;
    public GameObject OrderUIArea;
    public GameObject ProductPrefab;

    // Start is called before the first frame update
    void OnEnable()
    {
        goldText = transform.GetChild(1).transform.Find("CurrentMoney").gameObject;
        OrderUIArea = transform.Find("CurrentOrdersUI").gameObject;
        ProductPrefab = Resources.Load<GameObject>("Prefabs/UI/PF_ProductUI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartOrderUI(Order neworder)
    {
        GameObject orderUI = Resources.Load<GameObject>("Prefabs/UI/PF_OrderUI");
        GameObject Currentorder = Instantiate(orderUI, OrderUIArea.transform, false);
        neworder.orderUI = Currentorder.GetComponent<OrderUI>();

        Currentorder.GetComponent<OrderUI>().SetupOrderUI(neworder);
    }

    public void DisplayGold()
    {           
        goldText.GetComponent<TextMeshPro>().text = RoundManager.Instance.money.ToString();
    }
}
