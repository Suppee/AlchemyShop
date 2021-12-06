using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    // Variabler
    public MasterItem itemRef;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<MeshFilter>().mesh = itemRef.model;
        this.GetComponent<MeshRenderer>().material = itemRef.material;
        this.GetComponent<MeshCollider>().sharedMesh = itemRef.model;
    }
}
