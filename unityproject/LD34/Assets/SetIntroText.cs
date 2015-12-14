using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SetIntroText : MonoBehaviour {

	public Text introText;

	private bool loaded = false;
	public float wait = 6.0f;

	private string text = "SPEED TAG"+System.Environment.NewLine+
		"<size=10>Use A to start selecting paint, and B to jump."+System.Environment.NewLine+
		"When selecting paint, use A and B to choose which color."+System.Environment.NewLine+
		"Match the graffiti color to your paint color to score points!" +
		"</size>";

	// Use this for initialization
	void Start () {
		if (introText == null) {
			introText = this.gameObject.GetComponent<Text> ();
		}
		introText.text = text;
	}
	
	// Update is called once per frame
	void Update () {
		if ((!loaded) && (Time.realtimeSinceStartup < wait)) {
			loaded = true;
			UnityEngine.SceneManagement.SceneManager.LoadScene (1);
		}
	}
}
