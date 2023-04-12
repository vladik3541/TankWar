using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShot : MonoBehaviour
{
    [SerializeField] private Rigidbody m_Shell;
    [SerializeField] private Transform m_FireTransform;

    [SerializeField] private ParticleSystem m_FireEfect;

    [SerializeField] private float m_Distansce;

    [SerializeField] AudioClip m_FireClip;  
    private  AudioSource m_ShootingAudio;
    private bool m_Fired;

    private void Start()
    {
       m_Fired = true;
       m_ShootingAudio = GetComponent<AudioSource>();
    }
    private void Update() {
        if(Input.GetButton("Fire"))
        {
            if(m_Fired)
            {
                Fire();
            }
            
        }
    }

    private void Fire() {
        m_Fired = false;

        m_FireEfect.Play();

        // Change the clip to the firing clip and play it
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play ();

        Rigidbody shellInstance =
                Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        shellInstance.velocity = m_Distansce * m_FireTransform.forward;
        Invoke("ReadyFire", 1.1f);
    }

    private void ReadyFire()
    {
        m_Fired = true;
    }
}
