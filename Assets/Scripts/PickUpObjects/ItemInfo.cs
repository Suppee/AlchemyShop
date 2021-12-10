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
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        while(true)
        { 
            bool yayeet = true;
            while(yayeet)
            {
                //Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);
                if (gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero && gameObject.tag.Contains("Ingredient") && gameObject.transform.parent == null)
                {
                    yield return new WaitForSeconds(15);
                    yayeet = false;
                }
                yield return new WaitForSeconds(0.1f);
            }
            if (gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero && gameObject.tag.Contains("Ingredient") && gameObject.transform.parent == null)
                Destroy(gameObject);
            yield return new WaitForSeconds(.1f);
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(trail);
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
