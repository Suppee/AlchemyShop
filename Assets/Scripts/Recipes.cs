using UnityEngine;

//Her laver vi referencen til filmenuen
[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipes : ScriptableObject
{
    new public string name = "New recipe";
    public Mesh model;
    public GameObject[] ingredients;
}