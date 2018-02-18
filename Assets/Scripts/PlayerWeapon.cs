using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    public float fireRate = 0;
    public float damage = 1;

    public Transform BulletTrailPrefab;
    Transform firePoint;

    float timeToFire = 0;
    float width;

	// Use this for initialization
	void Start () {
        firePoint = transform.Find("tip");
        if (firePoint == null) {
            Debug.Log("Nu exista firePoint");
        }
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        firePoint.position += new Vector3(BulletTrailPrefab.GetComponent<SpriteRenderer>().size.x * 3, 
            BulletTrailPrefab.GetComponent<SpriteRenderer>().size.y / 4, 0);
    }
	
	// Update is called once per frame
	void Update () {
        //Shoot();
		if (fireRate == 0) {
            if (Input.GetButtonDown("Jump")) {
                Shoot();
            } else if (Input.GetButtonDown("Jump") && Time.time > timeToFire) {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
	}

    void Shoot() {
        Vector2 firePointPosition = new Vector2(gameObject.GetComponent<Rigidbody2D>().position.x + width / 3, gameObject.GetComponent<Rigidbody2D>().position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, 
            new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x + 2, firePointPosition.y) - firePointPosition);
        Effect();
        //Debug.Log(gameObject.GetComponent<Rigidbody2D>().position + "  " + firePointPosition + "  hit  " + hit.point + "  care ii cam asa " + (new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x + 2, firePointPosition.y)));
        Debug.DrawLine(firePointPosition, new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x + 2, firePointPosition.y), Color.white);
    }

    void Effect() {
        //Physics2D.IgnoreCollision(BulletTrailPrefab.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>());
        Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
    }
}
