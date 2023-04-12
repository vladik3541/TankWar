using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [SerializeField] private Rigidbody _Shell;
    [SerializeField] private Transform _FireTransform;

    [SerializeField] private ParticleSystem _FireEfect;

    [SerializeField] private float _Distansce;

    [SerializeField] AudioClip _FireClip;  
    private  AudioSource _ShootingAudio;
    private bool _Fired;

    private void Start()
    {
       _Fired = true;
       _ShootingAudio = GetComponent<AudioSource>();
    }
    public void Fire() {
        if(_Fired)
        {
            _Fired = false;

            _FireEfect.Play();

            // Change the clip to the firing clip and play it
            _ShootingAudio.clip = _FireClip;
            _ShootingAudio.Play ();

            Rigidbody shellInstance =
                    Instantiate (_Shell, _FireTransform.position, _FireTransform.rotation) as Rigidbody;

            shellInstance.velocity = _Distansce * _FireTransform.forward;
            Invoke("ReadyFire", 2.0f); 
        }
       
    }

    private void ReadyFire()
    {
        _Fired = true;
    }
}
