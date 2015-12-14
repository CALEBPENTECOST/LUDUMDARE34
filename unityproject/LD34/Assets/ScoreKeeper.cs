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

    [SerializeField]
    private int numMatchesUntilDifficultyRaise = 10;
    private int numDifficultyRaises = -1;

    [SerializeField]
    private ld34MenuController theMenuController;

	//private Vector4 badColor = new Vector4 (0.0f, 0.9f, 0.5f, 0.0f);
	//private Vector4 goodColor = new Vector4 (0.341f, 0.9f, 0.5f, 0.0f);
	//private Vector4 neutralColor = new Vector4 (0.8f, 0.0f, 0.0f, 0.0f);

	private int totalScore;
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
		Debug.Assert (scoreText != null);
		scoreText.color = Color.white;
		scoreText.text = getScoreString();
	}

	private string getScoreString(){
		return "Score: " + totalScore + System.Environment.NewLine +
		"Highest Match Streak: " + highestMatchStreak + System.Environment.NewLine +
		"Highest Miss Streak: " + highestMissStreak + System.Environment.NewLine +
		//"Total Matched: " + totalMatched + System.Environment.NewLine +
		//"Total Missed: " + totalMatched + System.Environment.NewLine +
		"Time: " + (int)Time.realtimeSinceStartup;
	}

	void Update () {
		scoreText.text = getScoreString();
	}

	public void TagCompleted(bool matches){
		if (matches) {
			currentMatchStreak++;
            totalMatched++;
            if (highestMatchStreak < currentMatchStreak) {
				scoreText.color = Color.green;
				highestMatchStreak = currentMatchStreak;
			} else {
				scoreText.color = Color.white;
			}
			highestMatchStreak = Mathf.Max (currentMatchStreak, highestMatchStreak);
			currentMissStreak = 0;
			totalScore += currentMatchStreak;
		} else {
			currentMissStreak++;
			if (highestMissStreak < currentMissStreak){
				scoreText.color = Color.red;
				highestMissStreak = currentMissStreak;
			} else {
				scoreText.color = Color.white;
			}
			currentMatchStreak = 0;
		}
		scoreText.text = getScoreString();
		//Debug.Log (scoreText.text);

        //Debug.Log("Total matched: " + totalMatched);
        // Check our scores, is it time to raise/lower difficulty?
        if (totalMatched % numMatchesUntilDifficultyRaise == 0)
        {
            //Debug.Log("adding color");
            numDifficultyRaises++;
            if(numDifficultyRaises > 0)
            {
                // raise the diffuculty!
                theMenuController.unlockNewColor();
            }
        }

    }
}
