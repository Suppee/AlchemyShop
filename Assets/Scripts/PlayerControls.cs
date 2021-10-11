using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    public float bevægelsesFart = 5f;
    float bevægelseX;
    float bevægelseY;
    public List<GameObject> inRange;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(bevægelseX, 0.0f, bevægelseY);

        // Look Towards
        transform.LookAt(newPosition + transform.position);


        // Move
        transform.Translate(newPosition * bevægelsesFart * Time.deltaTime, Space.World);
    }

    void OnMove(InputValue bevægelseVærdi)
    {

        Vector2 bevægelsesVector = bevægelseVærdi.Get<Vector2>();

         bevægelseX = bevægelsesVector.x;
         bevægelseY = bevægelsesVector.y;
        
       




    }


    void OnInteract()
    {

        print("HI");

    }

    void OnBøvle()
    {

        print("Bøvler");

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
