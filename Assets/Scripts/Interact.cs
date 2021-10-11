using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    // Interact Variables

    public UnityEvent interactAction;
    public KeyCode interactKey;
    public bool isInRange;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
                                        
                //Debug.Log("Activate");
               // interactAction.Invoke(); //Fire Event

        }

    }
    
}
