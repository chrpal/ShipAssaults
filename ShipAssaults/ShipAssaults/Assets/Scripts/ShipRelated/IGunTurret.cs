using UnityEngine;
using System.Collections;

public class IGunTurret : MonoBehaviour {

	[System.Serializable]
	public class gunBarrel {
		public GameObject barrel;
		public Transform projectileSpawnPoint;

	}


	public float fireAngle;

	public GameObject [] ammunitionTypes;
	public GameObject[] projectileTypes;
	public gunBarrel []  gunBarrels;

	
	public Transform [] projectileSpawnPoints;
	public int usedAmmunition=0;


	public float fieldOfViewRadius=30;
	public float fireRange;
	public float fireRate;
	public float turnSpeed=1;


	private Vector3 cannonStartAxis= new Vector3(1,0,0);
	private Vector3 target;
	private Vector3 fireDirection;
	private Quaternion lookRotation;
	private Quaternion startRotaton;
	public float firePower=50;

	public bool validTarget = false;
    
	private  float[] currentTimes;

	// Use this for initialization
	void Start () {
		//this.startRotaton=transform.rotation;
		currentTimes=new float[projectileSpawnPoints.Length];
		this.startRotaton=this.transform.rotation;

	}

	

	public int getProjectileTypesSize() {
		return this.projectileTypes.Length;
	}
	
	public void setFireAngle(float newFireAngle) {
		this.fireAngle=newFireAngle;
	}

	public void setUsedAmmunition(int newUsedAmmiunition) {
		usedAmmunition=newUsedAmmiunition;
	}

	public void setProjectileTypes(GameObject[] newProjectileTypes) {
		this.projectileTypes=newProjectileTypes;
	}

	public void setAmmunitionTypes(GameObject[] newAmmunitionTypes) {
		this.ammunitionTypes = newAmmunitionTypes;
	}

	public void setFirePower(float newFirePower) {
		this.firePower=newFirePower;
	}

	public void setTarget(Vector3 newTarget) {
		target=newTarget;
		this.validTarget = true;
	}

	public void unsetTarget() {
		this.validTarget = false;
	}


	public void setFireRange(float newFireRange) {
		this.fireRange=newFireRange;
	}

	public void setFieldOfViewRadius(float newFieldOfViewRadius){
		this.fieldOfViewRadius=newFieldOfViewRadius;
	}

	public void setTurnSpeed(float newTurnSpeed){
		this.turnSpeed=newTurnSpeed;
	}

	public void setFireRate(float newFireRate) {
		this.fireRate=newFireRate;
	}

	 
	  void Fire() {
			for(int i=0;i<projectileSpawnPoints.Length;i++){
		
				currentTimes[i]+=Time.deltaTime;
			    if(currentTimes[i]>this.fireRate+Random.value){

				GameObject ammoTemplate = this.ammunitionTypes[this.usedAmmunition];
				GameObject ammoObj = PrefabManager.get_instance().InstantiatePrefab(ammoTemplate,projectileSpawnPoints[i].position);
				Ammunition ammo = (Ammunition)ammoObj.GetComponent<Ammunition>();
				ammo.targetPosition = this.target;
				ammo.targetDirection = -transform.right;
				ammo.targetDistance = Vector3.Distance(transform.position,this.target);

				ammo.Fire();

      			  currentTimes[i]=0;

			    }

		}

	}



	// Update is called once per frame
	void Update () {

			fireDirection = (target - transform.position);
			fireDirection.z = 0.0f;
			//Debug.DrawRay(transform.position,fireDirection,Color.red);
			//Debug.DrawRay(transform.position,-transform.right,Color.red);


			if (fireDirection.magnitude>fieldOfViewRadius || this.validTarget == false) {

				//lookRotation = Quaternion.AngleAxis(0,new Vector3(0,0,1)); // Quaternion.FromToRotation (transform.right,cannonStartAxis);
				transform.localRotation = Quaternion.Lerp(transform.localRotation, this.startRotaton, turnSpeed*Time.deltaTime);
			    
				Debug.DrawRay(transform.position,transform.right,Color.red);

				Debug.DrawRay(transform.position,cannonStartAxis,Color.green);


			}
			else {
				lookRotation = Quaternion.FromToRotation (cannonStartAxis,-fireDirection);
				transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, turnSpeed*Time.deltaTime);
				Debug.DrawRay(transform.position,cannonStartAxis,Color.red);
				Debug.DrawRay(transform.position,-fireDirection,Color.green);

		
			Debug.Log(transform.rotation.eulerAngles.z+" currentAngle");
			Debug.Log(lookRotation.eulerAngles.z+this.fireAngle+" inRangeAngle");
			Debug.Log(lookRotation.eulerAngles.z+this.fireAngle);
			float inRangeAngle=lookRotation.eulerAngles.z+this.fireAngle;
			float currentAngle=transform.rotation.eulerAngles.z;

			if(fireDirection.magnitude<fireRange && (inRangeAngle>currentAngle && currentAngle>inRangeAngle-2*fireAngle)) {
					this.Fire();
				}

			}
	}
}
