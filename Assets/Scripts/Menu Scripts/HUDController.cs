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
        ProductPrefab = Resources.Load<GameObject>("Menus & UI/PF_ProductUI");
    }

    public void StartOrderUI(Order neworder)
    {
        GameObject orderUI = Resources.Load<GameObject>("Menus & UI/PF_OrderUI");
        GameObject Currentorder = Instantiate(orderUI, OrderUIArea.transform, false);
        neworder.orderUI = Currentorder.GetComponent<OrderUI>();

        Currentorder.GetComponent<OrderUI>().SetupOrderUI(neworder);
    }

    public void DisplayGold()
    {           
        goldText.GetComponent<TextMeshProUGUI>().text = RoundManager.Instance.money.ToString();
    }
}
