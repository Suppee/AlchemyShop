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
        gameObject.GetComponent<MeshFilter>().mesh = itemRef.model;
        gameObject.GetComponent<MeshRenderer>().material = itemRef.material;
        gameObject.GetComponent<MeshCollider>().sharedMesh = itemRef.model;       
    }

    IEnumerator Despawn()
    {
        bool yayeet = true;
        while(yayeet)
        {
            if (gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero && gameObject.tag.Contains("Ingredient") && gameObject.transform.parent == null)
                yield return new WaitForSeconds(15);
            if (gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero && gameObject.tag.Contains("Ingredient") && gameObject.transform.parent == null)
            {
                yayeet = false;
                Debug.Log(gameObject + "is a no more.");
                Destroy(gameObject);
            }                
        }        
    }
}
