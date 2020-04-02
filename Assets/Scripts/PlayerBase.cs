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

    private GameObject[] spawns;

    private float m_xChange;
    private bool m_isJumping;

    // [PN] TODO: make these constraints flexible depending on stage
    private const float leftConstraint = -9.4f;
    private const float rightConstraint = 9.4f;
    private const float bottomConstraint = -4.6f;
    private const float topConstraint = 4.6f;
    private const float buffer = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.grounded = false;
        spawns = GameObject.FindGameObjectsWithTag("Respawn");

        int index = Random.Range(0, spawns.Length);
        Vector3 pos = spawns[index].transform.position;
        transform.position = new Vector3(pos.x, pos.y, pos.z);
    }

    // Update is called once per frame
    void Update()
    {
        this.handleMovement();
        this.handleWraparound();
    }

    protected void handleMovement() {

        transform.Translate(new Vector3());

        // Handle horizontal
        transform.Translate(new Vector3(this.m_xChange * this.horizontalMovementModifier * Time.deltaTime, 0, 0));

        // Handle jump
        if (this.m_isJumping) {
            // only jump if player is bounded
            if (this.grounded) {
                rb.velocity = Vector2.up * jumpVelocity;
                this.grounded = false;
            }
            // don't allow a jump input to queue up
            // for immediately after the player lands
            this.m_isJumping = false;
        }
    }

    private void handleWraparound() {
        // Handle wraparound on the x-axis
        if (transform.position.x < leftConstraint - buffer) {
            transform.position = new Vector3(rightConstraint + buffer,transform.position.y, transform.position.z);
        } else if (transform.position.x > rightConstraint + buffer) {
            transform.position = new Vector3(leftConstraint - buffer,transform.position.y, transform.position.z);
        }

        // Handle wraparound on the y-axis
        if (transform.position.y < bottomConstraint - buffer) {
            transform.position = new Vector3(transform.position.x, topConstraint + buffer, transform.position.z);
        } else if (transform.position.y > topConstraint + buffer) {
            transform.position = new Vector3(transform.position.x, bottomConstraint - buffer, transform.position.z);
        }
    }

    private void OnMove(InputValue value) {
        this.m_xChange = value.Get<Vector2>().x;
    }

    private void OnJump() {
        this.m_isJumping = true;
    }

    private void OnMenu() {
        GameObject.Find("PauseMenu").GetComponent<PauseMenu>().TogglePause();
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
