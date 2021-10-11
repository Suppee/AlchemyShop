using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    // Movement Variables

    public Rigidbody rb;
    public float moveSpeed = 5f;
    Vector3 movement;


    // Look towards

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Input
      //  movement.x = Input.GetAxisRaw("Horizontal");
       // movement.z = Input.GetAxisRaw("Vertical");

        // Look towards

        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 newPosition = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.LookAt(newPosition + transform.position);
        transform.Translate(newPosition * moveSpeed * Time.deltaTime, Space.World);
    }

    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed);
    }

    //void OnMove()
}