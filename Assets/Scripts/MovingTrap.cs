using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    #region Variables
    [Header("Trap Info")]
    [SerializeField] Transform[] movePoints;
    [SerializeField] int nextPosition;
    [SerializeField] float trapSpeed;
    [SerializeField] float rotationMulipler;
    [SerializeField] float chanceToSpawn = 25f;
    Player player;
    #endregion

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        if (Random.Range(1, 100) > chanceToSpawn)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoints[nextPosition].position, trapSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoints[nextPosition].position) < 0.5f)
        {
            nextPosition++; //++ means +1

            if (nextPosition >= movePoints.Length)
            {
                nextPosition = 0;
            }
        }

        if (transform.position.x > movePoints[nextPosition].position.x)
        {
            transform.Rotate(new Vector3(0, 0, 100 * rotationMulipler * Time.deltaTime));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 100 * -rotationMulipler * Time.deltaTime));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.KnockBack();
        }
    }
}
