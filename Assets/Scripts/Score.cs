using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    [SerializeField] Text scoreText;
    private float points;

    // Use this for initialization
    void Start () {
        scoreText.text = "Score: " + points.ToString();	
	}
	
    public void AddScore(float value)
    {
        points += value;
        scoreText.text = "Score: " + points.ToString();
    }
}
