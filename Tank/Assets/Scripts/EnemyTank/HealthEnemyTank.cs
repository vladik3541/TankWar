using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemyTank : MonoBehaviour
{
    public float _Health;
    [SerializeField] private ParticleSystem _TankExplosion;

    public void TakeDamage(float damage) {
        _Health -= damage;

        if(_Health <= 0)
        {
            OnDeath();
            
        }
    }

    private void OnDeath() {

        _TankExplosion.transform.parent = null;

        _TankExplosion.Play();

        ParticleSystem.MainModule mainModule = _TankExplosion.main;
        Destroy (_TankExplosion.gameObject, mainModule.duration);

        Destroy(gameObject);

        
    }
}
