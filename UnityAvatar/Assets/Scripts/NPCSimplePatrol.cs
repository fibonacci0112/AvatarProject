using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class NPCSimplePatrol : MonoBehaviour
{
    //Dictates whether the agent waits on each node.
    
    bool _patrolWaiting = true;

    //The total time we wait at each node.
    [SerializeField]
    float _totalWaitTime = 3f;

    //The probability of switching direction.
    [SerializeField]
    float _switchProbability = 0.2f;

    //The list of all patrol nodes to visit.
    [SerializeField]
   // List<WayPoint> _patrolPoints;
    List<GameObject> _patrolPoints = new List<GameObject>();

    //Private variables for base behaviour.
    NavMeshAgent _navMeshAgent;
    int _currentPatrolIndex;
    bool _travelling;
    bool _waiting;
    bool _patrolForward;
    float _waitTimer;
    GameObject _npc;
    private float timer = 0.5f;

    Animator n_animator;
    // Use this for initialization
    public void Start()
    {

        n_animator = GetComponentInChildren<Animator>();
        _patrolPoints.AddRange(GameObject.FindGameObjectsWithTag("PlayerPoint"));
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _npc = this.gameObject;
        if (_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component is not attached to " + gameObject.name);
        }
        else
        {
            if (_patrolPoints != null && _patrolPoints.Count >= 0)
            {
                _currentPatrolIndex = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("Insufficient patrol points for basic patrolling behaviour.");
            }

        }

       
    }

    public void Update()
    {
        //Check if we're close to the destination.
        if (_travelling && _navMeshAgent.remainingDistance <= 1.0f)
        {
            
            
        



            //If we're going to wait, then wait.
            if (_patrolWaiting)
            {
                _waiting = true;
                _waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }

        }
        

        //Instead if we're waiting.
        if (_waiting)
        {
            n_animator.SetBool("dance", false);
            n_animator.SetBool("wave", false);
            n_animator.SetBool("walking", false);
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _totalWaitTime)
            {
               
                _waiting = false;

                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if (_patrolPoints != null)
        {
            Vector3 targetVector = _patrolPoints[_currentPatrolIndex].transform.position;
            _navMeshAgent.SetDestination(targetVector);
            _travelling = true;
           
            if (_travelling)
            {
                

                n_animator.SetBool("walking", true);
                

            }

        }
    }

    /// <summary>
    /// Selects a new patrol point in the available list, but
    /// also with a small probability allows for us to move forward or backwards.
    /// </summary>
    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= _switchProbability)
        {
            _patrolForward = !_patrolForward;
        }

        if (_patrolForward)
        {
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Count;
        }
        else
        {
            if (--_currentPatrolIndex < 0)
            {
                _currentPatrolIndex = _patrolPoints.Count - 1;
            }
        }
    }
}
