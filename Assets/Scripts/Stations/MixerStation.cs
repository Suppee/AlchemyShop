using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerStation : MasterStation

{
    // Variabler
    public List<GameObject> Ingredienser;
    public List<GameObject> Opskrifter;
       
    public bool HarTingIHaanden;

    private bool Blander;

    //Aktiver station
    
    public override void Activate()
    {
        if (Blander == false && HarTingIHaanden == true)
        {
            //tilføj ingrediens til listen
            
        } else if (Blander == false && HarTingIHaanden == false)
        {
            //start mixer minigame
        }
    }
}
