using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
	public void LoadLevel(string name)
	{
		Debug.Log("Level load requested for level named: '" + name + "'");
		Application.LoadLevel(name);
	}
	
	public void QuitRequest()
	{
		Debug.Log("Quit request function called");
		Application.Quit();
	}
	
	public void LoadNextLevel()
	{
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}