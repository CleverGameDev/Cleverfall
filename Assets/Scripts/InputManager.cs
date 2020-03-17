using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private List<InputCollection> inputs = new List<InputCollection>();

    public int totalPlayerNumber;

    void Start() {
        inputs = new List<InputCollection>();
        for (int i = 0; i < this.totalPlayerNumber; i++) {
            inputs.Add(new InputCollection());
        }
    }

    void Update()
    {
        this.updateInputs();
    }

    private void updateInputs() {
        inputs[0].UpdateInputs(Input.GetAxis("Horizontal"), Input.GetButtonDown("Jump"), Input.GetButton("Jump"), Input.GetButton("Fire1"));
    }

    public InputCollection GetPlayerInput(int playerCount) {
        return this.inputs[playerCount];
    }
}

public class InputCollection {
    float horizontal;
    bool jump;
    bool jumpHold;
    bool attack;

    public InputCollection() {
        
    }

    public void UpdateInputs(float horizontal, bool jump, bool jumpHold, bool attack) {
        this.horizontal = horizontal;
        this.jump = jump;
        this.jumpHold = jumpHold;
        this.attack = attack;
    }

    public float GetHorizontal() {
        return this.horizontal;
    }

    public bool GetJump() {
        return this.jump;
    }

    public bool GetJumpHold() {
        return this.jumpHold;
    }

    public bool GetAttack() {
        return this.attack;
    }
}