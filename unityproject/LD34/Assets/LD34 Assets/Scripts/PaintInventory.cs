using UnityEngine;
using System.Collections;

public class PaintInventory : MonoBehaviour {

	public float[] hues;
	public float selectedHue{
		get {
			return _selectedHue;
		}
		set {
			_selectedHue = value;
			Debug.Log ("Equiping hue "+value.ToString() +".");
		}
	}

	private float _selectedHue;

	// Use this for initialization
	void Start () {
		if ((hues == null) || (hues.Length == 0)) {
			hues = new float[8];
			for (int i = 0; i < hues.Length; i++) {
				hues [i] = ((float)(1.0f / hues.Length)) * i;
			}
		}
		selectedHue = hues [2];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
