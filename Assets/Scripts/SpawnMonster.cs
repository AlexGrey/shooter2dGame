using UnityEngine;
using System.Collections;

public class SpawnMonster : MonoBehaviour {
	public float time;
	public GameObject monster;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", time, time);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Spawn() {
		Instantiate (monster, transform.position, Quaternion.identity);
	}
}
