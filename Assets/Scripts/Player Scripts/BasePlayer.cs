using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Timers;
using Mirror;

public class BasePlayer : NetworkBehaviour
{
    // Bevaegelse Variable
    [SerializeField]
    public float speed = 5; 

    CharacterController controller;
    Vector3 direction = Vector3.zero;
    Vector2 inputVector = Vector2.zero;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    
    public int  playerIndex = 0;
    public bool Interact = false;
    public bool putDown = false;

    public bool moving = false;
    public Vector3 lastPos;

    // Interger Variabler 
    public List<GameObject> inRange;    // Objekter indenfor raekkevidde af spilleren    
    public GameObject interactionObj;   // Det objekt man forsøger at interger med
    public bool holdingObj;               // Bool til at tjekke om man holdet et objekt eller ej
    public Transform PickUpHolder;          // Placeringen af objektet på spilleren
    public GameObject heldObj;           // Objektet man holder

    // Kast/lægge ting
    Rigidbody m_Rigidbody;

    [SerializeField]
    private float throwing;    
    [SerializeField]
    private float power = 200;

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        UnityEngine.InputSystem.PlayerInput playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        playerInput.enabled = true;
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        m_Rigidbody = GetComponent<Rigidbody>();        
        StartCoroutine("FindTaettestObjekt");
        lastPos = this.transform.position;
    }
    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    // Pause Game
    public void OnPause(InputValue context)
    {
        GameManager.Instance.PauseGame();
    }

    // Player Input Move character
    public void OnMove(InputValue context)
    {
        if (isLocalPlayer && GameManager.Instance.curState == GameState.Playing)
            inputVector = context.Get<Vector2>();
    }

    // Player Input Interact with object
    public void OnInteract(InputValue context)
    {
        
        if (isLocalPlayer &&context.isPressed == true && GameManager.Instance.curState == GameState.Playing)
            {
                OnInteract();
            }
    }

    // Player Input Put down held object
    public void OnPutDown(InputValue context)
    {

            if (isLocalPlayer && context.isPressed == true && GameManager.Instance.curState == GameState.Playing)
            {
                putDown = true;
            }
    }

    

    public void Update()
    {
        direction = new Vector3(inputVector.x, 0, inputVector.y) * speed;
        controller.SimpleMove(direction);
        

         var displacement = this.transform.position - lastPos;
        lastPos = this.transform.position;
   
        if(displacement.magnitude > 0.001)  // return true if char moved 1mm
        {
            this.moving = true;
        } 
        else 
        {
            this.moving = false;
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);  
        }
        
        //if (Interact == true)
        //{
        //   Invoke("OnPickUp",0);          
        //}

        //if (putDown == true)
        //{
        //    Invoke("OnPickUp",0);
        //}
       
    }
    //Interger med objekt
    public void OnInteract()
    {
        if (inRange.Count > 0)
        {          

            //  Check if Interaction with Station
            if (interactionObj && interactionObj.tag.Contains("Station"))
            {
                if ((holdingObj == true && !interactionObj.tag.Contains("Ingredient")) || (holdingObj == false))
                {
                    //Debug.Log(this.gameObject + " har aktivet" + interaktionsobjekt);
                    interactionObj.GetComponent<BaseStation>().spillerref = this; 
                    interactionObj.GetComponent<BaseStation>().Activate();
                }
                //else
                   //Debug.Log("Kan ikke aktiver " + interaktionsobjekt);
            }

            //  Check if Interaction with Pick Up
            else if (interactionObj && interactionObj.tag.Contains("PickUp"))
            {
                if (holdingObj == true)
                    Smid();
                else
                {
                    heldObj = interactionObj;
                    SamlOp();
                }                      
            }
        }
        else if (heldObj != null)
            Smid();

        //interaktionsobjekt.GetComponent<Outline>().enabled = false;
        interactionObj = null;      
    }

    // Saml objekt op - attach object to player and removes gravity and collission
    public void SamlOp()
    {
        Debug.Log("PickUp");
        heldObj.transform.position = PickUpHolder.position;
        heldObj.transform.parent = PickUpHolder;
        heldObj.GetComponent<MeshCollider>().enabled = false;        
        heldObj.GetComponent<Rigidbody>().useGravity = false;
        heldObj.GetComponent<Rigidbody>().isKinematic = true;
        heldObj.GetComponent<Outline>().enabled = false;
        heldObj.GetComponent<Missile>().enabled = false;
        heldObj.GetComponent<AudioSource>().PlayOneShot(heldObj.GetComponent<PickUpObject>().itemRef.sound);  
        //heldObj.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        inRange.Remove(heldObj);
        holdingObj = true;

      

    }

    // Smid objekt i hånden
    public virtual void Smid()
    {
        Debug.Log("Drop");
        heldObj.transform.parent = null;                
        heldObj.GetComponent<Rigidbody>().useGravity = true;
        heldObj.GetComponent<Rigidbody>().isKinematic = false;
        heldObj.GetComponent<Outline>().enabled = true;
        heldObj.GetComponent<MeshCollider>().enabled = true;
        if (Interact == true)
        {   
            heldObj.GetComponent<Rigidbody>().AddForce(transform.up * power);
            heldObj.GetComponent<Rigidbody>().AddForce(transform.forward * throwing);
            heldObj.GetComponent<TrailRenderer>().enabled = true;

            Interact = false;
        }
        heldObj.GetComponent<AudioSource>().PlayOneShot(heldObj.GetComponent<PickUpObject>().itemRef.sound);
        heldObj = null;
        holdingObj = false;
    }

    // Objekt kommer inden for raekkevidde
    private void OnTriggerEnter(Collider other)
    {        
        if(!inRange.Contains(other.gameObject) && ((other.gameObject.tag.Contains("Station") || (other.gameObject.tag.Contains("PickUp")))))
            inRange.Add(other.gameObject);
        foreach(GameObject o in inRange)
        {
            if (!o)
                inRange.Remove(o);
        }
    }

    // Objekt forlader raekkevidde
    private void OnTriggerExit(Collider other)
    {
        if (inRange.Contains(other.gameObject))
        {
            if (interactionObj = other.gameObject)
                interactionObj = null;
            other.GetComponent<Outline>().enabled = false;
            inRange.Remove(other.gameObject);
        }
    }

    // Find closest interactable object
    IEnumerator FindTaettestObjekt()
    {
        while(true)
        {
            // Find taetteste objekt fra listen af objekter i raekkevidde
            float kortestafstand = Mathf.Infinity;
            foreach (GameObject ting in inRange)
            {
                // Debug line
                // Debug.DrawLine(ting.transform.position, this.transform.position, Color.red, 3);

                // Find afstand mellem objekt og spiller
                if(ting)
                {
                    float afstand = Vector3.Distance(ting.transform.position, this.transform.position);

                    // Hvis afstanden er kortere end den korteste afstand saa saet den korteste afstand til den nye afstand
                    if (afstand <= kortestafstand)
                    {
                        kortestafstand = afstand;
                        interactionObj = ting;
                        interactionObj.GetComponent<Outline>().enabled = true;
                        
                    }
                }          
            }
            yield return interactionObj;
        }        
    }
}