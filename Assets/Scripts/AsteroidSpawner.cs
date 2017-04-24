using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

    public GameObject[] asteroids;

    private int currentActiveAsteroids;
    private bool stopSpawning = false;

	// Use this for initialization
	void Start () {
        // Make sure no asteroids are active
		for (int i = 0; i < asteroids.Length; i++)
        {
            asteroids[i].gameObject.SetActive(false);
        }
        // Set the active count to zero
        currentActiveAsteroids = 0;
	}
	
	// Update is called once per frame
	void Update () {
		while (currentActiveAsteroids < 3 && !stopSpawning)
        {
            // Generate a random number to select a random asteroid to spawn
            int nextAsteroid = Random.Range(0, asteroids.Length);

            if(asteroids[nextAsteroid].gameObject.activeSelf)
            {
                // This asteroid is already spawned so skip it and spawn a different one
                continue;
            }

            // Move the astroid off the screen
            asteroids[nextAsteroid].gameObject.transform.position = PointSomewhereOffScreen();
            // Activate the asteroid
            asteroids[nextAsteroid].gameObject.SetActive(true);

            // Increment the asteroid counter
            currentActiveAsteroids++;
        }
	}

    Vector3 PointSomewhereOffScreen()
    {
        Vector3 point = new Vector3();

        // Get Vector3 coordinates of the 4 corners of the screen
        Vector3 bottomLeft  = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        Vector3 topLeft     = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        Vector3 topRight    = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // Random side of the screen the point will be on
        int side = Random.Range(0, 4);

        // Generate random point based on the side
        switch (side)
        {
            // Left side
            case 0:
                point = new Vector3(bottomLeft.x - 0.5F, Random.Range(bottomLeft.y - 1, topLeft.y + 1), 0);
                break;
            // Right side
            case 1:
                point = new Vector3(bottomRight.x + 0.5F, Random.Range(bottomRight.y - 1, topRight.y + 1), 0);
                break;
            // Top side
            case 2:
                point = new Vector3(Random.Range(topLeft.x - 1, topRight.x + 1), topLeft.y + 0.5F, 0);
                break;
            // Down side
            case 3:
                point = new Vector3(Random.Range(bottomLeft.x - 1, bottomRight.x + 1), bottomLeft.y - 0.5F, 0);
                break;
        }

        return point;
    }

    public void OnAsteroidHit(GameObject asteroid) {
        // Deactivate the asteroid
        asteroid.gameObject.SetActive(false);
        // Decrement the current count of asteroids
        currentActiveAsteroids--;
    }

    public void StopSpawning(bool stop)
    {
        stopSpawning = stop;
        currentActiveAsteroids = 0;
    }
}
