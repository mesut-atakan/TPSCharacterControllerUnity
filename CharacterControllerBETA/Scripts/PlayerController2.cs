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

    private void Awake()
    {
        //_rb = this.gameObject.GetComponent<Rigidbody>();
        controller = this.gameObject.GetComponent<CharacterController>();
    }

    private void FixedUpdate() 
    {
        CharacterController();
    }
    
    private void CharacterController()
    {
        _playerSpeed = Input.GetKey(KeyCode.LeftShift) ? 4f : !Input.GetKey(KeyCode.LeftControl) ? 2.5f : 1f ;

        //character Movement
        float speed_x = _playerSpeed * Input.GetAxisRaw("Horizontal");
        float speed_z = _playerSpeed * Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(speed_x, 0f, speed_z);

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * _playerSpeed * Time.deltaTime);
        }
        
        //_rb.velocity = new Vector3(speed_x, 0, speed_z);
    }
}


//character Rotation
       /* if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            //controller.Move(direction * _playerSpeed * Time.deltaTime);
        } */


        //JUMP System
        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);
        }*/