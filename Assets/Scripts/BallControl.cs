using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private Vector2 direction;

    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        direction = Random.insideUnitCircle.normalized;

        Invoke("MoveBall", 1);
    }

    void MoveBall()
    {
        rigidBody2D.velocity = direction * speed;
    }
}
