using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public InputManager inputManager;

    private int playerNum;

    private float horizontalMovementModifier = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        this.playerNum = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InputCollection ic = inputManager.GetPlayerInput(this.playerNum);
        this.handleInput(ic);
    }

    protected void handleInput(InputCollection ic) {
        float xChange = ic.GetHorizontal() * horizontalMovementModifier;
        this.transform.position = new Vector3(
            this.transform.position.x + xChange, 
            this.transform.position.y, 
            this.transform.position.z
        );
    }

    public void Die() {
        Destroy(this);
    }
}
