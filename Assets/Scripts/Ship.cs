using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour {
    public float rotationSpeed;

    private Transform tf;
    public GameObject gm;
    private AsteroidSpawner spawner;

    public GameObject bulletPrefab;

    public GameObject[] hearts;

	// Use this for initialization
	void Start () {
        tf = GetComponent<Transform>();
        spawner = gm.GetComponent<AsteroidSpawner>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            tf.Rotate(Vector3.forward * rotationSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            tf.Rotate(Vector3.back * rotationSpeed);
        }
        
        // Shoot a bullet
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
        }
	}

    void OnCollisionEnter2D(Collision2D asteroid)
    {
        // Make sure it's an asteroid and not a bullet
        if (asteroid.gameObject.name.StartsWith("Asteroid"))
        {
            // Set the asteroid to false so it can be respawned
            spawner.OnAsteroidHit(asteroid.gameObject);

            // "Kill" the user
            Die();
        }
    }

    void Die()
    {
        // Decrement the number of lives
        Statistics.lives--;

        // Stop spawning asteroids
        spawner.StopSpawning(true);

        // Get rid of ship
        this.gameObject.SetActive(false);
        hearts[Statistics.lives].SetActive(false);

        // Show "you died" text
        if (Statistics.lives > 0)
        {
            gm.GetComponent<Respawn>().deadText.gameObject.SetActive(true);
        }
        else
        {
            gm.GetComponent<Respawn>().gameOverText.GetComponent<Text>().text = "GAME OVER!\n\nScore: " + Statistics.shotAsteroids + "\n\nClick to Continue";
            gm.GetComponent<Respawn>().gameOverText.gameObject.SetActive(true);
        }
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, tf.position, tf.rotation);
        bullet.gameObject.SetActive(true);
    }
}
