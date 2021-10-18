using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class KundeOrder : MasterStation
{

    bool orderIgang;
    public List<Texture> order;
    public List<Recipes> opskriftListe;
    public GameObject[] vare;
    public List<Recipes> aktivorder;

    //Texture

    // Start is called before the first frame update
    void Start()
    {

        //Find alle opskrifter
        string[] opskriftlokationer = AssetDatabase.FindAssets("t:scriptableobject", new[] { "Assets/Scripts/Recipes" });
        opskriftListe.Clear();
        foreach (string opskriftstreng in opskriftlokationer)
        {
            var opskriftSti = AssetDatabase.GUIDToAssetPath(opskriftstreng);
            var opskrift = AssetDatabase.LoadAssetAtPath<Recipes>(opskriftSti);
            opskriftListe.Add(opskrift);

        }

            InvokeRepeating("SkabNyOrdre", 0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Activate()
    {
        if(orderIgang == true)
        {
            //Aktiv order i gang


        }
        else
        {
            //Ingen order i gang
            print("Tag en order");
            orderIgang = true;
        }
        
    }

    public void SkabNyOrdre()
    {
        //Reset UI
        foreach (GameObject v in vare)
        {
            v.GetComponent<RawImage>().texture = null;
        }

        int orderstroelse = Random.Range(1, 4);

        for(int i = 0; i < orderstroelse; i++)
        {
            int index = Random.Range(0, opskriftListe.Count);
            print(opskriftListe[index]);
           
            vare[i].GetComponent<RawImage>().texture = opskriftListe[index].texture;

        }
    }
}
