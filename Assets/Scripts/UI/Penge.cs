using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Penge : MonoBehaviour
{
    public int gold;
    public Text goldtext;
    GameObject currencyUI;

    void Update()
    {
       if (gold > 0)
       {
           gold = gold ++;
       } 
       else
       {
           gold = 0;
       }
       DisplayGold(gold);
    }
    void DisplayGold(float goldToDisplay)
    {

    goldtext.text = gold.ToString();    
    
    }
}
