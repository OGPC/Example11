using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public GameObject menuRoot;
	public bool paused = false;
	public bool toggleMouse = true;
	private bool lastPaused = false;

	void Start () {
		if (menuRoot == null)
			Debug.LogWarning("PauseMenu on " + gameObject.name + " has no menu assigned.");
		SetPause(paused);
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape))
			paused = !paused;

		if (paused != lastPaused) {
			lastPaused = paused;
			SetPause(paused);
		}
	}

	public void SetPause(bool isPaused) {
		paused = isPaused;
		lastPaused = isPaused;
		Time.timeScale = (isPaused?0f:1f);
		if (toggleMouse)
			Cursor.visible = isPaused;
		if (menuRoot != null)
			menuRoot.SetActive(isPaused);
	}

	public void ButtonResume() {
		SetPause(false);
	}

	public void ButtonRestart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void ButtonMenu() {
		Debug.Log("Pressed menu");
	}

	public void ButtonExit() {
		Debug.Log("Pressed exit");
		Application.Quit();
	}
}
