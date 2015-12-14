using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class TagSpotInit : MonoBehaviour {
	private Vector4 white = new Vector4 (0.8f, 0.0f, 0.0f, 0.0f);
	private Vector4 desiredColor;
	//white
	private Vector4 paintedColor;
	public bool isPainted = false;

	public bool paintedSuccess{
		get {
			return paintedColor == desiredColor;
		}
	}

	private SpriteRenderer tagSpotRender;
	private SpriteRenderer tagSpotEmoticon;

	public Sprite successEmoticon;
	public Sprite failureEmoticon;

	private PaintInventory pi;
	private ScoreKeeper sk;

	// Use this for initialization
	void Start () {
		desiredColor = white;
		paintedColor = white;
		//grab references to children
		foreach (SpriteRenderer sr in this.gameObject.GetComponentsInChildren<SpriteRenderer>()){
			if (sr.name == "TagSprite") {
				this.tagSpotRender = sr;
			} else if (sr.name == "TagSpotEmoticon") {
				this.tagSpotEmoticon = sr;
			}
		}
		sk = GameObject.FindGameObjectWithTag ("ScoreKeeper").GetComponent<ScoreKeeper>();
		pi = GameObject.FindGameObjectWithTag ("PaintInventory").GetComponent<PaintInventory>();
		//ensure desiredhue is valid
		// We dont have a desired hue. Lets get one, depending on what colors are available
		int colorIndex = Random.Range(0, pi.getCurrentAvailableHueCount());
		// We have a random index. Lets get the hue value for this color
		desiredColor = new Vector4 (pi.getHueFromColor(colorIndex), 0.9f, 0.5f, 0.0f);

		//set colors
		foreach (Material m in tagSpotRender.materials) {
			if (m.shader.name == "Custom/HSVRangeShader") {
				m.SetColor ("_HSVAAdjust", desiredColor);
			}
		}

		tagSpotEmoticon.enabled = false;
	}

	public void paintMe(){
		if (!isPainted) {
			float newlyPaintedHue = pi.selectedHue;
			Debug.Log ("Painting with hue "+newlyPaintedHue+", desiring "+desiredColor.x+" hue.");
			//mark as done
			isPainted = true;
			paintedColor = new Vector4 (newlyPaintedHue, 0.9f, 0.5f, 0.0f);
			//add emoticon
			if (paintedSuccess) {
				tagSpotEmoticon.sprite = successEmoticon;
			} else {
				tagSpotEmoticon.sprite = failureEmoticon;
			}
			tagSpotEmoticon.enabled = true;

			ExecuteEvents.Execute<IScoreKeeperTarget>(sk.gameObject, null,  (x,y) => x.TagCompleted(paintedSuccess));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!isPainted) {
			//same color, but White
			paintedColor = new Vector4 (desiredColor.x, 0.0f, 0.0f, 0.0f);
		}
		//pulse desired color and paintedcolor
		float progress = 0.5f + Mathf.Sin(Time.fixedTime * 5.0f)/2.0f;
		Vector4 curColor = Vector3.Lerp (paintedColor, desiredColor, progress);
		foreach (Material m in tagSpotRender.materials) {
			if (m.shader.name == "Custom/HSVRangeShader") {
				m.SetColor ("_HSVAAdjust", curColor);
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

	void OnTriggerStay2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			//coll.gameObject.SendMessage ("ApplyDamage", 10);
			paintMe ();
		}
	}
		
}
