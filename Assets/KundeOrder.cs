using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KundeOrder : MasterStation
{

    bool orderIgang;
    public List<Texture> order;
    //Texture

    // Start is called before the first frame update
    void Start()
    {

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

    void SkabNyOrdre()
    {
        int orderstroelse = Random.Range(1, 4);

        for(int i = 0; i < orderstroelse; i++)
        {
            //Tilføj produkt

        }
    }
}
