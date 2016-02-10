using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour {
	public InputField consoleField;
    private bool isActive = false;

	// Use this for initialization
	void Start () {
        consoleField.onEndEdit.AddListener(val =>
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            if (consoleField.text.Equals("reload")){
                //Application.LoadLevel("Main");
					SceneManager.LoadScene("Main");
            }
        });
        consoleField.gameObject.SetActive(isActive);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.BackQuote)){
            isActive = !isActive;
            consoleField.gameObject.SetActive(isActive);
        }
	}
}
