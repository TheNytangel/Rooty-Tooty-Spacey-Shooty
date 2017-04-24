using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour {
    public static int lives = 3;
    public static int shotAsteroids = 0;

	// Use this for initialization
	void Start () {
        lives = 3;
        shotAsteroids = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
