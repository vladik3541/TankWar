using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarExplosivesMovement : MonoBehaviour
{
    [SerializeField] private GameObject _Trigger;
    private float _DistanceForExplosiv = 7;

    private CarExplosivesPlayer _CarExplosivesPlayer;
    private NavMeshAgent agent;

    private void Start()
    {
        _Trigger = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        _CarExplosivesPlayer = GetComponent<CarExplosivesPlayer>();
    }

    private void FixedUpdate() {

        if(_Trigger != null)
            MovementCar();
    }

    private void MovementCar() {
        
        float dist = Vector3.Distance(_Trigger.transform.position, transform.position);

        agent.destination = _Trigger.transform.position;

        if(dist < _DistanceForExplosiv)
        {
            _CarExplosivesPlayer.ExplosivesCar();
        }
    }
}
