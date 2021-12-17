using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField]
    GameObject mover;
    [SerializeField]
    GameObject mover2;
    
    public Animator anim_wiz;
    
    public Animator anim_alc;

    private void Start()
    {
        anim_wiz = mover.GetComponentInChildren<Animator>();
        anim_alc = mover2.GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        anim_wiz.SetBool("isMoving", mover.GetComponent<Mover>().moving);
        anim_alc.SetBool("isMoving", mover2.GetComponent<Mover>().moving);
    }
}
