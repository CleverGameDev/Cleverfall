using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBase : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool grounded;

    public float horizontalMovementModifier;
    public float jumpVelocity;

    private float xChange;
    private bool jump;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.grounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.handleMovement();
    }

    protected void handleMovement() {

        transform.Translate(new Vector3());

        // Handle horizontal
        transform.Translate(new Vector3(this.xChange * this.horizontalMovementModifier * Time.deltaTime, 0, 0));

        // Handle jump
        if (this.jump && this.grounded) {
            rb.velocity = Vector2.up * jumpVelocity;
            this.grounded = false;
            this.jump = false;
        }
    }

    private void OnMove(InputValue value) {
        xChange = value.Get<Vector2>().x;
    }

    private void OnJump() {
        this.jump = true;
    }

    public void Die() {
        Destroy(this);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // TODO: more consistent raycast logic needed
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);
        if (hit.collider != null) {
            this.grounded = true;
        }
    }
}
