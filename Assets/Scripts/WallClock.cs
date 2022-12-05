using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WallClock : MonoBehaviour
{
    private Transform clockHandTransform;
    public TMP_Text roundTimerText;

    // Start is called before the first frame update
    void Awake()
    {
        clockHandTransform = transform.Find("ClockHandCanvas");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Spin Clock Arms
        if(GameManager.Instance.curState == GameState.Playing)
            clockHandTransform.eulerAngles = new Vector3(0, 0, -Time.realtimeSinceStartup * 90f);

        // Update Time Text
        DisplayTime(RoundManager.Instance.roundTime);
    }

    public void DisplayTime(float timeToDisplay)
    {

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        roundTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
