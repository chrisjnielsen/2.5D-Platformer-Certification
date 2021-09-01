using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController _controller;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 1.0f;
    [SerializeField]
    private float _jumpHeight = 15.0f;
    private float _yVelocity;
    private bool _canDoubleJump = false;
    private Vector3 _direction, _velocity;
    [SerializeField]
    private float _horizontalInput;
    private Animator _anim;
    private bool _jumping = false;
    private bool _onLedge;
    private Ledge _activeLedge;
    

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();

    }

    void Update()
    {
        CalculateMovement();

        if (_onLedge == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _anim.SetTrigger("ClimbUp");
            }
        }
     
    }

    void CalculateMovement()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        if (_controller.isGrounded == true)
        {
            _direction = new Vector3(0, 0, _horizontalInput);
            _velocity = _direction * _speed;
            _anim.SetFloat("Speed", Mathf.Abs(_horizontalInput));

            if (_horizontalInput != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180;
                transform.localEulerAngles = facing;
            }

            if (_jumping == true)
            {
                _jumping = false;
                _anim.SetBool("Jumping", false);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
                _jumping = true;
                _anim.SetBool("Jumping", true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_canDoubleJump == true)
                {
                    _yVelocity += _jumpHeight;
                    _anim.SetBool("DoubleJump", false);
                    _canDoubleJump = false;
                }
            }
            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);
    }

    public void GrabLedge(Vector3 handPos, Ledge currentLedge)
    {
        _controller.enabled = false;
        _anim.SetBool("GrabLedge", true);
        _anim.SetFloat("Speed", 0f);
        _anim.SetBool("Jumping", false);
        _onLedge = true;
        transform.position = handPos;
        _activeLedge = currentLedge;
    }

    public void ClimbUpComplete()
    {
        transform.position = _activeLedge.GetStandPos();
        _anim.SetBool("GrabLedge", false);
        _controller.enabled = true;
    }
}
