using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableChild : MonoBehaviour
{
    MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = gameObject.transform.GetComponent<MeshRenderer>();
    }

    void OnCollisionEnter(Collision col)
    {
        MakeChild(col.gameObject);
    }

    private void MakeChild(GameObject go)
    {
        //getting externs bond
        Vector3 goBonds = go.GetComponent<MeshRenderer>().bounds.extents;

        //Setting the positon of the object
        go.transform.position = new Vector3(gameObject.transform.position.x,
            meshRenderer.bounds.extents.y + goBonds.y + gameObject.transform.position.y,
            0);
        // After setting the position then making the child
        go.transform.SetParent(gameObject.transform);

        go.GetComponent<Rigidbody>().isKinematic = true;
    }

}