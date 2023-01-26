using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Mirror;

public class BaseStation : NetworkBehaviour
{
    //Variabler
    [HideInInspector]
    public BasePlayer spillerref;
    [HideInInspector]
    public GameObject playerPickup;

    //Aktiver Station Metode
    public virtual void Activate()
    { 

    }

    protected virtual void AcceptPickUp()
    {
        Destroy(spillerref.heldObj);
        //spillerref.holdingObj = false;
        spillerref.heldObj = null;
    } 
}
