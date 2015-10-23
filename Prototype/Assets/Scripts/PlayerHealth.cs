using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{	
	public float health = 100f;                 // The player's health.
    public float anger = 0f;
    public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
	public AudioClip[] ouchClips;				// Array of clips to play when the player is damaged.
	public float hurtForce = 10f;				// The force with which the player is pushed when hurt.
	public float damageAmount = 10f;			// The amount of damage to take when enemies touch the player

	private SpriteRenderer healthBar;           // Reference to the sprite renderer of the health bar.
    private SpriteRenderer angerBar;
    private float lastHitTime;					// The time at which the player was last hit.
	private Vector3 healthScale;                // The local scale of the health bar initially (with full health).
    private Vector3 angerScale;
    private PlayerControl playerControl;		// Reference to the PlayerControl script.
	private Animator anim, anim1;						// Reference to the Animator on the player


	void Awake ()
	{
		// Setting up references.
		playerControl = GetComponent<PlayerControl>();
		healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();

		// Getting the intial scale of the healthbar (whilst the player has full health).
		healthScale = healthBar.transform.localScale;
        angerBar = GameObject.Find("AngerBar").GetComponent<SpriteRenderer>();
        anim1 = GetComponent<Animator>();
        angerScale = angerBar.transform.localScale;
	}


	void OnCollisionEnter2D (Collision2D col)
	{
		// If the colliding gameobject is an Enemy...
		if(col.gameObject.tag == "Damage")
		{
			// ... and if the time exceeds the time of the last hit plus the time between hits...
			if (Time.time > lastHitTime + repeatDamagePeriod) 
			{
				// ... and if the player still has health...
				if(health > 0f)
				{
					// ... take damage and reset the lastHitTime.
					TakeDamage(col.transform); 
					lastHitTime = Time.time; 
				}
                // If the player doesn't have health, do some stuff, let him fall into the river to reload the level.
                else Application.LoadLevel("Prototype");
			}
		}
	}


	public void TakeDamage (Transform enemy)
	{
		// Make sure the player can't jump.
		playerControl.jump = false;

		// Create a vector that's from the enemy to the player with an upwards boost.
		Vector3 hurtVector = transform.position /*- enemy.position*/ + Vector3.up * 2f;

		// Add a force to the player in the direction of the vector and multiply by the hurtForce.
		GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);

		// Reduce the player's health by 10.
		health -= damageAmount;
        //damageAmount += 10;
        anger = 100 - health;

		// Update what the health bar looks like.
		UpdateHealthBar();
        UpdateAngerBar();
        UpdateAbility();

		// Play a random clip of the player getting hurt.
		int i = Random.Range (0, ouchClips.Length);
		AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
	}


	public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);

		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
	}
    public void UpdateAngerBar()
    {
        // Set the anger bar's colour to proportion of the way between green and red based on the player's health.
		angerBar.material.color = Color.Lerp(Color.green, Color.red, anger * 0.01f);

		// Set the scale of the anger bar to be proportional to the player's health.
		angerBar.transform.localScale = new Vector3(angerScale.x * anger * 0.01f, 1, 1);
    }
    public void UpdateAbility()
    {
        GetComponent<PlayerControl>().jumpForce = 200 + anger*5;
    }
}