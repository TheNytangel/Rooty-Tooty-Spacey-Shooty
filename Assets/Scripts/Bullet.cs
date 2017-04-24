using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 1.0F;

    private Transform tf;

    Vector3 bottomLeft;
    Vector3 topRight;

    // Use this for initialization
    void Start () {
        tf = GetComponent<Transform>();

        // Get Vector3 coordinates of the 4 corners of the screen
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)) - Vector3.one;
        topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)) + Vector3.one;
    }
	
	// Update is called once per frame
	void Update () {
        // Move in the direction it is facing
        tf.position += transform.right * speed;
        
        // If it's off the screen, destroy the bullet
        if(tf.position.x < bottomLeft.x || tf.position.x > topRight.x || tf.position.y < bottomLeft.y || tf.position.y > topRight.y)
        {
            Destroy(this.gameObject);
        }

        // If the player dies, also destroy
        if (GameObject.Find("ship") == null)
        {
            Destroy(this.gameObject);
        }
    }
}
