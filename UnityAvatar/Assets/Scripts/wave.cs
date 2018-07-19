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
    private void OnEnable()
    {
        _animator = KI.GetComponentInChildren<Animator>();

        _pos = KI.transform.position;

        _animator.SetBool("dance", false);

        _animator.SetBool("walking", false);

        _animator.SetBool("wave", true);
    }

    // Update is called once per frame
    void Update()
    {


       KI.transform.position = _pos;



    }
}
