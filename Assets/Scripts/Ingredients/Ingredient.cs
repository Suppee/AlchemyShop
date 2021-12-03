using UnityEngine;

//Her laver vi referencen til filmenuen

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Ingredienser" +
    "")]
public class Ingredient : ScriptableObject
{
    new public string name = "New Ingredient";
    public Mesh ingrediensModel;
    public Material material;
    public Texture icon;
}