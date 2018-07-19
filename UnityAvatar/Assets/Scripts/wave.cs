using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave : MonoBehaviour {

    public GameObject KI;
    Animator _animator;
    Rigidbody rb;
    bool _wave;
    Vector3 _pos;
    // Use this for initialization
    void Start()
    {
        _animator = KI.GetComponentInChildren<Animator>();

        _pos = KI.transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {

       

        _animator.SetBool("dance", false);

        _animator.SetBool("walking", false);
       
        _animator.SetBool("wave", true);

       KI.transform.position = _pos;



    }
}
