using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;


    private Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        position = transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 camPos = cam.transform.position;
        float temp = camPos.x * (1 - parallaxEffect);
        float distance = camPos.x * parallaxEffect;
        position = new Vector3(startpos+distance, position.y, position.z);
        transform.position = position;

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}
