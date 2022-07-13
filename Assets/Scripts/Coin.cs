using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float chanceToSpawn = 50;

    void Start()
    {
        if (Random.Range(1, 100) > chanceToSpawn) // chance to spawn the coin
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.coins++;
            Destroy(this.gameObject);
        }
    }
}
