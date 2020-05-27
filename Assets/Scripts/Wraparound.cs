using UnityEngine;
using UnityEngine.Assertions;

public class Wraparound : MonoBehaviour {

    private const float wraparoundBuffer = 0.01f;

    private CompositeCollider2D cc;

    private void Start() {
        cc = this.GetComponent<CompositeCollider2D>();
        Assert.IsNotNull(cc);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // If a player enters, do wraparound movement
        if (other.tag == "Player") {
            ColliderDistance2D colliderDistance = other.Distance(cc);
            Vector3 p = other.transform.position;

            // Wraparound movement logic assumes the level is symmetrical about the origin
            if (Mathf.Abs(colliderDistance.normal.x) > Mathf.Abs(colliderDistance.normal.y)) {
                // wrap around horizontally
                float buf = (wraparoundBuffer * Mathf.Sign(colliderDistance.normal.x));
                other.transform.position = new Vector3(-1 * (p.x - buf), p.y, p.z);
            } else {
                // wrap around vertically
                float buf = wraparoundBuffer * Mathf.Sign(colliderDistance.normal.y);
                other.transform.position = new Vector3(p.x, -1 * (p.y - buf), p.z);
            }
        }
    }
}
