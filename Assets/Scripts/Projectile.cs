using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	public float xVel;
	public bool isRight;
	float yVel = 0.0f;
	Rigidbody2D rb;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void Update() {
		if (isRight) {
			rb.velocity = new Vector2(xVel, yVel);
		} else {
			rb.velocity = new Vector2(-1 * xVel, yVel);
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == "Player") {
            PlayerAvatar player = other.gameObject.GetComponent<PlayerAvatar>();

            player.TakeDamage(1, this.gameObject);
        }
        Destroy(gameObject);
    }
}
