using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductInfo : MonoBehaviour
{
    // Variabler
    public Recipes Opskrift;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<MeshFilter>().mesh = Opskrift.model;
        this.GetComponent<MeshRenderer>().material = Opskrift.material;
        this.GetComponent<MeshCollider>().sharedMesh = Opskrift.model;
    }
}
