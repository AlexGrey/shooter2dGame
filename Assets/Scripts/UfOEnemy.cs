using UnityEngine;
using System.Collections;

public class UfOEnemy : MonoBehaviour {

	bool state = false;
	public int health = 4;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	/*void OnCollisionEnter2D(Collision2D collisionInfo) {
        if (collisionInfo.gameObject.tag.Equals("Player")){
            Invoke("HitPlayer", 0.5f);

        }
	}*/

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Player")){
			state = true;
			HitPlayer ();
			InvokeRepeating("HitPlayer", 2f, 1f);
            
            Debug.Log("touch player");
        }
    }

	public void DestroySelf() {
		Destroy (this.transform.parent.gameObject);
	}

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Player")){
			state = false;
        }
    }

    private void HitPlayer() {
		if (GameObject.Find ("CharacterRobotBoy").GetComponent<Character> ().health > 0 && state == true) {
			GameObject.Find ("CharacterRobotBoy").GetComponent<Character> ().health -= 1;
			GameObject.Find ("GUI").GetComponent<ManipulatorGUI> ().DeleteHeart ();
			Debug.Log ("current health: " + GameObject.Find ("CharacterRobotBoy").GetComponent<Character> ().health);

		} else if (state == false) {
			CancelInvoke("HitPlayer");
		}  
		if (GameObject.Find ("CharacterRobotBoy").GetComponent<Character> ().health == 0) {
			GameObject.Find ("GUI").GetComponent<ManipulatorGUI> ().ShowEndedGame ();
		}
    }


}
