using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private List<InputCollection> inputs;

    void Start() {
        inputs.Add(new InputCollection());
    }

    void Update()
    {
        this.updateInputs();
    }

    private void updateInputs() {
        inputs[0].UpdateInputs(Input.GetAxis("Horizontal"), Input.GetButton("Jump"), Input.GetButton("Fire1"));
    }

    public InputCollection GetPlayerInput(int playerCount) {
        return inputs[playerCount];
    }
}

public class InputCollection {
    float horizontal;
    bool jump;
    bool attack;

    public void UpdateInputs(float horizontal, bool jump, bool attack) {
        this.horizontal = horizontal;
        this.jump = jump;
        this.attack = attack;
    }
}