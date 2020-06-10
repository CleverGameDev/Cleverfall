using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	public float speed;
	public int damage;
	public int duration;

	private Rigidbody2D rb;
	private PlayerAvatar owner;
	private int remainingDuration;


	void Start() {
		this.rb = this.GetComponent<Rigidbody2D>();
		this.remainingDuration = duration;
	}

	void Update() {
		if (this.remainingDuration <= 0) {
			Destroy(gameObject);
		}
		this.remainingDuration--;
	}

	public void SetDirection(Vector2 direction) {
		if (!rb) {
			this.rb = this.GetComponent<Rigidbody2D>();
		}
		this.rb.velocity = direction * this.speed;
	}

	public void SetOwner(PlayerAvatar pa) {
		this.owner = pa;
	}

	public PlayerAvatar GetOwner() {
		return this.owner;
	}

	private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == "Player") {
            PlayerAvatar player = other.gameObject.GetComponent<PlayerAvatar>();

            player.TakeDamage(damage, this.gameObject);
        }
        Destroy(gameObject);
    }
}
