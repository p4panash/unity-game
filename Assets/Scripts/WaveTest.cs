using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTest : MonoBehaviour {

    public GameObject obj;

	void Start () {
        for (int i = 0; i < 6; i++) {
            Instantiate(obj);
        }		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
