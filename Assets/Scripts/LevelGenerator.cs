using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Transform[] levelParts;
    private GameObject player;

    public Vector3 nextPartPosition;

    public float nextPartDrawDistance;
    public float partDeleteDistance;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        DeletePart();
        GeneratePart();
    }

    private void GeneratePart()
    {
        while ((nextPartPosition.x - player.transform.position.x) < nextPartDrawDistance)
        {
            Transform part = levelParts[Random.Range(0, levelParts.Length)];
            Transform newPart = Instantiate(part, nextPartPosition - part.Find("Start Point").position, transform.rotation, transform);

            nextPartPosition = newPart.Find("End Point").position;

        }
    }

    private void DeletePart()
    {
        if (transform.childCount > 0)
        {
            Transform part = transform.GetChild(0);

            Vector3 distance = player.transform.position - part.position;

            if (distance.x > partDeleteDistance)
            {
                Destroy(part.gameObject);
            }
        }
    }
}
