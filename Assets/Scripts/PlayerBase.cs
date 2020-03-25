using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public InputManager inputManager;

    private int playerNum;
    private Rigidbody2D rb;
    private bool grounded;

    public float horizontalMovementModifier;
    public float jumpVelocity;
    public float fallMultiplier;
    public float lowFallMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        this.playerNum = 0;
        this.rb = this.GetComponent<Rigidbody2D>();
        this.grounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        InputCollection ic = inputManager.GetPlayerInput(this.playerNum);
        this.handleMovement(ic);
    }

    protected void handleMovement(InputCollection ic) {
        // Handle horizontal
        float xChange = ic.GetHorizontal() * horizontalMovementModifier;
        transform.Translate(new Vector3(xChange * Time.deltaTime, 0, 0));

        // Handle jump
        if (ic.GetJump() && this.grounded) {
            rb.velocity = Vector2.up * jumpVelocity;
            this.grounded = false;
        }
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !ic.GetJumpHold()) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowFallMultiplier - 1) * Time.deltaTime;
        }
    }

    public void Die() {
        Destroy(this);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // TODO: more consistent raycast logic needed
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);
        if (hit.collider != null && hit.collider.tag == "Ground") {
            this.grounded = true;
        }
    }
}
