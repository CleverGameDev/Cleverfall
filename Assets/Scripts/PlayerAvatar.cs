using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAvatar : MonoBehaviour {
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private bool grounded;
    private int hitpoints;

    public GameObject projectilePrefab;
    public float groundSpeed;
    public float jumpHeight;
    public float airAcceleration;
    public float walkAcceleration;
    public float airDeceleration;
    public float groundDeceleration;

    public AudioSource[] jumpSounds;
    public AudioSource takeDamageSound;
    public AudioSource dieSound;
    public AudioSource playerSpawnSound;

    private GameObject[] spawns;

    private HumanPlayer humanPlayer;

    private float m_xChange;
    private float m_yChange;
    private bool m_isJumping;

    public float fireDelay = 0.5F;

    private GameObject newProjectile;
    private float fireCooldown = 0.0F;

    // Start is called before the first frame update
    void Start() {
        respawn();
    }

    // Update is called once per frame
    void Update() {
        if (isDead()) {
            return;
        }

        this.handleMovement();
        this.handleAttack();
    }

    void respawn() {
        playerSpawnSound.Play();

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
        // TODO: try to figure out what causes the "jittering avatar" bug
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
                velocityY += Physics2D.gravity.y * Time.deltaTime; // TODO: have a max velocityY so it doesn't go crazy if player loops from top to bottom of level over and over

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

    protected void handleAttack() {
        fireCooldown = fireCooldown - Time.deltaTime;

        Vector3 projectilePos = transform.position;
        if (Input.GetButton("Fire1") && fireCooldown <= 0)
        {
            // Don't spawn right on top of player
            projectilePos += new Vector3(1.0f, 0.0f, 0.0f);
            newProjectile = Instantiate(projectilePrefab, projectilePos, Quaternion.identity) as GameObject;
            fireCooldown = fireDelay;
        }
    }

    public void TakeDamage(int amount, GameObject source) {
        takeDamageSound.Play();

        this.hitpoints -= amount;
        if (this.hitpoints <= 0) {
            dieSound.Play();

            // Die!
            this.humanPlayer.AddDeath();
            StartCoroutine(scheduleForDestruction());

            // Give a kill to whoever pwn'd this player
            PlayerAvatar killer = source.GetComponent<PlayerAvatar>();
            if (killer != null) {
                killer.humanPlayer.AddKill();
            }
        }
    }

    private bool isDead() {
        return hitpoints <= 0;
    }

    // scheduleForDestruction 
    // FUTURE: move all sound to a SoundManager, which exists independent of this
    //         -or- don't use Destroy(gameObject) to manage killing of PlayerAvatar
    public IEnumerator scheduleForDestruction() {
        // give time for death sounds to play (<1s)
        // FUTURE: add a death animation
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    public void SetHumanPlayer(HumanPlayer hp) {
        humanPlayer = hp;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // TODO: more consistent raycast logic needed
        ColliderDistance2D colliderDistance = other.collider.Distance(boxCollider);

        // if collision causes an overlap, move the object so that it's no longer overlapping

        if (colliderDistance.isOverlapped) {
            transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
        }

        if (other.collider.tag == "Collideable") {
            if (colliderDistance.normal == Vector2.up) {
                this.grounded = true;
            }
        }

        if (other.collider.tag == "Player") {
            if (colliderDistance.normal == Vector2.up) {
                rb.velocity = Vector2.up * 2.5f;
            }
            if (colliderDistance.normal == Vector2.down) {
                TakeDamage(1, other.gameObject);
            }
        }
    }

    //////////////////////
    // Input Handling
    //////////////////////
    // Passing down events from HumanPlayer
    public void _onMove(InputValue value) {
        this.m_xChange = value.Get<Vector2>().x;
        this.m_yChange = value.Get<Vector2>().y;
    }

    public void _onJump() {
        if (this.grounded) {
            jumpSounds[Random.Range(0, jumpSounds.Length)].Play();
            this.m_isJumping = true;
        }
    }
}
