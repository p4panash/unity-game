using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Movment : MonoBehaviour {

    public int enemySpeed = 1;
    public int moveX = -1;
    public float moveY = 0.5f;

    // x and y -> axis where the object will be generated
    private float y;
    private float x;

    //MIN_X, MIN_Y, MAX_X, MAX_Y -> the game area margins
    private float MIN_X;
    private float MIN_Y;
    private float MAX_X;
    private float MAX_Y;

    //the initial position of Y;
    private float INITIAL_Y;

    private bool UP;

    void Start() {
        float width = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float height = GetComponent<SpriteRenderer>().bounds.size.y / 2;
        MIN_Y = height;
        MIN_X = -width;
        MAX_X = Screen.width + width;
        MAX_Y = Screen.height - height;
        y = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + MIN_Y, Camera.main.ScreenToWorldPoint(new Vector2(0, MAX_Y)).y - MIN_Y);
        x = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(MAX_X, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(MAX_X + (MAX_X / 2), 0)).x);
        Debug.Log(x + "  " + height);
        gameObject.GetComponent<Rigidbody2D>().MovePosition(new Vector2(x, y));
        INITIAL_Y = y;
        UP = (Random.Range(0, 2) == 1);
    }

    // Update is called once per frame
    void Update () {
        if (gameObject.GetComponent<Rigidbody2D>().position.x > Camera.main.ScreenToWorldPoint(new Vector2(MAX_X, y)).x) {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX, 0) * enemySpeed;
        } else {
            if (gameObject.GetComponent<Rigidbody2D>().position.x > new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(MIN_X, 0)).x, y).x) {
                //Debug.Log("UP condition is " + (gameObject.GetComponent<Rigidbody2D>().position.y < (INITIAL_Y + GetComponent<SpriteRenderer>().bounds.size.y)));
                if (gameObject.GetComponent<Rigidbody2D>().position.y < (INITIAL_Y + GetComponent<SpriteRenderer>().bounds.size.y)
                        && gameObject.GetComponent<Rigidbody2D>().position.y + moveY <= (Camera.main.ScreenToWorldPoint(new Vector2(0, MAX_Y)).y) && UP) {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX, moveY) * enemySpeed;
                } else {
                    UP = false;
                    //Debug.Log("DOWN condition is " + (gameObject.GetComponent<Rigidbody2D>().position.y > (INITIAL_Y - GetComponent<SpriteRenderer>().bounds.size.y)));
                    if (gameObject.GetComponent<Rigidbody2D>().position.y > (INITIAL_Y - GetComponent<SpriteRenderer>().bounds.size.y)
                            && gameObject.GetComponent<Rigidbody2D>().position.y - moveY >= (Camera.main.ScreenToWorldPoint(new Vector2(0, MIN_Y)).y) && !UP) {
                        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX, -moveY) * enemySpeed;
                    } else {
                        UP = true;
                    }
                }
            }
        }
        if (gameObject.GetComponent<Rigidbody2D>().position.x < new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(MIN_X, 0)).x, y).x - GetComponent<SpriteRenderer>().bounds.size.x * 2) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (gameObject.GetComponent<Rigidbody2D>().position.x > Camera.main.ScreenToWorldPoint(new Vector2(MAX_X, y)).x && collision.gameObject.tag == "enemy") {
            Debug.Log("respawn");
            y = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + MIN_Y, Camera.main.ScreenToWorldPoint(new Vector2(0, MAX_Y)).y - MIN_Y);
            x = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(MAX_X, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(MAX_X + (MAX_X / 2), 0)).x);
            INITIAL_Y = y;
            StartCoroutine(wait());
            gameObject.GetComponent<Rigidbody2D>().MovePosition(new Vector2(x, y));
        } else {
            if (collision.gameObject.tag == "enemy")
                Debug.Log("Se ating");
                UP = !UP;
        }
    }

    IEnumerator wait() {
        yield return new WaitForSeconds(1f / (enemySpeed * 2));
    }
}
