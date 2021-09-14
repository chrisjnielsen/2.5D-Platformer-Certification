using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    Player _player;
    [SerializeField]
    private GameObject _standPosR, _standPosL;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _player = other.gameObject.GetComponent<Player>();
            if(_player != null)
            {
                _player.ReachedLadder();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player _player = other.GetComponent<Player>();
            if (_player != null)
            {
                _player.ClimbUpLadder(this);
            }
        }
    }
    public Vector3 GetStandPosR()
    {
        return _standPosR.transform.position;
    }

    public Vector3 GetStandPosL()
    {
        return _standPosL.transform.position;
    }

}
