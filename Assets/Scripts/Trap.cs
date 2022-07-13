using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] float chanceToSpawn =25;
    Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();


        if (Random.Range(1, 100) > chanceToSpawn) // chance to spawn the trap
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.KnockBack();
        }
    }
}
