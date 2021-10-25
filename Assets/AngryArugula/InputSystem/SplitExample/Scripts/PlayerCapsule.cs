using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCapsule : MonoBehaviour
{
    public float speed = 5;
    CharacterController controller;
    Vector3 direction = Vector3.zero;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }


    public void OnMove(InputValue input)
    {
        Vector2 vec = input.Get<Vector2>();
        direction = new Vector3(vec.x, 0, vec.y) * speed;
    }

    private void FixedUpdate()
    {
        controller.SimpleMove(direction);
    }
}
