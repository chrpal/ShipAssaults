using UnityEngine;
using System.Collections;

public class Ammunition : MonoBehaviour {

	public float lifeTime = 0.0f;
	public float firePower = 50;
	public float standardLifeTimeDeviationRatio = 0.0f;

	public float targetDistance = 0.0f;
	public float standardTargetDistanceDeviation = 0.0f;

	public bool intelligentProjectile = false;

	public float standardAngleDeviationDeg = 0.0f;
	public Vector3 targetPosition;
	public Vector3 targetDirection;

	protected float elapsedTime = 0.0f;
	protected bool moving = false;

	public void Fire()
	{
		if (this.intelligentProjectile == false) 
		{
			float angle = Vector3.Angle(new Vector3(1.0f, 0.0f, 0.0f),this.targetDirection);
			angle += Random.value*2*this.standardAngleDeviationDeg - this.standardAngleDeviationDeg;
			angle *= Mathf.PI/180;

			this.targetDirection = new Vector3(Mathf.Cos(angle) * this.targetDirection.magnitude, 
			                                   Mathf.Sin(angle) * this.targetDirection.magnitude, 
			                                   0.0f);

			this.targetDistance += Random.value*2*this.standardTargetDistanceDeviation - this.standardTargetDistanceDeviation;
			this.targetPosition = new Vector3(Mathf.Cos (angle)*this.targetDistance,
			                                  Mathf.Sin (angle)*this.targetDistance,
			                                  this.targetDirection.z);

			if (this.lifeTime == 0.0f)
			{
				this.lifeTime = this.targetDistance/this.firePower;
			} 
			else
			{
				float lifeTimeDeviation = this.lifeTime * this.standardLifeTimeDeviationRatio;
				this.lifeTime += Random.value*2*lifeTimeDeviation-lifeTimeDeviation;
			}

			gameObject.rigidbody.AddForce (this.targetDirection * this.firePower, ForceMode.Impulse);
		} 
		else 
		{
			//ROCKET control!
		}
		this.moving = true;
	}

	// Use this for initialization
	void Start () {
	
	}

	protected void CheckAmmoParameters()
	{
		bool explode = false;
		if (this.lifeTime > 0.0f)
		{
			this.elapsedTime += Time.deltaTime;
			if (this.elapsedTime >= this.lifeTime)
			{
				this.moving = false;
				explode = true;
			}
		}
		float distance = Vector3.Distance (transform.position, this.targetPosition);
		if (distance <= 2.0f)
		{
			this.moving = false;
			explode = true;
		}

		if (explode == true)
		{
			//explode = true;
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if (this.moving == true)
		{
			if (this.intelligentProjectile == false)
			{
				this.CheckAmmoParameters();
			}
			else
			{
				IMovingAlgorithm algo = gameObject.GetComponent<IMovingAlgorithm>();
				algo.Update();
				//ROCKET CONTROL!!!
			}
		}
	}
}
