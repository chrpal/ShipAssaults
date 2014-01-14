using UnityEngine;
using System.Collections;

public class FireControlSystem : MonoBehaviour {


	public IGunTurret [] turrets;

	public GameObject target = null;

	public float fireAngle;
	public float fieldOfViewRadius=30;
	public float fireRange;
	public float fireRate;
	public float turnSpeed=1;
	//public float firePower;
	//public GameObject[] projectileTypes;
	public GameObject [] ammunitionTypes;

	// Use this for initialization
	void Start () {

		this.setFieldOfViewRadius(fieldOfViewRadius);
		this.setFireRate(this.fireRate);
		this.setTurnSpeed(this.turnSpeed);
		this.setFireRange(this.fireRange);
		//this.setFirePower(this.firePower);
		//this.setProjectileTypes(projectileTypes);
		this.setAmmunitionTypes (ammunitionTypes);
		this.setUsedAmmunition(0);
		this.setFireAngle(this.fireAngle);
	}


	void setFireAngle(float newAngle) {
		foreach(IGunTurret turret in turrets) {

			turret.setFireAngle(newAngle);

		}
	}

	void setUsedAmmunition(int newAmmunition) {
		foreach(IGunTurret turret in turrets) {
			if(turret.getAmmunitionTypesSize()>newAmmunition){
				turret.setUsedAmmunition(newAmmunition);
			}
		}
	}


	/*void setProjectileTypes(GameObject[] newProjectileTypes) {
		foreach(IGunTurret turret in turrets) {
			turret.setProjectileTypes(newProjectileTypes);
		}
	}*/

	void setAmmunitionTypes(GameObject[] newAmmunitionTypes) {
		foreach (IGunTurret turret in turrets) {
			turret.setAmmunitionTypes(newAmmunitionTypes);
		}
	}

	/*void setFirePower(float firePower) {
		foreach(IGunTurret turret in turrets) {
			turret.setFirePower(firePower);
		}	
	}*/

	void setTarget(Vector3 newTarget) {
		foreach(IGunTurret turret in turrets) {
			turret.setTarget(newTarget);
		}	
	}

	void unsetTarget() {
		foreach(IGunTurret turret in turrets) {
			turret.unsetTarget();
		}	
	}
	
	
	void setFireRange(float newFireRange) {
		foreach(IGunTurret turret in turrets) {
			turret.setFireRange(newFireRange);
		}
	}
	
	void setFieldOfViewRadius(float newFieldOfViewRadius){
		foreach(IGunTurret turret in turrets) {
			turret.setFieldOfViewRadius(newFieldOfViewRadius);
		}
	}
	
	void setTurnSpeed(float newTurnSpeed){
		foreach(IGunTurret turret in turrets) {
			turret.setTurnSpeed(newTurnSpeed);
		}
	}
	
	void setFireRate(float newFireRate) {
		foreach(IGunTurret turret in turrets) {
			turret.setFireRate(newFireRate);
		}
	}

	// Update is called once per frame
	void Update () {

		if (target != null) 
		{
			this.setTarget (target.transform.position);
		} else 
		{
			this.unsetTarget();
		}
	}
}
