using UnityEngine;
using System.Collections;

public class FireControlSystem : MonoBehaviour {


	public IGunTurret [] turrents;

	public GameObject target;

	public float fieldOfViewRadius=30;
	public float fireRange;
	public float fireRate;
	public float turnSpeed=1;
	public float firePower;
	public GameObject[] projectileTypes;

	// Use this for initialization
	void Start () {

		this.setFieldOfViewRadius(fieldOfViewRadius);
		this.setFireRate(this.fireRate);
		this.setTurnSpeed(this.turnSpeed);
		this.setFireRange(this.fireRange);
		this.setFirePower(this.firePower);
		this.setProjectileTypes(projectileTypes);
		this.setUsedAmmunition(0);
	}

	void setUsedAmmunition(int newAmmunition) {
		foreach(IGunTurret turrent in turrents) {
			if(turrent.getProjectileTypesSize()>newAmmunition){
			   turrent.setUsedAmmunition(newAmmunition);
			}
		}
	}


	void setProjectileTypes(GameObject[] newProjectileTypes) {
		foreach(IGunTurret turrent in turrents) {
			turrent.setProjectileTypes(newProjectileTypes);
		}
	}

	void setFirePower(float firePower) {
		foreach(IGunTurret turrent in turrents) {
			turrent.setFirePower(firePower);
		}	
	}

	void setTarget(Vector3 newTarget) {
		foreach(IGunTurret turrent in turrents) {
			turrent.setTarget(newTarget);
		}	
	}
	
	
	void setFireRange(float newFireRange) {
		foreach(IGunTurret turrent in turrents) {
			turrent.setFireRange(newFireRange);
		}
	}
	
	void setFieldOfViewRadius(float newFieldOfViewRadius){
		foreach(IGunTurret turrent in turrents) {
			turrent.setFieldOfViewRadius(newFieldOfViewRadius);
		}
	}
	
	void setTurnSpeed(float newTurnSpeed){
		foreach(IGunTurret turrent in turrents) {
			turrent.setTurnSpeed(newTurnSpeed);
		}
	}
	
	void setFireRate(float newFireRate) {
		foreach(IGunTurret turrent in turrents) {
			turrent.setFireRate(newFireRate);
		}
	}

	// Update is called once per frame
	void Update () {
		this.setTarget(target.transform.position);
		//turrents[0].setTarget(target.transform.position);
		//turrents[1].setTarget(target.transform.position);
	     
	}
}
