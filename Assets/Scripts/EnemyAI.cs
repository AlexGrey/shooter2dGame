using UnityEngine;
using System.Collections;
using Pathfinding;

//[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class EnemyAI : MonoBehaviour {

	public Transform target;
	public float updateRate = 2f;
	public Path path;
	public float speed = 300f;
	public ForceMode2D fMode;

	[HideInInspector]
	public bool pathIsEnded = false;
	public float nextWaypointDistance = 3;

	private Seeker seeker;
	private int currentWaypoint = 0;

	// Use this for initialization
	void Start () {
		target = GameObject.Find ("CharacterRobotBoy").transform;
		seeker = GetComponent<Seeker> ();

		seeker.StartPath (transform.position, target.position, OnPathComplete);

		StartCoroutine (UpdatePath ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator UpdatePath() {
		if (target == null) {
			return false;
		}
		seeker.StartPath (transform.position, target.position, OnPathComplete);
		yield return new WaitForSeconds (1f / updateRate);
		StartCoroutine (UpdatePath ());
	}

	public void OnPathComplete(Path p) {
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}
	}

	void FixedUpdate() {
		if (target == null) {
			return;
		}
		if (path == null) {
			
			return;
		}
		if (currentWaypoint >= path.vectorPath.Count) {
			if (pathIsEnded) {
				return;
			}
			pathIsEnded = true;
			return;
		}
		pathIsEnded = false;

		transform.position = Vector3.MoveTowards (transform.position, path.vectorPath[currentWaypoint], speed * Time.deltaTime);
		float dist = Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]);
		if (dist < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}
}
