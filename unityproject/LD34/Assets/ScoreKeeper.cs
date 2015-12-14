using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public interface IScoreKeeperTarget : IEventSystemHandler {
	void TagCompleted(bool matches);
}



public class ScoreKeeper : MonoBehaviour, IScoreKeeperTarget {
	public int currentMatchStreak;
	public int highestMatchStreak;
	public int totalMatched;

	public int currentMissStreak;
	public int highestMissStreak;
	public int totalMissed;

	//private Vector4 badColor = new Vector4 (0.0f, 0.9f, 0.5f, 0.0f);
	//private Vector4 goodColor = new Vector4 (0.341f, 0.9f, 0.5f, 0.0f);
	//private Vector4 neutralColor = new Vector4 (0.8f, 0.0f, 0.0f, 0.0f);

	private int totalScore{
		get {
			return highestMatchStreak - highestMissStreak;
		}
	}

	public Text scoreText;

	// Use this for initialization
	void Start () {
		currentMatchStreak = 0;
		highestMatchStreak = 0;
		totalMatched = 0;
		currentMissStreak = 0;
		highestMissStreak = 0;
		totalMissed = 0;

		if (scoreText == null) {
			scoreText = this.gameObject.GetComponent<Text> ();
		}
		scoreText.color = Color.white;
		scoreText.text = "Score: 0";
	}

	public void TagCompleted(bool matches){
		if (matches) {
			currentMatchStreak++;
			if (highestMatchStreak < currentMatchStreak) {
				scoreText.color = Color.green;
			} else {
				scoreText.color = Color.white;
			}
			highestMatchStreak = Mathf.Max (currentMatchStreak, highestMatchStreak);
			currentMissStreak = 0;
		} else {
			currentMissStreak++;
			if (highestMissStreak < currentMissStreak){
				scoreText.color = Color.red;
			} else {
				scoreText.color = Color.white;
			}
			currentMatchStreak = 0;
		}
		scoreText.text = "Score: " + totalScore;
	}
}
