using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellPlayer : MonoBehaviour
{
    [SerializeField] private LayerMask m_TankMask;

    [SerializeField] private ParticleSystem m_ExplosionParticles;

    [SerializeField] private AudioSource m_ExplosionAudio;
    [SerializeField] private float m_Damage;
    [SerializeField] private float m_MaxLifeTime = 2f;
    [SerializeField] private float m_ExplosionForce = 1000f;
    [SerializeField] private float m_ExplosionRadius = 5f; 

    private void Start ()
    {
        // If it isn't destroyed by then, destroy the shell after it's lifetime.
        Destroy (gameObject, m_MaxLifeTime);
    }
    private void OnTriggerEnter (Collider other)
    {
        // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
        Collider[] colliders = Physics.OverlapSphere (transform.position, m_ExplosionRadius, m_TankMask);

        // Go through all the colliders...
        for (int i = 0; i < colliders.Length; i++)
        {
            // ... and find their rigidbody.
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody> ();

            // If they don't have a rigidbody, go on to the next collider.
            if (!targetRigidbody)
                continue;

            // Add an explosion force.
            targetRigidbody.AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius);

            // Find the TankHealth script associated with the rigidbody.
            HealthEnemyTank targetHealth = targetRigidbody.GetComponent<HealthEnemyTank> ();

            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (!targetHealth)
                continue;
            // Deal this damage to the tank.
            targetHealth.TakeDamage(m_Damage);
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody> ();

            if (!targetRigidbody)
                continue;

            targetRigidbody.AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius);

            CarWithHealth targetHealth = targetRigidbody.GetComponent<CarWithHealth> ();

            if (!targetHealth)
                continue;

            targetHealth.TakeDamage(m_Damage);
        }

        // Unparent the particles from the shell.
        m_ExplosionParticles.transform.parent = null;

        // Play the particle system.
        m_ExplosionParticles.Play();

        // Play the explosion sound effect.
        m_ExplosionAudio.Play();

        // Once the particles have finished, destroy the gameobject they are on.
        ParticleSystem.MainModule mainModule = m_ExplosionParticles.main;
        Destroy (m_ExplosionParticles.gameObject, mainModule.duration);

        // Destroy the shell.
        Destroy (gameObject);
    }
}
