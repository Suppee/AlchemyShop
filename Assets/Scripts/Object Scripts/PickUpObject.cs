using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PickUpObject : NetworkBehaviour
{
    // Variabler
    [SyncVar]
    public GameObject parent;
    public MasterItem itemRef;
    public bool trail = false;
    public GameObject poofeffect;

    // Start is called before the first frame update
    [Server]
    void Start()
    {
        gameObject.GetComponent<MeshFilter>().mesh = itemRef.model;
        gameObject.GetComponent<MeshRenderer>().material = itemRef.material;
        gameObject.GetComponent<MeshCollider>().sharedMesh = itemRef.model;
        StartCoroutine(Despawn());
    }

    [Command (requiresAuthority = false)]
    public void PickUp(Transform parentTransform)
    {
        transform.position = parentTransform.position;
        transform.parent = parentTransform;
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Outline>().enabled = false;
        GetComponent<Missile>().enabled = false; 
    }

    public void Drop()
    {
        transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Outline>().enabled = true;
        GetComponent<MeshCollider>().enabled = true;
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
