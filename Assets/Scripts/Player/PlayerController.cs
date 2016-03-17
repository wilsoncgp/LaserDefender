using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public GameObject laserPrefab;
	public AudioClip laserFireSound;
	public AudioClip playerDieSound;
	
	public float playerSpeed = 15.0f;
	public float padding = 1.0f;
	public float laserSpeed = 5.0f;
	public float firingRate = 0.2f;
	public float health = 250.0f;

	float minX;
	float maxX;
	
	void OnTriggerEnter2D(Collider2D col)
	{
		Laser enemyLaser = col.gameObject.GetComponent<Laser>();
		if(!ReferenceEquals(enemyLaser, null))
		{
			health -= enemyLaser.GetDamage();
			enemyLaser.Hit();
			if(health <= 0)
			{
				Die ();
			}
		}
	}
	
	void Die()
	{
		LevelManager levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		levelManager.LoadLevel("Win");
		Destroy(this.gameObject);
		AudioSource.PlayClipAtPoint(playerDieSound, this.transform.position);
	}
	
	// Use this for initialization
	void Start ()
	{
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		minX = leftMost.x + padding;
		maxX = rightMost.x - padding;
	}
	
	void Fire()
	{
		if(!ReferenceEquals(laserPrefab, null))
		{
			GameObject laser = Instantiate(laserPrefab, this.transform.position, Quaternion.identity) as GameObject;
			laser.rigidbody2D.velocity = Vector3.up * laserSpeed;
			AudioSource.PlayClipAtPoint(laserFireSound, this.transform.position);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			this.transform.position += Vector3.left * playerSpeed * Time.deltaTime;
		}
		
		if(Input.GetKey(KeyCode.RightArrow))
		{
			this.transform.position += Vector3.right * playerSpeed * Time.deltaTime;
		}
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}
		
		if(Input.GetKeyUp(KeyCode.Space))
		{
			CancelInvoke("Fire");
		}
		
		// Restrict the player to the game space
		float newX = Mathf.Clamp(transform.position.x, minX, maxX);
		transform.position = new Vector3(newX, this.transform.position.y, this.transform.position.z);
	}
}
