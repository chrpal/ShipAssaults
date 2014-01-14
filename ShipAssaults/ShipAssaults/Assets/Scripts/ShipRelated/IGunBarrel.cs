using UnityEngine;
using System.Collections;

public class IGunBarrel : MonoBehaviour {

	//public GameObject barrel;
	public Transform projectileSpawnPoint;
	public float backwardScalingCoefficient = 0.01f;
	public float forwardScalingCoefficient = 0.01f;
	public float minimumScalingValue = 0.2f;
	public float maximumScalingValue = 1.0f;

	protected bool fireBurstAnimation = false;

	protected int scalingDirection = 0;
	protected byte animationStatus = 0;
	protected float currentScalingValue = 1.0f;

	// Use this for initialization
	void Start () {
	
	}

	protected virtual void Animate()
	{
		//Start animation
		if (this.animationStatus == 0)
		{
			//Scale the barrel down in y direction
			this.animationStatus = 1;
			this.scalingDirection = -1;
		}
		//Scale the barrel down in y direction
		else if (this.animationStatus == 1)
		{
			this.currentScalingValue += this.scalingDirection * this.backwardScalingCoefficient;
			if (this.currentScalingValue <= this.minimumScalingValue)
			{
				this.currentScalingValue = this.minimumScalingValue;
				this.animationStatus = 2;
				this.scalingDirection = 1;
			}
			transform.localScale = new Vector3(this.currentScalingValue, this.transform.localScale.y, transform.localScale.z);
		}
		//Scale the barrel up in y direction
		else if (this.animationStatus == 2)
		{
			this.currentScalingValue += this.scalingDirection * this.forwardScalingCoefficient;
			if (this.currentScalingValue >= this.maximumScalingValue)
			{
				this.currentScalingValue = this.maximumScalingValue;
				this.animationStatus = 3;
				this.scalingDirection = 0;
			}
			transform.localScale = new Vector3(this.currentScalingValue, transform.localScale.y, transform.localScale.z);
		}
		//Animation ready
		else if (this.animationStatus == 3)
		{
			this.animationStatus = 0;
			this.scalingDirection = 0;
			this.fireBurstAnimation = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (this.fireBurstAnimation == true)
		{
			this.Animate();
		}
	}

	public virtual void Fire()
	{
		this.fireBurstAnimation = true;
	}
}
