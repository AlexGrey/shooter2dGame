using UnityEngine;
using System.Collections;

public class SpawnMonster : MonoBehaviour {

	public GameObject monster;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", 1f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Spawn() {
		Instantiate (monster, transform.position, Quaternion.identity);
	}
}
