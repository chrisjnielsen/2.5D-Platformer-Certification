using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float _speed = 20f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 5 * _speed * Time.deltaTime, 0);   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.AddCoins();
            }

            Destroy(this.gameObject);
        }

    }

}
