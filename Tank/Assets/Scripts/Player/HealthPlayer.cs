using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private float m_Healt;

    [SerializeField] private Slider m_SliderHP;
    [SerializeField] private ParticleSystem m_TankExplosion;
    public bool m_Death;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        m_SliderHP.value = m_Healt;
    }
    public void TakeDamage(float damage) {
        m_Healt -= damage;

        if(m_Healt <= 0)
        {
            OnDeath();
            
        }
    }

    private void OnDeath() {

        m_Death = true;

        m_TankExplosion.transform.parent = null;

        m_TankExplosion.Play();

        ParticleSystem.MainModule mainModule = m_TankExplosion.main;
        Destroy (m_TankExplosion.gameObject, mainModule.duration);

        Destroy(gameObject);

        
    }
}
