using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class IShip : MonoBehaviour {

	protected List<IGunTurret> guns;

	public float maxTranslation;
	public float maxRotation;
	public Vector3 targetLocation;
	public Texture2D shipTexture;
	protected bool moveCommand = false;

	protected int rotationDirection = 0;


	protected abstract void InitializeShip ();

	// Use this for initialization
	protected virtual void Start () {
		//Load texture
		//this.texture = (Texture2D)Resources.LoadAssetAtPath ("/Assets/Resources/Images/patrolboat", typeof(Texture2D));
		renderer.material.mainTexture = this.shipTexture;

		//Set shader
		Shader shader = Shader.Find ("Unlit/Transparent");
		renderer.material.shader = shader;

		this.InitializeShip ();
	}


	protected virtual void PerformMovement()
	{
		Vector3 currentPos = renderer.transform.position;
		Vector3 localCurrentPos = renderer.transform.localPosition;
	
		Vector3 deltaVect = this.targetLocation - currentPos;   

		float distance = deltaVect.magnitude;

		Quaternion ownLookQuaternion = renderer.transform.rotation;

		Quaternion targetLookQuaternion = Quaternion.LookRotation (targetLocation, new Vector3(1f,0,0));

		//Quaternion test = Quaternion.Euler (new Vector3 (0, 0, renderer.transform.eulerAngles.z));

		transform.rotation = Quaternion.Slerp (ownLookQuaternion, targetLookQuaternion, Time.time * 0.01f);

		return;

		float r = deltaVect.magnitude;
		float x = deltaVect.x;
		float y = deltaVect.y;
		float controlOrientation = 0;

		if (y >= 0) 
		{
			controlOrientation = Mathf.Acos(x/r) * 180/Mathf.PI;
		} 
		else {
			controlOrientation = 360 - Mathf.Acos (x/r)  * 180/Mathf.PI;
		}

		float ownOrientation = renderer.transform.eulerAngles.z;
		while (ownOrientation<0) 
		{
			ownOrientation += 360;
		}
		ownOrientation %= 360;

		float deviationOrientation = controlOrientation - ownOrientation;
		while (deviationOrientation < 0) 
		{
			deviationOrientation += 360;
		}
		deviationOrientation %= 360;

		if (Mathf.Abs(deviationOrientation) >= 0.5f) 
		{
			float rotation = this.maxRotation;

			if (Mathf.Abs (deviationOrientation) < this.maxRotation)
			{
				renderer.transform.eulerAngles = new Vector3(renderer.transform.eulerAngles.x,
				                                             renderer.transform.eulerAngles.y,
				                                             controlOrientation);
				/*rotation = Mathf.Abs(deviationOrientation);
				if (deviationOrientation <= 180)
				{
					this.rotationDirection = 1;
				}
				else
				{
					this.rotationDirection = -1;
				}*/
			} 
			else 
			{
				if (this.rotationDirection == 0)
				{
					if (deviationOrientation <= 180)
					{
						this.rotationDirection = 1;
					}
					else
					{
						this.rotationDirection = -1;
					}
				}

				renderer.transform.RotateAround (currentPos, new Vector3 (0, 0, 1), this.rotationDirection * rotation);
			}

		} 
		else
		{
			this.rotationDirection = 0;
			if (distance > 0.2f) 
			{
				float ownOrientationRad = ownOrientation*Mathf.PI/180;
				currentPos.x += this.maxTranslation*Mathf.Cos(ownOrientationRad);
				currentPos.y += this.maxTranslation*Mathf.Sin(ownOrientationRad);
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
