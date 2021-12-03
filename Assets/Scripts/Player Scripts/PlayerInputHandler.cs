using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Mover mover;

    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var movers = FindObjectsOfType<Mover>();
        var index = playerInput.playerIndex;
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
    }
    
    public void OnMove(CallbackContext context)
    {
        if(mover != null)
            mover.SetInputVector(context.ReadValue<Vector2>());
    }

    public void OnInteract(CallbackContext context)
    {
        if(mover != null)
        {
            if(context.started == true)
            {
                mover.Interact = true;
            }
           
        }
    }

    public void OnPutDown(InputAction.CallbackContext context)
    {
        if( mover != null)
        {
           
            if(context.started == true)
            { 
                mover.putDown = true;
            }
            
        }
    }

    void LateUpdate()
    {
        mover.Interact = false;
        mover.putDown = false;
    }
}
