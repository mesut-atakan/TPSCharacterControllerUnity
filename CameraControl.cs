using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] [Tooltip("Kamera Takip Hızı")] [Range(0,1.5f)] private float CamSpeed = 0.3f;
    [SerializeField] [Tooltip("Takip edilmesi istenilen obje (player)")] private Transform PlayerTR;
    [SerializeField] [Tooltip("kameranın Target a ne kadar uzak olacağı belirlenir.")] private Vector3 distance;

    private void Start() {
        PlayerTR = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void CameraTarget(Transform Target)
    {
        transform.position = Vector3.Lerp(transform.position, Target.transform.position - distance, CamSpeed);
        transform.LookAt(Target);
    }

    private void CameraTransform()
    {
        
    }

    private void FixedUpdate() 
    {
        CameraTarget(PlayerTR);
    }
}
