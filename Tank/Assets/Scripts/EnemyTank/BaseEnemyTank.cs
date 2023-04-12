using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyTank : MonoBehaviour
{
    
    [SerializeField] private GameObject _Trigger;
    [SerializeField] private float _DistansStop = 10;
    [SerializeField] private float _DistansForAttack = 20;

    private EnemyShot _EnemyShot;
    private float dist;
    private bool _StopMove;
    Vector3 _DistansHIt;
    private NavMeshAgent agent;

    private void Start()
    {
        _EnemyShot = GetComponent<EnemyShot>();
        agent = GetComponent<NavMeshAgent>();
        _Trigger = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {
        if(_Trigger)
        {
            MovementTank();
            RotateToTargget(); 
        }
        
    }

    private void MovementTank()
    {
        float dist = Vector3.Distance(_Trigger.transform.position, transform.position);
    
        if(_Trigger != null && _StopMove == false)
        {
            agent.destination = _Trigger.transform.position;
        }
        if(dist <= _DistansStop)
        {
            _StopMove = true;
        }
        else
        {

            _StopMove = false;
        }
        
    }

    private void RotateToTargget() {
        
        
        dist = Vector3.Distance(_Trigger.transform.position, transform.position);

        if(dist < _DistansForAttack)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward * 100, Color.red);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "Player")
                {
                    _EnemyShot.Fire();
                    Vector3 direction = _Trigger.transform.position  - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
                    transform.rotation = rotation;
                    _StopMove = true;
                }   
            }
        }

    }
}
