using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixer : ActivateWorkstation
{   
   
    public List<GameObject> Ingredienser;

    public List<GameObject> Opskrifter;

    
    public bool HarTingIHaanden;

    private bool Blander;

    private void Awake()
    {
        if (Blander == false && HarTingIHaanden == true)
        {
            


        } else if (Blander == false && HarTingIHaanden == false)
        {

        }
        
    }
}
