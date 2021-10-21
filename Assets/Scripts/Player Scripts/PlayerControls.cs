using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    // Bevaegelse Variable
    public float bevaegelsesFart = 5f;
    float bevaegelseX;
    float bevaegelseY;

    // Interger
    public List<GameObject> iRaekkevide;
    bool holderObjekt;
    public GameObject interaktionsobjekt;

    // PickUp
    public Transform PickUpHolder;
    public GameObject objekthold;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(bevaegelseX, 0.0f, bevaegelseY);

        // Look Towards
        transform.LookAt(newPosition + transform.position);
        


        // Move
        transform.Translate(newPosition * bevaegelsesFart * Time.deltaTime, Space.World);

    }

    void OnMove(InputValue bevaegelseVaerdi)
    {

        Vector2 bevaegelsesVector = bevaegelseVaerdi.Get<Vector2>();

        bevaegelseX = bevaegelsesVector.x;
        bevaegelseY = bevaegelsesVector.y;

        Debug.Log("Bevægelse");
        
    }
    
    void OnInteract()
    {
        if(holderObjekt == false)
        {
            StartCoroutine("FindTaettestObjekt");
        
            //Interger med objekt
            if (interaktionsobjekt.CompareTag("Station"))
            {
                if(holderObjekt == true) //&& interaktionsobjekt.compare)
                {

                }
                else
                {
                    interaktionsobjekt.GetComponent<MasterStation>().playerPickup = objekthold;
                    interaktionsobjekt.GetComponent<MasterStation>().player = this.gameObject;
                    interaktionsobjekt.GetComponent<MasterStation>().Activate();
                }

                Debug.Log("Aktiver Workstation");

                // Aktiver station                
                
            }
            else if (interaktionsobjekt.CompareTag("PickUp"))
            {
                Debug.Log("Ingrediens samlet op");

                // Saml objekt op              
                holderObjekt = true;
                objekthold = interaktionsobjekt;
                objekthold.transform.position = PickUpHolder.position;
                objekthold.transform.parent = PickUpHolder;
                objekthold.GetComponent<Rigidbody>().useGravity = false;
                objekthold.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        else
        {          
            if(iRaekkevide.Count > 1)
            {
                StartCoroutine("FindTaettesteObjekt");
                interaktionsobjekt.GetComponent<MasterStation>().playerPickup = objekthold;
                interaktionsobjekt.GetComponent<MasterStation>().player = this.gameObject;
                interaktionsobjekt.GetComponent<MasterStation>().Activate();
            }
            
            else
            {
                holderObjekt = false;
                objekthold.transform.parent = null;
                objekthold.GetComponent<Rigidbody>().useGravity = true;
                objekthold.GetComponent<Rigidbody>().isKinematic = false;
                objekthold = null;
            }
           
        }
        interaktionsobjekt = null;
        
    }

    void AktiverStation()
    {

    }

    void SamlOp()
    {
        holderObjekt = true;
        objekthold = interaktionsobjekt;
        objekthold.transform.position = PickUpHolder.position;
        objekthold.transform.parent = PickUpHolder;
        objekthold.GetComponent<Rigidbody>().useGravity = false;
        objekthold.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Smid()
    {
        holderObjekt = false;
        objekthold.transform.parent = null;
        objekthold.GetComponent<Rigidbody>().useGravity = true;
        objekthold.GetComponent<Rigidbody>().isKinematic = false;
        objekthold = null;
    }
    
    // Boevling
    void OnBoevle()
    {
        print("Boevler");
    }

    //Objekt kommer inden for raekkevidde
    private void OnTriggerEnter(Collider other)
    {
        if(!iRaekkevide.Contains(other.gameObject) && (other.gameObject.CompareTag("Station") || (other.gameObject.CompareTag("PickUp"))))
            iRaekkevide.Add(other.gameObject);   
    }

    //Objekt forlader raekkevidde
    private void OnTriggerExit(Collider other)
    {
        if(iRaekkevide.Contains(other.gameObject))
            iRaekkevide.Remove(other.gameObject);   
    }

    //Find taetteste objekt
    IEnumerator FindTaettestObjekt()
    {
            // Find taetteste objekt fra listen af objekter i r�kkevidde
            float kortestafstand = Mathf.Infinity;
            foreach (GameObject ting in iRaekkevide)
            {
                //Debug line
                Debug.DrawLine(ting.transform.position, this.transform.position, Color.red, 3);

                //Find afstand mellem objekt og spiller
                float afstand = Vector3.Distance(ting.transform.position, this.transform.position);
                           
                //Hvis afstanden er kortere end den korteste afstand saa saet den korteste afstand til den nye afstand
                if (afstand <= kortestafstand)
                {
                    kortestafstand = afstand;
                    interaktionsobjekt = ting;
                    print("Co-Routine Finished");
                }
            }
            yield return interaktionsobjekt;
    }
}