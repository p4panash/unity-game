using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour {

    public GameObject obj;
    public PlayerScore score;
    public float moveSpeed = 1;

    private void Start() {
        obj = GameObject.Find("Player");
        score = obj.GetComponent<PlayerScore>();
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        if (transform.position.x > 
            new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x + gameObject.GetComponent<SpriteRenderer>().size.x * 2, 0).x) {
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "enemy") {
            score.AddScore(10);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player") {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
    }
}
