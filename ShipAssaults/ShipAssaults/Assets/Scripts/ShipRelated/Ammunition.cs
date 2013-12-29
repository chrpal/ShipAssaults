using UnityEngine;
using System.Collections;

public class Ammunition : MonoBehaviour {

	public float lifeTime = 0.0f;
	public float firePower = 50;
	public float standardLifeTimeDeviation = 0.0f;

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
			float angle = Vector3.Angle(Vector3.zero,this.targetDirection);
			angle += Random.value*2*this.standardAngleDeviationDeg - this.standardAngleDeviationDeg;
			angle *= Mathf.PI/180;
			Vector3 randomDirection = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f);

			this.targetDistance += Random.value*2*this.standardTargetDistanceDeviation - this.standardTargetDistanceDeviation;
			this.targetPosition = new Vector3(this.targetDirection.x*this.targetDistance,
			                                  this.targetDirection.y*this.targetDistance,
			                                  this.targetDirection.z);

			this.lifeTime += Random.value*2*this.standardLifeTimeDeviation-this.standardLifeTimeDeviation;

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
		if (this.lifeTime > 0.0f)
		{
			this.elapsedTime += Time.deltaTime;
			if (this.elapsedTime >= this.lifeTime)
			{
				this.moving = false;
				//EXPLODE!!!!!
			}
		}
		float distance = Vector3.Distance (transform.position, this.targetPosition);
		if (distance < 1.0f)
		{
			this.moving = false;
			//EXPLODE
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
