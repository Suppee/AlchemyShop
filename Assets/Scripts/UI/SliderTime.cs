using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTime : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider timerSlider;
    public float gameTime;
    public GameObject fill;
    private bool stopTimer;
    public Gradient g;

    public void Start()
    {
        g = new Gradient();
        GradientColorKey[] colorKey = new GradientColorKey[3];
        colorKey[0].color = Color.red;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.yellow;
        colorKey[1].time = 0.5f;
        colorKey[2].color = Color.green;
        colorKey[2].time = 1.0f;
        var alphaKey = new GradientAlphaKey[0];
        g.SetKeys(colorKey, alphaKey);
    }

    public void OnStart()
    {
        stopTimer = false;
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;        
        Canvas.ForceUpdateCanvases();
    }    

    // Update is called once per frame
    void Update()
    {
        gameTime -= Time.deltaTime;
        fill.GetComponent<Image>().color = g.Evaluate(timerSlider.value / timerSlider.maxValue);
        if (gameTime <= 0)
        {
            
            stopTimer = true;
            gameTime = 0;
            this.gameObject.GetComponentInParent<OrderSetupScript>().DeleteOrder();
            this.enabled = false;

        }
        else if (stopTimer == false)
        {
            timerSlider.value = gameTime;
        }

        
        
    }
}
