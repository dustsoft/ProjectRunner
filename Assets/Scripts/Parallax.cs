using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    #region Variables
    GameObject cam;
    float length, startPos;
    public float parallaxEffect;
    #endregion

    void Start()
    {
        cam = GameObject.Find("Main Camera");

        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = (cam.transform.position.x) * (1 - parallaxEffect); // how far we moved relativly to the camera
        float distance = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + length)
        {
            startPos = startPos + length;
        }
        else if (temp < startPos - length)
        {
            startPos = startPos - length;
        }

    }
}