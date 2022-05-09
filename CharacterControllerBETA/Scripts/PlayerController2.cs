using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent (typeof(Rigidbody))] //geçerli objede rigidbody yok ise rigidbody eklenecek.
[RequireComponent (typeof(CharacterController))] //geçerli objede Character Controller componenti bulunmuyorsa. Bu component eklenecek.
public class PlayerController2 : MonoBehaviour
{
    //private Rigidbody _rb;
    private CharacterController controller;

    //speeds:
    [SerializeField] [Tooltip("Karakterin Geçerli Hızı")] private float _playerSpeed = 0;
    [SerializeField] [Tooltip("Walk Speed")] private float _moveSpeed;
    [SerializeField] [Tooltip("Run Speed")] private float _runSpeed;
    [SerializeField] [Tooltip("Crounch Speed")] private float _crounchSpeed;
    [SerializeField] [Tooltip("Jump Speed")] private float _jumpSpeed;
    private float turnSmoothVelocity;
    
    [Space]
    [Header("Transform")]
    [SerializeField] [Tooltip("Player Rotation Speed")] [Range(0f,1f)] private float turnSmoothTime = 0.1f;
    [SerializeField] [Tooltip("Camera Transform Information")] private Transform Camera;

    private void Awake()
    {
        //_rb = this.gameObject.GetComponent<Rigidbody>();
        controller = this.gameObject.GetComponent<CharacterController>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    private void FixedUpdate() 
    {
        CharacterController();
    }
    
    private void CharacterController()
    {
        _playerSpeed = Input.GetKey(KeyCode.LeftShift) ? 8f : !Input.GetKey(KeyCode.LeftControl) ? 5f : 2.5f ;

        //character Movement
        float speed_x = _playerSpeed * Input.GetAxisRaw("Horizontal");
        float speed_z = _playerSpeed * Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(speed_x, 0f, speed_z);

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * _playerSpeed * Time.deltaTime);
        }
    }
}