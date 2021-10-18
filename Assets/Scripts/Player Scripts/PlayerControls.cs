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
    GameObject objekthold;
    

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
        if(holderObjekt==false)
        {

            // Find taetteste objekt fra listen af objekter i r�kkevidde
            float kortestafstand = Mathf.Infinity;
            foreach (GameObject ting in iRaekkevide)
            {

                //Find afstand mellem objekt og spiller
                float afstand = Vector3.Distance(ting.transform.position, this.transform.position);

                //Debug line
                Debug.DrawLine(ting.transform.position, this.transform.position, Color.red, 3);

                //Hvis afstanden er kortere end den korteste afstand saa saet den korteste afstand til den nye afstand
                if (afstand <= kortestafstand)
                {
                    kortestafstand = afstand;
                    interaktionsobjekt = ting;
                }

            }

            //Interger med objekt
            if (interaktionsobjekt.CompareTag("Station"))
            {
                Debug.Log("Spawn fra Workstation");
                //Aktiver station
                interaktionsobjekt.GetComponent<MasterStation>().Activate();
            }
            else if (interaktionsobjekt.CompareTag("PickUp"))
            {
                Debug.Log("Ingrediens samlet op");

                // Saml objekt op

                interaktionsobjekt.transform.position = PickUpHolder.position;
                interaktionsobjekt.transform.parent = PickUpHolder;

                holderObjekt = true;
                objekthold = interaktionsobjekt;
                objekthold.GetComponent<Rigidbody>().useGravity = false;
                objekthold.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        else
        {

            //Smid objekt
            objekthold.GetComponent<Rigidbody>().useGravity = true;
            objekthold.GetComponent<Rigidbody>().isKinematic = false;
            objekthold.transform.parent = null;
            holderObjekt = false;
        }
        interaktionsobjekt = null;
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
}
