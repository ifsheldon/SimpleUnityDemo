﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBounce : MonoBehaviour
{
    public static int hit = 0;

    public static int destroyed = 0;

    // Start is called before the first frame update
    void Start()
    {
        const float minForce = 2.0f;
        const float maxForce = 3.0f;
        int rand = Random.Range(-1, 2);
        Vector2 direction = new Vector2(0, rand);
        float magnitude = Random.Range(minForce, maxForce);
        GetComponent<Rigidbody2D>().AddForce(direction * magnitude, ForceMode2D.Force);
        print(direction.y);
    }


    void OnBecameInvisible()
    {
        destroyed++;
        Destroy(gameObject);
    }


    void Update()
    {
        // ugly, temp
        gameObject.transform.rotation = Quaternion.identity;
    }

    void OnMouseDown()
    {
        if (transform.position.y <= 0)
        {
            Destroy(gameObject);
            hit++;
        }
        //        print("click");
    }


}