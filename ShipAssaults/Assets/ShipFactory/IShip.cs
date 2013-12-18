using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class IShip : MonoBehaviour {

	protected List<IGunTurret> guns;

	protected float maxTranslation;
	protected float maxRotation;
	protected float magnitude;
	public Vector3 targetLocation;
	protected bool moveCommand = false;


	protected abstract void InitializeShip ();

	// Use this for initialization
	void Start () {
		this.magnitude = 0.0f;
		this.InitializeShip ();
	}


	protected virtual void PerformMovement()
	{
		Vector3 currentPos = renderer.transform.position;

		//Target location in der Basis vom Schiff
		Vector3 deltaVect = this.targetLocation - currentPos;


		float tmpDeltaMagnitude = deltaVect.magnitude;
		while (tmpDeltaMagnitude < 0) 
		{
			tmpDeltaMagnitude += 360;
		}

		float tmpOwnMagnitude = this.magnitude;
		while (tmpOwnMagnitude<0) 
		{
			tmpOwnMagnitude += 360;
		}
		tmpOwnMagnitude %= 360;

		float deltaAngle = tmpOwnMagnitude - tmpDeltaMagnitude;
		while (deltaAngle < 0) 
		{
			deltaAngle += 360;
		}
		deltaAngle %= 360;

		if (deltaAngle >= 0.01f) 
		{
			float rotation = this.maxRotation;

			int direction = 1;

			if (Mathf.Abs (deltaAngle) < this.maxRotation)
			{
				rotation = Mathf.Abs(deltaAngle);
				direction = -1;
			}


			/*if (360-deltaAngle<deltaAngle)
			{
				direction = -1;
			}*/

			renderer.transform.RotateAround (currentPos, new Vector3 (0, 0, 1), direction * rotation);
			this.magnitude += direction * rotation;

		} 
		else 
		{
			float distance = Mathf.Sqrt(deltaVect.x*deltaVect.x + deltaVect.y*deltaVect.y);
			if (distance > 1) 
			{
				currentPos.x += this.maxTranslation*Mathf.Cos(magnitude);
				currentPos.y += this.maxTranslation*Mathf.Sin(magnitude);
				renderer.transform.position = currentPos;
			} 
			else 
			{
				this.moveCommand = false;
			}
		}
	}

	protected abstract void AnimateShipMotor();

	// Update is called once per frame
	protected virtual void Update () {
		if (moveCommand == true) 
		{
			this.PerformMovement ();
			this.AnimateShipMotor();
		}
	}
}
