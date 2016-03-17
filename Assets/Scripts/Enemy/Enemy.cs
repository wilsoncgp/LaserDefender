using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public AudioClip laserFireSound;
	public AudioClip enemyDieSound;
	
	public float health = 150.0f;
	public GameObject laserPrefab;
	public float laserSpeed = 5.0f;
	public int scoreValue = 150;
	
	public float minTimeBetweenFire = 0.5f;
	public float maxTimeBetweenFire = 2.0f;
	
	private ScoreKeeper scoreKeeper;

	void OnTriggerEnter2D(Collider2D col)
	{
		Laser laser = col.gameObject.GetComponent<Laser>();
		if(!ReferenceEquals(laser, null))
		{
			health -= laser.GetDamage();
			laser.Hit();
			if(health <= 0)
			{
				Destroy(this.gameObject);
				AudioSource.PlayClipAtPoint(enemyDieSound, this.transform.position);
				scoreKeeper.Score(scoreValue);
			}
		}
	}
	
	void Start()
	{
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	
		ScheduleLaser();
	}
	
	void ScheduleLaser()
	{
		Invoke("Fire", Random.Range(minTimeBetweenFire, maxTimeBetweenFire));
	}
	
	void Fire()
	{
		if(!ReferenceEquals(laserPrefab, null))
		{
			GameObject laser = Instantiate(laserPrefab, this.transform.position, Quaternion.identity) as GameObject;
			laser.rigidbody2D.velocity = Vector3.down * laserSpeed;
			AudioSource.PlayClipAtPoint(laserFireSound, this.transform.position);
		}
		
		ScheduleLaser();
	}
	
	void Update()
	{
	}
}
