using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour {

    public int enemySpeed = 1;
    public int moveX = -1;
    private float y;
    private float x;
    private float MIN_X;
    private float MIN_Y;
    private float MAX_X;
    private float MAX_Y;

    void Start () {
        float width = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float height = GetComponent<SpriteRenderer>().bounds.size.y / 2;
        MIN_Y = height;
        MIN_X = -width;
        MAX_X = Screen.width + width;
        MAX_Y = Screen.height - height;
        y = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + MIN_Y, Camera.main.ScreenToWorldPoint(new Vector2(0, MAX_Y)).y - MIN_Y);
        x = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(MAX_X, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(MAX_X + (MAX_X / 2), 0)).x);
        Debug.Log(x + "  " + width);
        gameObject.GetComponent<Rigidbody2D>().MovePosition(new Vector2(x, y));
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(gameObject.GetComponent<Rigidbody2D>().position);
        if (gameObject.GetComponent<Rigidbody2D>().position.x > new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(MIN_X, 0)).x, y).x) {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX, 0) * enemySpeed;
        }
        if (gameObject.GetComponent<Rigidbody2D>().position.x < new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(MIN_X, 0)).x, y).x - GetComponent<SpriteRenderer>().bounds.size.x * 2) {
            Destroy(gameObject);
        } 
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "enemy") {
            Debug.Log("regenerate");
            y = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + MIN_Y, Camera.main.ScreenToWorldPoint(new Vector2(0, MAX_Y)).y - MIN_Y);
            x = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(MAX_X, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(MAX_X * 2, 0)).x);
            StartCoroutine(wait());
            gameObject.GetComponent<Rigidbody2D>().MovePosition(new Vector2(x, y));
        }
    }

    IEnumerator wait() {
        yield return new WaitForSeconds(1f / (enemySpeed * 2));
    }
}
