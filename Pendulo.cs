﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulo : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public float leftPushRange;
    public float rightPushRange;
    public float velocityThreshold;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.angularVelocity = velocityThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        Push();
    }
    public void Push()
    {
        if (transform.rotation.z > 0
        && transform.rotation.z < rightPushRange
        && (rigidbody2D.angularVelocity > 0)
        && rigidbody2D.angularVelocity < velocityThreshold)
        {
            rigidbody2D.angularVelocity = velocityThreshold;
        }
        else if (transform.rotation.z < 0
       && transform.rotation.z > rightPushRange
       && (rigidbody2D.angularVelocity < 0)
       && rigidbody2D.angularVelocity > velocityThreshold * -1)
        {
            rigidbody2D.angularVelocity = velocityThreshold * -1;
        }

    }

}
