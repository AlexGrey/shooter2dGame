using UnityEngine;
using System.Collections;

public class TurretGun : MonoBehaviour {

	public Transform turret;
	public Transform target;
	public Transform startPoint;
	public int rotationOffset = 90;
	public GameObject bullet;
	public Transform vision;

	private bool canShoot = true;

	[HideInInspector]
	public int health = 3;

	// Use this for initialization
	void Start () {
		//InvokeRepeating("Shoot", 2, 1F);
	}
	
	// Update is called once per frame
	void Update () {
		

		if (vision.GetComponent<VisionAI> ().playerIsVisible && canShoot) {

			Vector3 difference = transform.position -  target.transform.position;
			difference.Normalize();

			float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);	
			//Shoot ();
			StartCoroutine(Shoot());
			canShoot = false;

		}

	}

	public void DestroySelf(){
		Destroy (turret.gameObject);
	}

	IEnumerator Shoot() {
		yield return new WaitForSeconds(2f);
		GameObject clone = (GameObject) Instantiate(bullet, startPoint.transform.position, startPoint.transform.rotation);
		canShoot = true;
		//Vector2 dir = (Vector2)(Vector3.Normalize(target.transform.position - startPoint.transform.position));
		//clone.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 200f, ForceMode2D.Impulse);
	}
}
