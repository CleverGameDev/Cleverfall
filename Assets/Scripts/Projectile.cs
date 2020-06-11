using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {
	public float speed;
	public int damage;
	public float duration;

	private Rigidbody2D rb;
	private PlayerAvatar owner;

	void Awake() {
		this.rb = this.GetComponent<Rigidbody2D>();
		StartCoroutine(scheduleForDestruction()); 
	}

	public void SetDirection(Vector2 direction) {
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

    private IEnumerator scheduleForDestruction() {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
