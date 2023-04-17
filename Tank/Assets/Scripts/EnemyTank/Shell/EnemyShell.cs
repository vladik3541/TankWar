using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShell : MonoBehaviour
{
    [SerializeField] private LayerMask _TankMask;

    [SerializeField] private ParticleSystem _ExplosionParticles;

    [SerializeField] private AudioSource _ExplosionAudio;
    [SerializeField] private float _Damage;
    [SerializeField] private float _MaxLifeTime = 2f;
    [SerializeField] private float _ExplosionRadius = 5f; 

    private void Start ()
    {
        // If it isn't destroyed by then, destroy the shell after it's lifetime.
        Destroy (gameObject, _MaxLifeTime);
    }
    private void OnTriggerEnter (Collider other)
    {
        // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
        Collider[] colliders = Physics.OverlapSphere (transform.position, _ExplosionRadius, _TankMask);

        // Go through all the colliders...
        for (int i = 0; i < colliders.Length; i++)
        {
            // ... and find their rigidbody.
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody> ();

            // If they don't have a rigidbody, go on to the next collider.
            if (!targetRigidbody)
                continue;

            // Find the TankHealth script associated with the rigidbody.
            HealthPlayer targetHealth = targetRigidbody.GetComponent<HealthPlayer> ();

            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (!targetHealth)
                continue;
            // Deal this damage to the tank.
            targetHealth.TakeDamage(_Damage);
        }
        // Unparent the particles from the shell.
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
