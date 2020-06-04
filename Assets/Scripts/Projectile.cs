using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	public float xVel;
	Rigidbody2D rb;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	public void Initialize(Vector2 direction) {
		rb.velocity = direction * xVel;
	}

	private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == "Player") {
            PlayerAvatar player = other.gameObject.GetComponent<PlayerAvatar>();

            player.TakeDamage(1, this.gameObject);
        }
        Destroy(gameObject);
    }
}
