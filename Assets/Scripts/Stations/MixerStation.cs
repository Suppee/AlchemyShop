using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerStation : MasterStation

{
    // Variabler
    public List<GameObject> blanding;
    public List<GameObject> Opskrifter;
       
    public bool HarTingIHaanden;

    private bool Blander;

    //Aktiver station
    
    public override void Activate()
    {
        Debug.Log("Aktiveret" + this);
        if (Blander == false && HarTingIHaanden == true)
        {

            //tilføj ingrediens til listen
            blanding.Add(playerPickup);
            playerPickup = null;
            Destroy(player.GetComponent<PlayerControls>().objekthold);
        } else if (Blander == false && HarTingIHaanden == false)
        {
            //start mixer minigame
        }
    }
}
