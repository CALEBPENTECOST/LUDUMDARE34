using UnityEngine;
using System.Collections;

public class TagSpotInit : MonoBehaviour {

	public float desiredHue = -0.5f;
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

	private PaintInventory pi;

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
		Vector4 desiredColor = new Vector4 (Mathf.Clamp(desiredHue,0.0f,1.0f), 0.9f, 0.5f, 0.0f);
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


		pi = GameObject.FindGameObjectWithTag ("PaintInventory").GetComponent<PaintInventory>();
	}

	public void paintMe(){
		float newlyPaintedHue = pi.selectedHue;
		if (!isPainted) {
			Debug.Log ("Painting with hue "+newlyPaintedHue+", desiring "+desiredHue+" hue.");
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
			tagSpotEmoticon.enabled = true;
		} else {
			Debug.Log ("You tried to paint something that was already painted.");
		}
	}
	
	// Update is called once per frame
	void Update () {
		//ensure desiredhue is valid
		if (desiredHue < 0.0f || desiredHue > 1.0f) {
            // We dont have a desired hue. Lets get one, depending on what colors are available
            int colorIndex = Random.Range(0, pi.getCurrentAvailableHueCount());

            // We have a random index. Lets get the hue value for this color
            desiredHue = pi.getHueFromColor(colorIndex);
		}

		//repaint desired color, just in case it changes.
		if (!isPainted) {
			Vector4 desiredColor = new Vector4 (desiredHue, 0.9f, 0.5f, 0.0f);
			foreach (Material m in tagSpotFrontRender.materials) {
				if (m.shader.name == "Custom/HSVRangeShader") {
					m.SetColor ("_HSVAAdjust", desiredColor);
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		Debug.Log(this.name + " was hit by " + coll.gameObject.name);
		if (coll.gameObject.tag == "Player") {
			//coll.gameObject.SendMessage ("ApplyDamage", 10);
			paintMe ();
		}
	}
}
