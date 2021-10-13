using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject PickUpItem;
    public Transform Player;


    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKey("p") && other.gameObject.tag == "PickUp")
        {
            Debug.Log("PickUp in Range");
            PickUpItem.transform.parent = Player.transform;
        }
    }
}
