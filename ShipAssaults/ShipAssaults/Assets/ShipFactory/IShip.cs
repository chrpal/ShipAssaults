using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class IShip : MonoBehaviour {
	
	protected List<IGunTurret> guns;
	protected string identifier = "";
	public float translationSpeed = 1;
	public float rotationSpeed = 1;
	public Vector3 targetLocation;
	public Texture2D shipTexture;
	protected bool moveCommand = false;
	
	protected int rotationDirection = 0;
	
	protected abstract void InitializeShip ();

	protected virtual void generateID()
	{
		Random.seed = Mathf.FloorToInt (Time.realtimeSinceStartup);
		for (int i=0; i < 12; i++) 
		{
			int number = Random.Range(0, 9);
			this.identifier += number.ToString();
		}
	}

	// Use this for initialization
	protected virtual void Start () {
		//Load texture
		//this.texture = (Texture2D)Resources.LoadAssetAtPath ("/Assets/Resources/Images/patrolboat", typeof(Texture2D));
		renderer.material.mainTexture = this.shipTexture;
		
		//Set shader
		Shader shader = Shader.Find ("Unlit/Transparent");
		renderer.material.shader = shader;

		this.generateID ();

		this.InitializeShip ();
	}
	
	
	protected virtual void PerformMovement()
	{
		Vector3 currentPos = renderer.transform.position;
		
		Vector3 deltaVect = this.targetLocation - currentPos;
		deltaVect = new Vector3 (deltaVect.x, deltaVect.y, 0);
		
		float distance = deltaVect.magnitude;
		
		if (this.rotationDirection == 0) 
		{
			if (deltaVect.y >= 0)
			{
				this.rotationDirection = 1;
			}
			else
			{
				this.rotationDirection = -1;
			}
		}
		
		float angleDeviation = Vector3.Angle (transform.right, deltaVect);
		
		if (Mathf.Abs (angleDeviation) > 1.0f)
		{
			Vector3 newOrientation = new Vector3(0,
			                                     0,
			                                     this.rotationDirection * this.rotationSpeed + transform.eulerAngles.z);
			transform.eulerAngles = newOrientation;
		}
		
		float angleDeviationRatio = angleDeviation / 180;
		float angleDistanceSquaredRatio = (angleDeviationRatio*angleDeviationRatio)/(distance*distance);

		float speedCoefficient = (1 - angleDeviationRatio) * (1-angleDistanceSquaredRatio);

		if (speedCoefficient > 1) 
		{
			speedCoefficient = 1.0f;
		} else if (speedCoefficient < 0) 
		{
			speedCoefficient = 0.0f;
		}
		
		float speed = this.translationSpeed;
		speed = speedCoefficient * speed;

		float speedRatio = speed / this.translationSpeed;

		if (distance < speed)
		{
			speed = distance;
		}
		
		if (distance >= 0.05f) 
		{
			float ownOrientationRad = transform.eulerAngles.z*Mathf.PI/180;
			currentPos.x += speed*Mathf.Cos(ownOrientationRad);
			currentPos.y += speed*Mathf.Sin(ownOrientationRad);
			renderer.transform.position = currentPos;
		} 
		else 
		{
			this.moveCommand = false;
			this.rotationDirection = 0;
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
