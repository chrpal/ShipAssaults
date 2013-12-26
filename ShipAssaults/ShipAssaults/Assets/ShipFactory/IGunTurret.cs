using UnityEngine;
using System.Collections;

public class IGunTurret : MonoBehaviour {


	public float angle;
	public Vector3 target;
	public GameObject [] usedProjectileTypes;
	public Transform [] projectileSpawnPoints;
	//public float ammo;
	public float fieldOfViewRadius=30;
	public float fireRange;
	public float fireRate;
	public Vector3 cannonStartAxis= new Vector3(1,0,0);
	public float turnSpeed=1;

	private Vector3 fireDirection;
	private Quaternion lookRotation;
	private Quaternion startRotaton;
	private bool test=false;
	public float firePower=50;
	private float currentTime;

	// Use this for initialization
	void Start () {
		//this.startRotaton=transform.rotation;
	}



	public void setTarget(Vector3 newTarget) {
		target=newTarget;
	}

	 
	void fire() {
		//Debug.Log("Hallo");
		Debug.Log(projectileSpawnPoints.Length+" nuzzels");

		currentTime+=Time.deltaTime;

		if(currentTime>this.fireRate){

		foreach(Transform pos in projectileSpawnPoints) {
			Debug.Log("fire");
			GameObject projectile= (GameObject) Object.Instantiate(usedProjectileTypes[0],pos.position,Quaternion.identity);
			projectile.rigidbody.AddForce(-transform.right*firePower,ForceMode.Impulse);
				}

			currentTime=0;

		}



	}



	// Update is called once per frame
	void Update () {



		if(this.target!=null) {



			fireDirection = (target - transform.position);
			fireDirection.z = 0.0f;
			//Debug.DrawRay(transform.position,fireDirection,Color.red);
			//Debug.DrawRay(transform.position,-transform.right,Color.red);


			if (fireDirection.magnitude>fieldOfViewRadius) {

				lookRotation = Quaternion.AngleAxis(0,new Vector3(0,0,1)); // Quaternion.FromToRotation (transform.right,cannonStartAxis);
				transform.localRotation = Quaternion.Lerp(transform.localRotation, lookRotation, turnSpeed*Time.deltaTime);
			
				Debug.DrawRay(transform.position,transform.right,Color.red);

				Debug.DrawRay(transform.position,cannonStartAxis,Color.green);


			}
			else {
				lookRotation = Quaternion.FromToRotation (cannonStartAxis,-fireDirection);
				transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, turnSpeed*Time.deltaTime);
				Debug.DrawRay(transform.position,cannonStartAxis,Color.red);
				Debug.DrawRay(transform.position,-fireDirection,Color.green);

				if(fireDirection.magnitude<fireRange) {
					this.fire();
				}

			}


			//transform.localRotation = Quaternion.Slerp(transform.localRotation, lookRotation, Time.deltaTime*turnSpeed );
		}
	
	}
}
