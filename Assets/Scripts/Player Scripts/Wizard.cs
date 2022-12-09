using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : BasePlayer
{
    public override void Smid()
    {   
        heldObj.GetComponent<Transform>();
        
        heldObj.transform.parent = null;        
        heldObj.GetComponent<MeshCollider>().enabled = true;
        heldObj.GetComponent<Rigidbody>().isKinematic = false;
        heldObj.GetComponent<Outline>().enabled = true;
        if(Interact == true)
        {
            heldObj.GetComponent<Missile>().enabled = true;
            heldObj.GetComponent<TrailRenderer>().enabled = true;
            Interact = false;
        }
        else
        {
           heldObj.GetComponent<Rigidbody>().useGravity = true;
        }
        holdingObj = false;
        heldObj.GetComponent<AudioSource>().PlayOneShot(heldObj.GetComponent<PickUpObject>().itemRef.sound);
        heldObj = null;
        
        var displacement = this.transform.position - lastPos;
        lastPos = this.transform.position;
    }
}
