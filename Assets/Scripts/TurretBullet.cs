using UnityEngine;
using System.Collections;

public class TurretBullet : MonoBehaviour {
	GameObject player;
	Vector2 dir;
	public float speed = 10f;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("CharacterRobotBoy");
		transform.forward = player.transform.forward;
		Debug.Log (player.transform.forward);
		dir = (Vector2)(Vector3.Normalize( player.transform.position - this.transform.position));
	}

	void Update(){
		Destroy(this.gameObject, 1f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		/*transform.Translate(transform.forward * Time.deltaTime * 20f);
		Destroy(this.gameObject, 1f);*/
		this.gameObject.GetComponent<Rigidbody2D> ().velocity = dir * speed * Time.deltaTime;

	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Block") {
			Destroy (this.gameObject);
			if (coll.gameObject.tag == "Player") {
				GameObject.Find ("GUI").GetComponent<ManipulatorGUI> ().ShowEndedGame ();
			}
		}
	}

}
