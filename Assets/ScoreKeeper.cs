using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
	
	public static int score = 0;
	static Text scoreText;
	
	void Start()
	{
		scoreText = this.GetComponent<Text>();
		Reset();
		scoreText.text = score.ToString();
	}
	
	public void Score(int points)
	{
		score += points;
		scoreText.text = score.ToString();
	}
	
	public static void Reset()
	{
		score = 0;
	}
}
