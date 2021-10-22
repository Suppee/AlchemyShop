using UnityEngine;

//Her laver vi referencen til filmenuen
[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]

public class Recipes : ScriptableObject
{
    new public string name = "New Recipe";
    public Mesh model;
    public Ingredient[] ingredients;
    public Material material;
    public Texture texture;    
    public string Kode;

    void OnValidate()
    {
        Kode = "";
        Kode += ingredients.Length.ToString();
        foreach (Ingredient ing in ingredients)
        {
            Kode += ing.Kode;
        }
    }
}