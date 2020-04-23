using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoxiousGas : MonoBehaviour {

    private Hashtable playerTimeInGas;
    private int maxPlayerExposureTime = 400;

    // Start is called before the first frame update
    void Start() {
        playerTimeInGas = new Hashtable();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            PlayerAvatar player = other.gameObject.GetComponent<PlayerAvatar>();
            playerTimeInGas[player] = 0;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            PlayerAvatar player = other.gameObject.GetComponent<PlayerAvatar>();

            // if (!playerTimeInGas.Contains(player)) {
            //     playerTimeInGas[player] = 0;
            // }

            playerTimeInGas[player] = (int)playerTimeInGas[player] + 1;
            if ((int)playerTimeInGas[player] > maxPlayerExposureTime) {
                player.Die();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            PlayerAvatar player = other.gameObject.GetComponent<PlayerAvatar>();
            playerTimeInGas[player] = 0;
        }
    }
}
