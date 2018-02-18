using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour {

    public int playerSpeed = 10;
    private float moveX;
    private float moveY;
    private float MIN_X;
    private float MIN_Y;
    private float MAX_X;
    private float MAX_Y;
    private Rigidbody2D rb2D;

    private void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        float width = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float height = GetComponent<SpriteRenderer>().bounds.size.y / 2;
        MIN_Y = height;
        MIN_X = width;
        MAX_X = Screen.width - MIN_X;
        MAX_Y = Screen.height - MIN_Y;
    }

    private void FixedUpdate() {
        // CONTROLS
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        // PHYSICS
        // Calculating the velocity for the input
        Vector2 velocity = new Vector2(0, 0);
        if (moveX != 0 && moveY != 0) {
            velocity = new Vector2(moveX * playerSpeed, moveY * playerSpeed);
        } else if (moveX == 0) {
            velocity = new Vector2(0, moveY * playerSpeed);
        } else if (moveY == 0) {
            velocity = new Vector2(moveX * playerSpeed, 0);
        }
        Vector2 position = new Vector2(rb2D.position.x + velocity.x * Time.fixedDeltaTime, rb2D.position.y + velocity.y * Time.fixedDeltaTime);
        position.x = Mathf.Clamp(position.x, Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + MIN_X, Camera.main.ScreenToWorldPoint(new Vector2(MAX_X, 0)).x - MIN_X);
        position.y = Mathf.Clamp(position.y, Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + MIN_Y, Camera.main.ScreenToWorldPoint(new Vector2(0, MAX_Y)).y - MIN_Y);
        rb2D.MovePosition(position);
    }

}
