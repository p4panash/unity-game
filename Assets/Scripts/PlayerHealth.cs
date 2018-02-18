using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public int HP;
    public bool alive;

	// Use this for initialization
	void Start () {
        alive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!alive) {
            EditorSceneManager.LoadScene("Prototip");
        }
	}

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "enemy") {
            StartCoroutine(Wait());
            alive = false;
            Destroy(collision.gameObject);
        }
    }

    IEnumerator Wait() {
        yield return new WaitForSecondsRealtime(2);
    }
}
