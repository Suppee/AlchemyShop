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
            if (Input.GetKeyDown(interactKey)) //Key Check
            {

                Debug.Log("Activate");
                interactAction.Invoke(); //Fire Event

            }
        }

    }


    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player in range");
        }

    }

    void OnTriggerExit(Collider collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player no longer in range");
        }

    }
}
