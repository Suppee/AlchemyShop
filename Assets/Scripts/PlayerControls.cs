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
        
       




    }


    void OnInteract()
    {

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
