using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ScoreKeeper : MonoBehaviour {

    //the score.
    public float[] score;
    //the game's UI element 
    public Text text; 

	// Use this for initialization
	void Start () {
        score = new float[2];
        //display the score to the screen
        UpdateScoreText();

	}

    void UpdateScoreText()
    {
        string toWrite = "Blue: " + (int)(score[0]) + " \t" + "Red: " + (int)(score[1]);
        text.text = toWrite;
    }

    //public method to be called from the Goal script
    public void ScoreGoal(int whichgoal)
    {
        //add half the score (two colliders)
        score[whichgoal]+=0.5f;
        //display the updated score
        UpdateScoreText();
    }

}
