using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float chanceToSpawn = 50; // only variable used in this class is the "chanceToSpawn" float variable

    void Start()
    {
        if (Random.Range(1, 100) > chanceToSpawn) // chance to spawn the coin
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.coins++;
            Destroy(this.gameObject);
        }
    }
}
