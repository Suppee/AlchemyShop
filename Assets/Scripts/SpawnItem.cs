using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : ActivateWorkstation
{

    // Variables

    public GameObject myPrefab;
    
    
    // Start

    private void Start()
    {
      
    }

    // Spawn prefab

    public override void Activate()
    {

        Instantiate(myPrefab, transform.position + new Vector3(0,1,0), transform.rotation);
        
    }

    
}
