using UnityEngine;
using System.Collections;

public class MoveTrail : MonoBehaviour {

    public int moveSpeed = 230;

	// Use this for initialization
	void Start () {
        LineRenderer line = GetComponent<LineRenderer>();
        line.sortingLayerName = "Foreground";
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
        Destroy(this.gameObject, 0.01f);
	}

	void OnTriggerEnter2D(Collider2D collider2D) {
        Debug.Log(collider2D);
	}

}
