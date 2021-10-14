using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHolder : MonoBehaviour
{
    public bool HolderPickUp;
    public GameObject Ingredient;
    public Transform Player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            Debug.Log("Ingrediens samlet op");
        }
           
        if (other.gameObject.tag == "Ingredient")
        {
            Ingredient.transform.SetParent(Player);
        }
    }

}
