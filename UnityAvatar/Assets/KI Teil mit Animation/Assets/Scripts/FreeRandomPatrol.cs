using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreeRandomPatrol : MonoBehaviour {
    Animator n_animator;
    bool walking = false;
    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;
    public NavMeshSurface surface;

    // Use this for initialization

  public void Start()
    {
        n_animator = GetComponent<Animator>();
        surface.BuildNavMesh();
    }
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer * 10;
    }

    // Update is called once per frame
    void Update()
    {
        

        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            n_animator.SetBool("walking", true);
            agent.SetDestination(newPos);
            timer = 0;

            
        }
       
       


       
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {

        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;
        

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
  
}
