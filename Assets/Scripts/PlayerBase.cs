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
    public Transform[] spawns;

    private float m_xChange;
    private bool m_isJumping;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.grounded = false;
        int index = Random.Range(0, spawns.Length);
        transform.position = new Vector3(spawns[index].position.x, spawns[index].position.y, spawns[index].position.z);
    }

    // Update is called once per frame
    void Update()
    {
        this.handleMovement();
    }

    protected void handleMovement() {

        transform.Translate(new Vector3());

        // Handle horizontal
        transform.Translate(new Vector3(this.m_xChange * this.horizontalMovementModifier * Time.deltaTime, 0, 0));

        // Handle jump
        if (this.m_isJumping && this.grounded) {
            rb.velocity = Vector2.up * jumpVelocity;
            this.grounded = false;
            this.m_isJumping = false;
        }
    }

    private void OnMove(InputValue value) {
        this.m_xChange = value.Get<Vector2>().x;
    }

    private void OnJump() {
        this.m_isJumping = true;
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
