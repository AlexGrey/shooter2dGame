using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ManipulatorGUI : MonoBehaviour {
	public Image endedPanel;
    public List<GameObject> hearts;
	public Text countKilledMosters;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DeleteHeart() {
        if (hearts.Count > 0) {
            Destroy(hearts[hearts.Count - 1]);
            hearts.RemoveAt(hearts.Count - 1);
        }
    }

	public void ShowEndedGame() {
		endedPanel.gameObject.SetActive (true);
		countKilledMosters.text = GameObject.Find ("CharacterRobotBoy").GetComponent<Character> ().countOfKilledMosters.ToString();
		Time.timeScale = 0;
	}

	public void ReloadScene() {
		Time.timeScale = 1;
		SceneManager.LoadScene("Main");
	}
}
