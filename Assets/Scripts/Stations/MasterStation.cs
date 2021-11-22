using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MasterStation : MonoBehaviour
{
    //Variabler
    [HideInInspector]
    public Mover spillerref;
    [HideInInspector]
    public GameObject playerPickup;

    //Aktiver Station Metode
    public virtual void Activate()
    { 
    }

    public void Destroy()
    {
        Destroy(spillerref.objekthold);
        spillerref.holderObjekt = false;
        spillerref.objekthold = null;
    }
 
}
