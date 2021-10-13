using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    public float bevaegelsesFart = 5f;
    float bevaegelseX;
    float bevaegelseY;
    public List<GameObject> inRange;

    //PickUps
    public bool HolderPickUp;
    public GameObject PickUp;
    public Transform Player; 
    
    

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

        /*
        //PickUps
        if (HolderIngrediens == false)
        {
            Ingrediens.transform.SetParent(Player.transform);
        }
        else
        {
            Ingrediens.transform.SetParent(null);
        }
        //
        */

    }

    void OnMove(InputValue bevaegelseVaerdi)
    {

        Vector2 bevaegelsesVector = bevaegelseVaerdi.Get<Vector2>();

         bevaegelseX = bevaegelsesVector.x;
         bevaegelseY = bevaegelsesVector.y;
        
    }
    
    void OnInteract()
    {
        
        print("Interger!");

        print("HI");
        

    }

    void OnBoevle()
    {

        print("Boevler");

    }

    private void OnTriggerEnter(Collider other)
    {
        if(!inRange.Contains(other.gameObject))
        {
            if (other.gameObject.CompareTag("Workstation"))
            {
                inRange.Add(other.gameObject);
                print(other + "in range");

            }
        }

        //PickUps
        if (Input.GetKey("p") && other.gameObject.tag == "PickUpItem")
        {
            gameObject.transform.parent = PickUp.transform;
        }
            
     

    }

    private void OnTriggerExit(Collider other)
    {
        if(inRange.Contains(other.gameObject))
        { 
            inRange.Remove(other.gameObject);
            print(other + "out of range");
        }
    }
}
