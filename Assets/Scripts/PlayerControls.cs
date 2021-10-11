using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    public float moveSpeed = 5f;
    float bevægelseX;
    float bevægelseY;
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
        transform.Translate(newPosition * moveSpeed * Time.deltaTime, Space.World);
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
}
