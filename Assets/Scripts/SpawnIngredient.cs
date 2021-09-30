using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIngredient : MonoBehaviour
{
    public GameObject myPrefab;
    Rigidbody rb;
    

    private void Start()
    {
      
    }

    public void Spawn(GameObject Ingredient)
    {
        Instantiate(myPrefab, transform.position + new Vector3(0,1,0), transform.rotation);
        rb = myPrefab.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.up * 1000, ForceMode.Impulse);
    }

    
}
