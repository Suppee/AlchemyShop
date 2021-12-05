using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float force = 10;
    [SerializeField] private float rotationForce = 600;
    [SerializeField] private float secondsBeforeHoming = 1;
    [SerializeField] private float launchForce;
    private Rigidbody rb;
    private bool shouldFolow;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();  
        StartCoroutine(WaitBeforeHoming());  
        target = GameObject.Find("PF_Alchemist").gameObject.transform;

    }


    void Update()
    {
        if (shouldFolow)
        {
            if (target != null)
            {
                Vector3 direction = target.position - rb.position;
                direction.Normalize();
                Vector3 rotarionAmount = Vector3.Cross(transform.forward, direction);
                rb.angularVelocity = rotarionAmount * rotationForce;
                rb.velocity = transform.forward * force;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.enabled = false;
    }

    private IEnumerator WaitBeforeHoming()
    {
        rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
        yield return new WaitForSeconds(secondsBeforeHoming);
        shouldFolow = true;
    }
}
