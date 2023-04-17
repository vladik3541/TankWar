using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointForCapture : MonoBehaviour
{
    [SerializeField] private bool _PointIsCapture;

    [Header("Flag")]
    [SerializeField] private Material _BlueColor;
    [SerializeField] private Renderer _Flag; 


    [SerializeField] private Image _LoadCircleImage;

    [SerializeField] [Range(0, 50)] float progress = 0f;



    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && !_PointIsCapture)
        {
            progress += Time.deltaTime;
            _LoadCircleImage.fillAmount = progress * 0.02f;
            if(progress >= 50f)
            {
                _PointIsCapture = true;
                _Flag.sharedMaterial = _BlueColor;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && !_PointIsCapture)
        {
            progress = 0;
            _LoadCircleImage.fillAmount = progress;
        }
    }
}
