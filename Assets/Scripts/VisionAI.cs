using UnityEngine;
using System.Collections;

public class VisionAI : MonoBehaviour {
	public LayerMask whatToHit;

	[HideInInspector]
	public bool playerIsVisible = false;
	private float angel = 0;
	RaycastHit2D hit;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Player") && !playerIsVisible){
			InvokeRepeating ("checkVision", 0.2f, 0.2f);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Player")){
			CancelInvoke ("checkVision");
			playerIsVisible = false;
		}
	}

	void checkVision () {
		for (int i = 0; i < 360; i+= 10) {
			float x1 = this.transform.position.x + (Mathf.Cos (i) * this.GetComponent<CircleCollider2D>().radius);
			float y1 = this.transform.position.y + (Mathf.Sin (i) * this.GetComponent<CircleCollider2D>().radius);

			Vector2 v1 = new Vector2 (this.transform.position.x, this.transform.position.y);
			Vector2 v2 = new Vector2 (x1, y1);
			hit = Physics2D.Raycast (v1, v2 - v1, this.GetComponent<CircleCollider2D>().radius, whatToHit);
			if (hit.collider != null && hit.collider.transform.tag.Equals("Player")) {
				Debug.Log (hit.collider);
				Debug.DrawRay (v1, v2 - v1, Color.red);
				playerIsVisible = true;
				CancelInvoke ("checkVision");
			} else {
				Debug.DrawRay (v1, v2 - v1, Color.green);
			}
		}

	}
}
