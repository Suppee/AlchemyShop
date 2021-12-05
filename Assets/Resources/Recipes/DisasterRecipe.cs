using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Her laver vi referencen til filmenuen
[CreateAssetMenu(fileName = "New Disaster Recipe", menuName = "Disaster Recipe")]
public class DisasterRecipe : MasterRecipe
{
    public MonoBehaviour Effect;
}
