using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoxiousGas : MonoBehaviour {

    public ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start() {
        this.particleSystem = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other) {
        if (other.gameObject.tag == "Player") {
            PlayerAvatar player = other.gameObject.GetComponent<PlayerAvatar>();

            player.TakeDamage(1, this.gameObject);
        }
    }
}
