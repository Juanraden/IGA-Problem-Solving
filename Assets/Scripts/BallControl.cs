﻿using System.Collections;
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
    }

    // Update is called once per frame
    void Update()
    {
        MoveBall();

        //ArrowMovement();

        ClickMovement();
    }

    void MoveBall()
    {
        rigidBody2D.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
        }
    }

    private void ArrowMovement()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction += Vector2.left;

            if (direction == Vector2.zero || direction.x < Vector2.left.x)
                direction = Vector2.left;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction += Vector2.down;

            if (direction == Vector2.zero || direction.y < Vector2.down.y)
                direction = Vector2.down;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction += Vector2.right;

            if (direction == Vector2.zero || direction.x > Vector2.right.x)
                direction = Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction += Vector2.up;

            if (direction == Vector2.zero || direction.y > Vector2.up.y)
                direction = Vector2.up;
        }
    }

    private void ClickMovement()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currentPos = transform.position;

        if (Input.GetMouseButtonDown(0))
            if (Vector2.Distance(mousePos, currentPos) > 0.1f)
                direction = (mousePos - currentPos).normalized;
    }
}
