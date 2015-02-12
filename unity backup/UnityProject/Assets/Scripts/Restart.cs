using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {
	void Start() {

	}

	public void RestartGame () {
		Application.LoadLevel(Application.loadedLevel);
	}
}
