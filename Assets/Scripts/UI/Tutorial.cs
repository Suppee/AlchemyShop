using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    private GameObject text;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    public GameObject text5;
    public GameObject text6;
    public GameObject text7;
    public Mover spillerref;
    public Mover spillerref2;
    
    [SerializeField]
    private Penge penge;

    public GameObject Pil_Order;
    public GameObject Pil_Ing;
    public GameObject Pil_Mix;

    private bool start = true;
    private bool control = true;
    private bool step3 = false;
    private bool step4 = false; 
    private bool step6 = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("Start");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("PlayerInput 1(Clone)") && start == true)
        {
            text.SetActive(false);
            StartCoroutine("Step2");
            start = false;
        }

        if(step3)
        {
            text2.SetActive(false);
            Pil_Order.SetActive(true);
            text3.SetActive(true);
            if(GameObject.Find("PF_OrderUI(Clone)") && control == true)
            {
                Pil_Order.SetActive(false);
                text3.SetActive(false);
                text4.SetActive(true);
                Pil_Ing.SetActive(true);
                step4 = true;
                control = false;
                step3 = false;
            } 
        }

        if((spillerref.holderObjekt && step4 == true) || (spillerref2.holderObjekt && step4 == true))
        {
            control = false;
            step4 = false;
            text4.SetActive(false);
            Pil_Ing.SetActive(false);
            text5.SetActive(true);
            Pil_Mix.SetActive(true);  
            step6 = true; 
        }

        if(spillerref.objekthold.tag.Contains("Product") || spillerref2.objekthold.tag.Contains("Product"))
        {
            text5.SetActive(false);
            Pil_Mix.SetActive(false);
            Pil_Order.SetActive(true);
            text6.SetActive(true);
        }
        
        if(penge.gold > 0)
        {
            Pil_Order.SetActive(false);
            text6.SetActive(false);
            text7.SetActive(true);
            StartCoroutine("Slut");
        }
    }

    void Step2()
    {
        text2.SetActive(true);
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(4); 
        step3 = true;
    }

    IEnumerator Slut()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("3DMainMenu");
    }
}
