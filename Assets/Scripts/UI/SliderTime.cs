using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTime : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider timerSlider;
    public float gameTime;

    private bool stopTimer;
    public void OnStart()
    {
        stopTimer = false;
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime -= Time.deltaTime;

        if (gameTime <= 0)
        {
            
            stopTimer = true;
            gameTime = 0;            
            this.gameObject.GetComponentInParent<KundeOrder>().KundePause();
            this.enabled = false;

        }
        else if (stopTimer == false)
        {
            timerSlider.value = gameTime;
        }
    }
}
