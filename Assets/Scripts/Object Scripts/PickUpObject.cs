using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    // Variabler
    public MasterItem itemRef;
    public bool trail = false;
    public GameObject poofeffect;

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
                    yield return new WaitForSeconds(8);
                    yayeet = false;
                }
                yield return new WaitForSeconds(0.1f);
            }
            if (gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero && gameObject.tag.Contains("Ingredient") && gameObject.transform.parent == null)
            {
                Instantiate(poofeffect, gameObject.transform);
                yield return new WaitForSeconds(0.2f);
                Destroy(gameObject);
            }

            yield return new WaitForSeconds(.1f);
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(trail == true)
            StartCoroutine(WaitBeforeStoping());
    }

    private IEnumerator WaitBeforeStoping()
    {
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<TrailRenderer>().enabled = false;
    }

    private IEnumerator Trail()
    {
        yield return new WaitForSeconds(1);
        trail = true;
        Debug.Log(trail);
    }
}
