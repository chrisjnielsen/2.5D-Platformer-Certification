using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMove : MonoBehaviour
{
    [SerializeField]
    private GameObject _origin, _target;
    private bool Switch;
    [SerializeField]
    private float _speed = 3f;
    private float _waitTime, _maxWaitTime;

    void Start()
    {
        _waitTime = 0;
        _maxWaitTime = 5f;
    }

    void FixedUpdate()
    {
            if (transform.position.y <= _target.transform.position.y && Switch == false)
            {
                Switch = true;
                _waitTime = 0.0f;

            }
            if (transform.position.y >= _origin.transform.position.y && Switch == true)
            {
                Switch = false;
                _waitTime = 0.0f;

            }

            if (Switch)
            {
                if (_waitTime >= _maxWaitTime)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _origin.transform.position, _speed * Time.deltaTime);
                }
                else
                {
                    _waitTime += Time.deltaTime;
                }
            }
            else
            {
                if (_waitTime >= _maxWaitTime)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
                }
                else
                {
                    _waitTime += Time.deltaTime;
                }
            }
    }

private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
