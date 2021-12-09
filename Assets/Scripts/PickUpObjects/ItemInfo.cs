using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    // Variabler
    public MasterItem itemRef;
    public bool trail = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshFilter>().mesh = itemRef.model;
        gameObject.GetComponent<MeshRenderer>().material = itemRef.material;
        gameObject.GetComponent<MeshCollider>().sharedMesh = itemRef.model;
    }

    private void Update()
    {
        if(gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero  && gameObject.tag.Contains("Ingredient") && gameObject.transform.parent == null)
        {
            Destroy(gameObject, 15);
        }
            
        if (gameObject.GetComponent<Rigidbody>().velocity != Vector3.zero)
        {
            StartCoroutine(Hello());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(trail);
        if(trail == true)
            StartCoroutine(WaitBeforeStoping());
    }
    private IEnumerator WaitBeforeStoping()
    {
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<TrailRenderer>().enabled = false;
    }

    private IEnumerator Hello()
    {
        yield return new WaitForSeconds(1);
        trail = true;
        Debug.Log(trail);
    }
}
