using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAvatar: MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private bool grounded;
    private int hitpoints;

    public float groundSpeed;
    public float jumpHeight;
    public float airAcceleration;
    public float walkAcceleration;
    public float airDeceleration;
    public float groundDeceleration;

    private GameObject[] spawns;

    private float m_xChange;
    private float m_yChange;
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
        respawn();
    }

    // Update is called once per frame
    void Update()
    {
        this.handleMovement();
        this.handleWraparound();
        this.handleDeath();
    }

    void respawn() {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.boxCollider = this.GetComponent<BoxCollider2D>();
        this.grounded = false;
        this.hitpoints = 3;

        spawns = GameObject.FindGameObjectsWithTag("Respawn");
        int index = Random.Range(0, spawns.Length);
        Vector3 pos = spawns[index].transform.position;
        transform.position = new Vector3(pos.x, pos.y, pos.z);
    }

    protected void handleMovement() {
        float acceleration = walkAcceleration;
        float deceleration = groundDeceleration;

        // Handle sideways movement
        float velocityX = Mathf.MoveTowards(rb.velocity.x, groundSpeed * this.m_xChange, acceleration * Time.deltaTime);
        if (this.m_xChange == 0) {
            velocityX = Mathf.MoveTowards(rb.velocity.x, 0, deceleration * Time.deltaTime);
        }

        // Handle vertical movement
        float velocityY = rb.velocity.y;
        if (this.grounded) {
            velocityY = 0;
            if (this.m_isJumping) {
                velocityY = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
                velocityY += Physics2D.gravity.y * Time.deltaTime;

                // don't allow a jump input to queue up
                // for immediately after the player lands
                this.m_isJumping = false;

                this.grounded = false;
            }
            if (rb.velocity.y < 0) {
                this.grounded = false;
            }
        }
        rb.velocity = new Vector2(velocityX, velocityY);
        transform.Translate(rb.velocity * Time.deltaTime);
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

    private void handleDeath() {
        if (this.hitpoints <= 0) {
            this.Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // TODO: more consistent raycast logic needed
        ColliderDistance2D colliderDistance = other.collider.Distance(boxCollider);

        if (colliderDistance.isOverlapped) {
            transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
        }

        if (other.collider.tag == "Ground") {
            if (colliderDistance.normal == Vector2.up) {
                this.grounded = true;
            }
        }

        if (other.collider.tag == "Player") {
            if (colliderDistance.normal == Vector2.up) {
                rb.velocity = Vector2.up * 2.5f;
            }
            if (colliderDistance.normal == Vector2.down) {
                this.hitpoints -= 1;
            }
        }
    }

    //////////////////////
    // Input Handling
    //////////////////////
    // Passing down events from HumanPlayer
    public void _onMove(InputValue value) {
        this.m_xChange = value.Get<Vector2>().x;
        Debug.Log("Avatar:m_xChange .. " + m_xChange + " .. " + this.m_xChange);
        this.m_yChange = value.Get<Vector2>().y;
    }

    public void _onJump() {
        if (this.grounded) {
            this.m_isJumping = true;
        }
    }
}
