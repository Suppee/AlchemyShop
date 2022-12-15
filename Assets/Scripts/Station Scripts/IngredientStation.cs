using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class IngredientStation : BaseStation
{
    // Variables
    GameObject ingrediensPrefab;
    public IngredientItem ingredient;
    GameObject NyIngredient;

    public void OnRenderObject()
    {
        gameObject.transform.GetChild(2).GetComponent<MeshFilter>().mesh = ingredient.model;
        gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material = ingredient.material;
    }

    public void Start()
    {
        ingrediensPrefab = Resources.Load<GameObject>("Prefabs/PF_Item") as GameObject;        
    }

    // Hidkald Prefab
    [Server]
    public override void Activate()
    {
        GameObject NyIngredient = Instantiate(ingrediensPrefab);
        NetworkServer.Spawn(NyIngredient, connectionToClient); 
        NyIngredient.GetComponent<PickUpObject>().itemRef = ingredient;
        Test();
        //spillerref.GetComponent<BasePlayer>().heldObj.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }

    public void Test()
    {
        Debug.Log(spillerref);
        spillerref.GetComponent<BasePlayer>().SamlOp(NyIngredient); 
    }
}
