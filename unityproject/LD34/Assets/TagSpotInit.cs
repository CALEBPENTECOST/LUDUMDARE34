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

	private SpriteRenderer tagSpotFrontRender;
	private SpriteRenderer tagSpotBackRender;
	private SpriteRenderer tagSpotEmoticon;

	public Sprite successEmoticon;
	public Sprite failureEmoticon;

	private Vector4 whiteColor = new Vector4 (0.9f, 0.0f, 1.0f, 0.0f);

	// Use this for initialization
	void Start () {
		//grab references to children
		foreach (SpriteRenderer sr in this.gameObject.GetComponentsInChildren<SpriteRenderer>()){
			if (sr.name == "TagSpotFront") {
				this.tagSpotFrontRender = sr;
			} else if (sr.name == "TagSpotBack") {
				this.tagSpotBackRender = sr;
			} else if (sr.name == "TagSpotEmoticon") {
				this.tagSpotEmoticon = sr;
			}
		}

		//set colors
		Vector4 desiredColor = new Vector4 (desiredHue, 0.9f, 0.5f, 0.0f);
		foreach (Material m in tagSpotFrontRender.materials) {
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
		//repaint desired color, just in case it changes.
		if (!isPainted && paintedHue >= 0.0f) {
			Vector4 desiredColor = new Vector4 (desiredHue, 0.9f, 0.5f, 0.0f);
			foreach (Material m in tagSpotFrontRender.materials) {
				if (m.shader.name == "Custom/HSVRangeShader") {
					m.SetColor ("_HSVAAdjust", desiredColor);
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			//coll.gameObject.SendMessage ("ApplyDamage", 10);
			Debug.Log(this.name + " was hit by " + coll.gameObject.name);
			paintMe (this.desiredHue);
		}
	}
}
