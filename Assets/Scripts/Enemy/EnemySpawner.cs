using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	public GameObject enemyPrefab;
	public float width = 16.0f;
	public float height = 3.0f;
	public float enemySpeed = 5.0f;
	public float spawnDelay = 0.5f;
	
	float minX;
	float maxX;
	private bool moveLeft = true;

	// Use this for initialization
	void Start ()
	{
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		minX = leftMost.x + (width * 0.5f);
		maxX = rightMost.x - (width * 0.5f);
	
		SpawnUntilFull();
	}
	
	void SpawnEnemies()
	{
		if(!ReferenceEquals(enemyPrefab, null))
		{
			foreach(Transform child in this.transform)
			{
				GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
				enemy.transform.parent = child;
			}
		}
	}
	
	void SpawnUntilFull()
	{
		Transform freePosition = NextFreePosition();
		
		if(!ReferenceEquals(freePosition, null))
		{
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		
		if(!ReferenceEquals(NextFreePosition(), null))
		{
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}
	
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(this.transform.position, new Vector3(width, height));
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(moveLeft)
		{
			this.transform.position += Vector3.left * enemySpeed * Time.deltaTime;
		}
		else
		{
			this.transform.position += Vector3.right * enemySpeed * Time.deltaTime;
		}
		
		// Restrict the enemy formation to the game space
		float newX = Mathf.Clamp(transform.position.x, minX, maxX);
		if(newX == minX)
		{
			moveLeft = false;
		}
		else if(newX == maxX)
		{
			moveLeft = true;
		}
		
		if(AllMembersDead())
		{
			Debug.Log ("Empty formation!");
			SpawnUntilFull();
		}
	}
	
	Transform NextFreePosition()
	{
		foreach(Transform childPosition in this.transform)
		{
			if(childPosition.childCount == 0)
			{
				return childPosition;
			}
		}
		
		return null;
	}
	
	bool AllMembersDead()
	{
		foreach(Transform childPosition in this.transform)
		{
			if(childPosition.childCount > 0) {
				return false;
			}
		}
		
		return true;
	}
}
