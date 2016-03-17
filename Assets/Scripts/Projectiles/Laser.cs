using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public float damage = 100.0f;
	
	public float GetDamage()
	{
		return damage;
	}
	
	public void Hit()
	{
		Destroy(this.gameObject);
	}
}
