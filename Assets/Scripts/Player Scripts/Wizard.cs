using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Mover
{
    public override void Smid()
    {   
        objekthold.GetComponent<Transform>();
        
        objekthold.transform.parent = null;        
        objekthold.GetComponent<MeshCollider>().enabled = true;
        objekthold.GetComponent<Rigidbody>().isKinematic = false;
        objekthold.GetComponent<Outline>().enabled = true;
        if(Interact == true)
        {
            objekthold.GetComponent<Missile>().enabled = true;
            Interact = false;
        }
        else
        {
           objekthold.GetComponent<Rigidbody>().useGravity = true;
        }
        holderObjekt = false;
        iRaekkevide.Add(objekthold);
        objekthold.GetComponent<AudioSource>().PlayOneShot(objekthold.GetComponent<ItemInfo>().itemRef.sound);
        objekthold = null;
    }
}
