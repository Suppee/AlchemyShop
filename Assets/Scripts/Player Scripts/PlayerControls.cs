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
    
    // Interger Variabler 
    public List<GameObject> iRaekkevide;    // Objekter indenfor raekkevidde af spilleren    
    public GameObject interaktionsobjekt;   // Det objekt man forsøger at interger med
    public bool holderObjekt;               // Bool til at tjekke om man holdet et objekt eller ej
    public Transform PickUpHolder;          // Placeringen af objektet på spilleren
    public GameObject objekthold;           // Objektet man holder
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nyPosition = new Vector3(bevaegelseX, 0.0f, bevaegelseY);

        // Vend mod ny position (Pej fremad) og bevæg mod ny position
        transform.LookAt(nyPosition + transform.position);
        transform.Translate(nyPosition * bevaegelsesFart * Time.deltaTime, Space.World);
    }

    void OnMove(InputValue bevaegelseVaerdi)
    {
        Vector2 bevaegelsesVector = bevaegelseVaerdi.Get<Vector2>();

        bevaegelseX = bevaegelsesVector.x;
        bevaegelseY = bevaegelsesVector.y;

        Debug.Log("Bevægelse");        
    }
    
    //Interger med objekt
    void OnInteract()
    {
        if(iRaekkevide.Count > 0)
        {
            StartCoroutine("FindTaettestObjekt");
        
            //Interger med Station
            if (interaktionsobjekt.tag.Contains("Station"))
            {
                if ((holderObjekt == true && !interaktionsobjekt.tag.Contains("Ingredient")) || (holderObjekt == false))
                    AktiverStation();               
                else
                    Debug.Log("Kan ikke aktiver" + interaktionsobjekt);
            }
            //Interger med Pick Up
            else if (interaktionsobjekt.tag.Contains("PickUp"))
            {
                if (holderObjekt == true)
                    Smid();
                else
                    objekthold = interaktionsobjekt;
                    SamlOp();                     
            }
        }
        else
            Smid();
        interaktionsobjekt = null;      
    }

    //Aktiver Station
    void AktiverStation()
    {
        Debug.Log(this.gameObject + "Aktiver Station");
        interaktionsobjekt.GetComponent<MasterStation>().spillerref = this;    
        interaktionsobjekt.GetComponent<MasterStation>().Activate();
    }

    // Saml objekt op
    public void SamlOp()
    {
        Debug.Log("Ting samlet op");
        holderObjekt = true;
        objekthold.transform.position = PickUpHolder.position;
        objekthold.transform.parent = PickUpHolder;
        objekthold.GetComponent<MeshCollider>().enabled = false;        
        objekthold.GetComponent<Rigidbody>().useGravity = false;
        objekthold.GetComponent<Rigidbody>().isKinematic = true;
        iRaekkevide.Remove(objekthold);
    }

    // Smid objekt i hånden
    public void Smid()
    {
        Debug.Log("Ting smidt");
        holderObjekt = false;
        objekthold.transform.parent = null;        
        objekthold.GetComponent<MeshCollider>().enabled = true;
        objekthold.GetComponent<Rigidbody>().useGravity = true;
        objekthold.GetComponent<Rigidbody>().isKinematic = false;
        iRaekkevide.Add(objekthold);
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
        Debug.Log(other+ "inden raekkevidde");
        if(!iRaekkevide.Contains(other.gameObject) && (other.gameObject.tag.Contains("Station") || (other.gameObject.tag.Contains("PickUp"))))
            iRaekkevide.Add(other.gameObject);   
    }

    //Objekt forlader raekkevidde
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other + "udenfor raekkevidde");
        if (iRaekkevide.Contains(other.gameObject))
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