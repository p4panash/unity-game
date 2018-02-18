using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

    public int playerScore;
    public GameObject playerScoreUI;

	// Use this for initialization
	void Start () {
        playerScore = 0;
        playerScoreUI.GetComponent<Text>().text = ("Score : " + playerScore);
    }

    public void AddScore(int value) {
        playerScore += value;
        playerScoreUI.GetComponent<Text>().text = ("Score : " + playerScore);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
