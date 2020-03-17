using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public InputManager inputManager;

    private int playerNum;
    private Rigidbody2D rb;

    public float horizontalMovementModifier = 0.25f;
    public float jumpVelocity = 5.0f;
    public float fallMultiplier = 2.5f;
    public float lowFallMultiplier = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.playerNum = 0;
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputCollection ic = inputManager.GetPlayerInput(this.playerNum);
        this.handleInput(ic);
    }

    protected void handleInput(InputCollection ic) {
        // Handle horizontal
        float xChange = ic.GetHorizontal() * horizontalMovementModifier;
        this.transform.position = new Vector3(
            this.transform.position.x + xChange, 
            this.transform.position.y, 
            this.transform.position.z
        );

        // Handle jumping
        if (ic.GetJump()) {
            rb.velocity = Vector2.up * jumpVelocity;
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
}
