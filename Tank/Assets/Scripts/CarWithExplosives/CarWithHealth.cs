using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWithHealth : MonoBehaviour
{
    [SerializeField]private float _Health;
    private CarExplosivesPlayer _CarExplosivesPlayer;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        _CarExplosivesPlayer = GetComponent<CarExplosivesPlayer>();
    }

    public void TakeDamage(float damage) {
        _Health -= damage;

        if(_Health <= 0)
        {
            OnDeath();
            
        }
    }

    private void OnDeath() {
        _CarExplosivesPlayer.ExplosivesCar();
    }
}
