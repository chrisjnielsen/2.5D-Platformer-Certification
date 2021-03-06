using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField]
    private GameObject _handPosObject;
    [SerializeField]
    private GameObject _standPosObject;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ledge_Grab_Checker"))
        {
            Player player = other.transform.parent.GetComponent<Player>();
            if (player != null)
            {
                player.GrabLedge(_handPosObject, this);
            }
        }
    }

    public GameObject GetStandPos()
    {
        return _standPosObject;
    }
}
