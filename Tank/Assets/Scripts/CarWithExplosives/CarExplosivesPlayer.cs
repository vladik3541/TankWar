using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarExplosivesPlayer : MonoBehaviour
{
    [SerializeField] private LayerMask _TankMask;

    [SerializeField] private ParticleSystem _ExplosionParticles;

    [SerializeField] private AudioSource _ExplosionAudio;

    [SerializeField] private float _Damage;
    [SerializeField] private float _ExplosionForce = 1000f;
    [SerializeField] private float _ExplosionRadius; 

    public void ExplosivesCar()
    {
        
        Collider[] colliders = Physics.OverlapSphere (transform.position, _ExplosionRadius, _TankMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody> ();

            if (!targetRigidbody)
                continue;

            targetRigidbody.AddExplosionForce (_ExplosionForce, transform.position, _ExplosionRadius);

            HealthPlayer targetHealth = targetRigidbody.GetComponent<HealthPlayer> ();

            if (!targetHealth)
                continue;
            
            targetHealth.TakeDamage(_Damage);
        }
        for(int i = 0; i < colliders.Length; i++) {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody> ();

            if (!targetRigidbody)
                continue;

            targetRigidbody.AddExplosionForce (_ExplosionForce, transform.position, _ExplosionRadius);

            HealthEnemyTank enemyTargetTank = targetRigidbody.GetComponent<HealthEnemyTank>();

            if (!enemyTargetTank)
                continue;

            enemyTargetTank.TakeDamage(_Damage);
        }

        _ExplosionParticles.transform.parent = null;

        // Play the particle system.
        _ExplosionParticles.Play();

        // Play the explosion sound effect.
        _ExplosionAudio.Play();

        // Once the particles have finished, destroy the gameobject they are on.
        ParticleSystem.MainModule mainModule = _ExplosionParticles.main;
        Destroy (_ExplosionParticles.gameObject, mainModule.duration);

        // Destroy the shell.
        Destroy (gameObject);
            
    }
}
