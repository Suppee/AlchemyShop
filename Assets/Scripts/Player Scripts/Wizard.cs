using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Mover
{
    public override void Smid()
    {   
        
        if(Interact == true)
        {
            objekthold.GetComponent<Missile>().enabled = true;
            Interact = false;
        }
        objekthold.GetComponent<Transform>();
        holderObjekt = false;
        objekthold.transform.parent = null;        
        objekthold.GetComponent<MeshCollider>().enabled = true;
        objekthold.GetComponent<Rigidbody>().useGravity = true;
        objekthold.GetComponent<Rigidbody>().isKinematic = false;
        objekthold.GetComponent<Outline>().enabled = true;
        
     
        iRaekkevide.Add(objekthold);
        objekthold = null;
    }
}
