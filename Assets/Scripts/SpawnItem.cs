using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : ActivateStation
{

    // Variables

    public GameObject ingrediensPrefab;
    
    // Hidkald Prefab

    public override void Activate()
    {

        Instantiate(ingrediensPrefab, transform.position + new Vector3(0,1,0), transform.rotation);
        
    }

    
}
