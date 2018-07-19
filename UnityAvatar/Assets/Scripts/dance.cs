using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dance : MonoBehaviour {

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
        _wave = true;

        
    }

    // Update is called once per frame
    void Update()
    {

        

        _animator.SetBool("walking", false);

        _animator.SetBool("wave", false);

        _animator.SetBool("dance", true);

        KI.transform.position = _pos;



    }
}
