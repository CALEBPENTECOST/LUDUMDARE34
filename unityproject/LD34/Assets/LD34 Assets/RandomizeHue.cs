using UnityEngine;
using System.Collections;

public class RandomizeHue : MonoBehaviour {

	public float hueMin = 0.0f;
	public float hueMax = 1.0f;
	public float saturationMin = 0.4f;
	public float saturationMax = 0.6f;
	public float valueMin = 0.4f;
	public float valueMax = 0.6f;


	// Use this for initialization
	void Start () {
		
		float hue = Random.Range(hueMin, hueMax);
		float saturation = Random.Range(saturationMin, saturationMax);
		float value = Random.Range(valueMin, valueMax);
		Color randomColor = new Color (hue, saturation, value, 0.0f);

		foreach (Material m in GetComponent<Renderer>().materials) {
			if (m.shader.name == "Custom/HSVRangeShader") {
				m.color = randomColor;
				m.SetColor ("_Color", randomColor);
				m.SetColor ("_COLOR", randomColor);
				m.SetColor ("Alpha Color Key", randomColor);
				Debug.Log ("Changed color of " + this.name + " to " + randomColor.ToString () + ".");
			}
		}

	}
}
