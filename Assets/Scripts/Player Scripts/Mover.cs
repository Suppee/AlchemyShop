using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Timers;

public class Mover : MonoBehaviour
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
    PlayerInputHandler playerInputHandler;
    public bool Interact = false;
    public bool putDown = false;

    public bool moving = false;
    public bool moving2 = false; 
    public Vector3 lastPos;

    // Interger Variabler 
    public List<GameObject> iRaekkevide;    // Objekter indenfor raekkevidde af spilleren    
    public GameObject interaktionsobjekt;   // Det objekt man forsøger at interger med
    public bool holderObjekt;               // Bool til at tjekke om man holdet et objekt eller ej
    public Transform PickUpHolder;          // Placeringen af objektet på spilleren
    public GameObject objekthold;           // Objektet man holder

    // Kast/lægge ting
    Rigidbody m_Rigidbody;

    [SerializeField]
    private float yeet;    
    [SerializeField]
    private float kraft = 200;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        m_Rigidbody = GetComponent<Rigidbody>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        StartCoroutine("FindTaettestObjekt");
        lastPos = this.transform.position;
    }

    public void SetInputVector(Vector2 direction)
    {       
        inputVector = direction; 
    }  

    public int GetPlayerIndex()
    {
        return playerIndex;
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
            Debug.Log(this.transform.position);
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
        
        if (Interact == true)
        {
           Invoke("OnPickUp",0);          
        }

        if (putDown == true)
        {
            Invoke("OnPickUp",0);
        }
       
    }
    //Interger med objekt
    public void OnPickUp()
    {
        if(iRaekkevide.Count > 0)
        {
            

            //Interger med Station
            if (interaktionsobjekt && interaktionsobjekt.tag.Contains("Station"))
            {
                if ((holderObjekt == true && !interaktionsobjekt.tag.Contains("Ingredient")) || (holderObjekt == false))
                {
                    //Debug.Log(this.gameObject + " har aktivet" + interaktionsobjekt);
                    interaktionsobjekt.GetComponent<MasterStation>().spillerref = this; 
                    interaktionsobjekt.GetComponent<MasterStation>().Activate();
                }
                //else
                   //Debug.Log("Kan ikke aktiver " + interaktionsobjekt);
            }
            //Interger med Pick Up
            else if (interaktionsobjekt && interaktionsobjekt.tag.Contains("PickUp"))
            {
                if (holderObjekt == true)
                    Smid();
                else
                {
                    objekthold = interaktionsobjekt;
                    SamlOp();
                }                      
            }
        }
        else if (objekthold != null)
            Smid();

        //interaktionsobjekt.GetComponent<Outline>().enabled = false;
        interaktionsobjekt = null;      
    }

    // Saml objekt op - attach object to player and removes gravity and collission
    public void SamlOp()
    {
        objekthold.transform.position = PickUpHolder.position;
        objekthold.transform.parent = PickUpHolder;
        objekthold.GetComponent<MeshCollider>().enabled = false;        
        objekthold.GetComponent<Rigidbody>().useGravity = false;
        objekthold.GetComponent<Rigidbody>().isKinematic = true;
        objekthold.GetComponent<Outline>().enabled = false;
        objekthold.GetComponent<Missile>().enabled = false;
        objekthold.GetComponent<AudioSource>().PlayOneShot(objekthold.GetComponent<ItemInfo>().itemRef.sound);
        iRaekkevide.Remove(objekthold);
        holderObjekt = true;
    }

    // Smid objekt i hånden
    public virtual void Smid()
    {
        objekthold.transform.parent = null;                
        objekthold.GetComponent<Rigidbody>().useGravity = true;
        objekthold.GetComponent<Rigidbody>().isKinematic = false;
        objekthold.GetComponent<Outline>().enabled = true;
        objekthold.GetComponent<MeshCollider>().enabled = true;
        if (Interact == true)
        {   
            objekthold.GetComponent<Rigidbody>().AddForce(transform.up * kraft);
            objekthold.GetComponent<Rigidbody>().AddForce(transform.forward * yeet);
            objekthold.GetComponent<TrailRenderer>().enabled = true;

            Interact = false;
        }
        objekthold.GetComponent<AudioSource>().PlayOneShot(objekthold.GetComponent<ItemInfo>().itemRef.sound);
        objekthold = null;
        holderObjekt = false;
    }

    // Objekt kommer inden for raekkevidde
    private void OnTriggerEnter(Collider other)
    {        
        if(!iRaekkevide.Contains(other.gameObject) && ((other.gameObject.tag.Contains("Station") || (other.gameObject.tag.Contains("PickUp")))))
            iRaekkevide.Add(other.gameObject);
        foreach(GameObject o in iRaekkevide)
        {
            if (!o)
                iRaekkevide.Remove(o);
        }
    }

    // Objekt forlader raekkevidde
    private void OnTriggerExit(Collider other)
    {
        if (iRaekkevide.Contains(other.gameObject))
        {
            if (interaktionsobjekt = other.gameObject)
                interaktionsobjekt = null;
            other.GetComponent<Outline>().enabled = false;
            iRaekkevide.Remove(other.gameObject);
        }
    }

    // Find taetteste objekt
    IEnumerator FindTaettestObjekt()
    {
        while(true)
        {
            // Find taetteste objekt fra listen af objekter i raekkevidde
            float kortestafstand = Mathf.Infinity;
            foreach (GameObject ting in iRaekkevide)
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
                        interaktionsobjekt = ting;
                        interaktionsobjekt.GetComponent<Outline>().enabled = true;
                        
                    }
                }          
            }
            yield return interaktionsobjekt;
        }        
    }
}