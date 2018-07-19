using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dance : MonoBehaviour {

    public GameObject KI;
    Animator _animator;
    Rigidbody rb;
    bool _wave;
    Vector3 _pos;


    private void OnEnable()
    {
        _animator = KI.GetComponentInChildren<Animator>();

        _pos = KI.transform.position;
        _wave = true;

        _animator.SetBool("walking", false);

        _animator.SetBool("wave", false);

        _animator.SetBool("dance", true);

    }

    // Update is called once per frame
    void Update()
    {

        

        

        KI.transform.position = _pos;



    }
}
