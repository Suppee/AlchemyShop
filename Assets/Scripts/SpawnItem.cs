using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{

    // Variables

    public GameObject myPrefab;
    
    
    // Start

    private void Start()
    {
      
    }

    // Spawn prefab

    public void Spawn(GameObject Ingredient)
    {

        Instantiate(myPrefab, transform.position + new Vector3(0,1,0), transform.rotation);
        
    }

    
}
