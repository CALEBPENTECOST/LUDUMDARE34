using UnityEngine;
using System.Collections;

public class TagSpotInit : MonoBehaviour {

	public float desiredHue = 0.5f;
	/// <summary>
	/// Negative value for not painted.
	/// Set this value to trigger graphic update.
	/// </summary>
	private float paintedHue = -1.0f;

	private bool isPainted = false;

	public bool paintedSuccess{
		get {
			return paintedHue == desiredHue;
		}
	}

	public SpriteRenderer tagSpotRender;
	public SpriteRenderer tagSpotBackRender;
	public SpriteRenderer tagSpotEmoticon;

	public Sprite successEmoticon;
	public Sprite failureEmoticon;

	private Vector4 whiteColor = new Vector4 (0.9f, 0.0f, 1.0f, 0.0f);

	// Use this for initialization
	void Start () {
		Vector4 desiredColor = new Vector4 (desiredHue, 0.9f, 0.5f, 0.0f);

		foreach (Material m in tagSpotRender.materials) {
			if (m.shader.name == "Custom/HSVRangeShader") {
				m.SetColor ("_HSVAAdjust", desiredColor);
			}
		}

		foreach (Material m in tagSpotBackRender.materials) {
			if (m.shader.name == "Custom/HSVRangeShader") {
				m.SetColor ("_HSVAAdjust", whiteColor);
			}
		}

		tagSpotEmoticon.enabled = false;
	}

	public void paintMe(float newlyPaintedHue){
		if (!isPainted && paintedHue >= 0.0f) {
			//mark as done
			isPainted = true;
			paintedHue = newlyPaintedHue;
			//paint background
			Vector4 paintedColor = new Vector4 (paintedHue, 0.9f, 0.5f, 0.0f);
			foreach (Material m in tagSpotBackRender.materials) {
				if (m.shader.name == "Custom/HSVRangeShader") {
					m.SetColor ("_HSVAAdjust", paintedColor);
				}
			}
			//add emoticon
			bool success = (paintedHue == desiredHue);
			if (success) {
				tagSpotEmoticon.sprite = successEmoticon;
			} else {
				tagSpotEmoticon.sprite = failureEmoticon;
			}
		}
		Debug.Log ("You tried to paint something that was already painted.");
	}
	
	// Update is called once per frame
	void Update () {

	}
}
