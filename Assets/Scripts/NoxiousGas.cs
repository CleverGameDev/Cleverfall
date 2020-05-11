using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoxiousGas : MonoBehaviour {

    void OnParticleCollision(GameObject other) {

        if (other.gameObject.tag == "Player") {
            PlayerAvatar player = other.gameObject.GetComponent<PlayerAvatar>();

            player.TakeDamage(1, this.gameObject);
        }
    }
}
