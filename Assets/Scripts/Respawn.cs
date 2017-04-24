using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
    public GameObject ship;
    public GameObject deadText;
    public GameObject gameOverText;
    public AsteroidSpawner spawner;

	private Quaternion startingRotation;

	// Use this for initialization
	void Start () {
		// Get the initial rotation
		startingRotation = ship.GetComponent<Transform>().rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if(!ship.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Space) && Statistics.lives > 0)
        {
            DoRespawn();
        }
	}

    void DoRespawn()
    {
		// Face the starting rotation
		ship.gameObject.transform.rotation = startingRotation;
		// Activate the ship
        ship.gameObject.SetActive(true);
		// Remove the "you died!" text
        deadText.gameObject.SetActive(false);

        // Start the asteroids spawning again
        spawner.StopSpawning(false);
    }
}
