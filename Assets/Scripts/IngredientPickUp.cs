using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IngredientPickUp : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactionKey;
    public UnityEvent interactAction;

    void Update()
    {
        if (isInRange) //If Player is in range
        {
            if (Input.GetKeyDown(interactionKey)) //And is pressing the interactionKey (.)
            {
                interactAction.Invoke(); //Perform the UnityEvent
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player in range"); 
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        isInRange = false;
        Debug.Log("Player out of range");
    }
}
