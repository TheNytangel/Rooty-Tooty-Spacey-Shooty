using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    private Transform tf;
    public GameObject gm;

    public bool randomRotationSpeed = true;
    public bool randomMovementSpeed = true;

    public float rotationSpeed = 1.0F;
    public float movementSpeed = 0.01F;

	// Use this for initialization
	void Start () {
        tf = GetComponent<Transform>();

        if(randomRotationSpeed)
        {
            rotationSpeed = Random.Range(-1.0F, 1.0F);
        }

        if(randomMovementSpeed)
        {
            movementSpeed = Random.Range(0.003F, 0.007F);
        }
	}
	
	// Update is called once per frame
    void Update() {
        // Rotate the asteroid
        tf.Rotate(new Vector3(0, 0, rotationSpeed));

        if (GameObject.Find("ship") != null)
        {
            tf.transform.position = Vector3.MoveTowards(tf.position, new Vector3(0, 0, 0), movementSpeed);
        }
        else
        {
            // User is dead, so go away and reset
            this.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D bullet)
    {
        // Make sure it's a bullet and not the ship
        if(bullet.gameObject.name.StartsWith("Bullet"))
        {
            gm.GetComponent<Respawn>().spawner.OnAsteroidHit(this.gameObject);
            Destroy(bullet.gameObject);

            Statistics.shotAsteroids++;
        }
    }
}
