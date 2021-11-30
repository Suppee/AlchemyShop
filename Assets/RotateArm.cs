using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateArm : MonoBehaviour
{

    private Transform clockHandTransform;

    // Start is called before the first frame update
    void Awake()
    {
        clockHandTransform = transform.Find("ClockHandCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        clockHandTransform.eulerAngles = new Vector3(0, 0, -Time.realtimeSinceStartup * 90f);
    }
}
