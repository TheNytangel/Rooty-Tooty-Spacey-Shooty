using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleText : MonoBehaviour {
	// The seped the text will float at
	public float floatSpeed = 0.3F;
	// The "size" of the box the text will float within (by default, 50 units in each direction)
	public float border = 50.0F;
	// How close to the next border it should be to influence the direction (see GenerateRandomDirection())
	public float closenessToBorder = 5.0F;

	// Variables to store the borders based on the user-inputted border
	private float rightBorder, leftBorder, topBorder, bottomBorder;

	// Variable for random direction to go in
	private Vector3 randomDirection;
	// RectTransform variable to store the RectTransform component
	private RectTransform pos;

	// Use this for initialization
	void Start () {
		// Get the RectTransform component
		pos = GetComponent<RectTransform> ();

		// Set the borders for where the text should float
		rightBorder = pos.localPosition.x + border;
		leftBorder = pos.localPosition.x - border;
		topBorder = pos.localPosition.y + border;
		bottomBorder = pos.localPosition.y - border;

		// Generate a new random direction to float in
		GenerateRandomDirection ();
	}
	
	// Update is called once per frame
	void Update () {
		// Make the title text float around in a random direction
		pos.localPosition += randomDirection * floatSpeed;

		// Generate a new random direction based on what border it hit
		// Right border
		if (pos.localPosition.x >= rightBorder) {
			GenerateRandomDirection (true);
		}
		// Left border
		else if (pos.localPosition.x <= leftBorder) {
			GenerateRandomDirection (false, true);
		}
		// Top border
		else if (pos.localPosition.y >= topBorder) {
			GenerateRandomDirection (false, false, true);
		}
		// Bottom border
		else if (pos.localPosition.y <= bottomBorder) {
			GenerateRandomDirection (false, false, false, true);
		}
	}

	// Function to generate a new random direction
	void GenerateRandomDirection(bool hitRightWall = false, bool hitLeftWall = false, bool hitTopWall = false, bool hitBottomWall = false) {
		// Set default values for the new Vector3 (between -1 and 1 for x, -1 and 1 for y)
		float x1 = -1.0F, x2 = 1.0F, y1 = -1.0F, y2 = 1.0F;

		// If it hit the right wall, don't let the new random direction go to the right (make the range -1 to 0)
		if (hitRightWall) {
			x2 = 0.0F;

			// If it's also within range of the top border, make it go down as well (range -1 to 0 on the y coordinate)
			if (pos.localPosition.y >= topBorder - closenessToBorder) {
				y2 = 0.0F;
			// If it's also within range of the bottom border, make it go up as well (range 0 to 1 on the y coordinate)
			} else if (pos.localPosition.y <= bottomBorder + closenessToBorder) {
				y1 = 0.0F;
			}
		}
		// If it hit the left wall, don't let the new random direction go to the left (make the range 0 to 1)
		if (hitLeftWall) {
			x1 = 0.0F;

			// If it's also within range of the top border, make it go down as well (range -1 to 0 on the y coordinate)
			if (pos.localPosition.y >= topBorder - closenessToBorder) {
				y2 = 0.0F;
			// If it's also within range of the bottom border, make it go up as well (range 0 to 1 on the y coordinate)
			} else if (pos.localPosition.y <= bottomBorder + closenessToBorder) {
				y1 = 0.0F;
			}
		}
		// If it hit the top wall, don't let the new random direction go up (make the range -1 to 0)
		if (hitTopWall) {
			y2 = 0.0F;

			// If it's also within range of the right border, make it go left as well (range -1 to 0 on the x coordinate)
			if (pos.localPosition.x >= rightBorder - closenessToBorder) {
				x2 = 0.0F;
			// If it's also within range of the left border, make it go right as well (range 0 to 1 on the x coordinate)
			} else if (pos.localPosition.x <= leftBorder + closenessToBorder) {
				x1 = 0.0F;
			}
		}
		// If it hit the bottom wall, don't let the new random direction go down (make the range 0 to 1)
		if (hitBottomWall) {
			y1 = 0.0F;

			// If it's also within range of the right border, make it go left as well (range -1 to 0 on the x coordinate)
			if (pos.localPosition.x >= rightBorder - closenessToBorder) {
				x2 = 0.0F;
			// If it's also within range of the left border, make it go right as well (range 0 to 1 on the x coordinate)
			} else if (pos.localPosition.x <= leftBorder + closenessToBorder) {
				x1 = 0.0F;
			}
		}

		// Generate a new Vector3 with a random number in the ranges specified from above
		randomDirection = new Vector3 (Random.Range(x1, x2), Random.Range(y1, y2), 0);
		// Normalize the Vector3 so that it goes the same speed no matter what direction it is going (smaller numbers used to make it go slower)
		randomDirection.Normalize();
	}
}
