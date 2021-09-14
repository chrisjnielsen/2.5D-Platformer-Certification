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
    private Vector3 _direction;
    [SerializeField]
    private float _horizontalInput, _verticalInput;
    private Animator _anim;
    [SerializeField]
    private bool _jumping;
    private bool _onLedge;
    private Ledge _activeLedge;
    private int _coins=0;
    private UIManager _uiManager;
    private Ladder _activeLadder;
    public bool _onLadder, _ladderExit;

    [SerializeField]
    private GameObject _model;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }
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
        if (_controller.isGrounded == true)
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _direction = new Vector3(0, 0, _horizontalInput);
            _anim.SetFloat("Speed", Mathf.Abs(_horizontalInput));

            if (_horizontalInput != 0)
            {
                Vector3 facing = _model.transform.localEulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180;
                _model.transform.localEulerAngles = facing;
            }

            if (_jumping)
            {
                _jumping = false;
                _anim.SetBool("Jumping", _jumping);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _direction.y = _jumpHeight;
                _jumping = true;
                _anim.SetBool("Jumping", _jumping);
            }
        }
        else if (_onLadder)
        {
            transform.rotation = Quaternion.Euler(7f, 0, 0);
            _verticalInput = Input.GetAxisRaw("Vertical");
            _direction = new Vector3(0, _verticalInput, 0);
            _anim.SetFloat("VertiSpeed", _verticalInput);
        }
        else
        {
            _direction.y -= _gravity * Time.deltaTime;
        }
        _controller.Move(_direction * _speed * Time.deltaTime);
    }

    public void GrabLedge(GameObject handPos, Ledge currentLedge)
    {
        _controller.enabled = false;
        _anim.SetBool("GrabLedge", true);
        _anim.SetFloat("Speed", 0f);
        _anim.SetBool("Jumping", false);
        _onLedge = true;
        transform.position = handPos.transform.position;
        _activeLedge = currentLedge;
    }

    public void ClimbUpComplete()
    {
        transform.position = _activeLedge.GetStandPos().transform.position;
        _anim.SetBool("GrabLedge", false);
        _controller.enabled = true;
    }

    public void AddCoins()
    {
        _coins++;
        _uiManager.UpdateCoinDisplay(_coins);
    }

    public void ReachedLadder()
    {
        _onLadder = !_onLadder;
        _anim.SetBool("ReachedLadder", _onLadder);
    }

    public void ClimbUpLadder(Ladder currentLadder)
    {
        _anim.SetTrigger("UseLadder");
        _controller.enabled = false;
        _activeLadder = currentLadder;
    }

    public void LadderComplete()
    {
        if (_model.transform.localEulerAngles.y < 60)
        {
            transform.position = _activeLadder.GetStandPosR();
        }
        else
        {
            transform.position = _activeLadder.GetStandPosL();
        }
        _anim.SetBool("ReachedLadder", false);
        _controller.enabled = true;
        _onLadder = false;
    }


    /*public void RollComplete()
    {
        if (_model.transform.localEulerAngles.y < 60)
        {
            transform.position = _finishRollPosR.transform.position;
        }
        else
        {
            transform.position = _finishRollPosL.transform.position;
        }
        _controller.enabled = true;
    }*/
    




}
