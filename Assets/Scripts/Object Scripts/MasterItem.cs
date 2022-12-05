using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterItem : ScriptableObject
{
    new public string name = "New Item";
    public Mesh model;
    public Material material;
    public Texture icon;
    public AudioClip sound;
    public GameObject particle;

    public void activeparticlesat(GameObject here)
    {
        Instantiate(particle, here.transform );
    }
}
