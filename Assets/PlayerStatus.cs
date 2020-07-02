using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
    public TextMeshProUGUI player;
    public TextMeshProUGUI item;
    public TextMeshProUGUI hitPoints;

    private HumanPlayer humanPlayer;
    public HumanPlayer HumanPlayer {
        get { return humanPlayer; }
        set { humanPlayer = value; }
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        player.text = humanPlayer.GetCharacter();
        GameObject paGameObject = humanPlayer.PlayerAvatar;
        if (paGameObject != null) {
            PlayerAvatar pa = paGameObject.GetComponent<PlayerAvatar>();
            item.text = pa.GetWeapon();
            hitPoints.text = "HP = " + pa.Hitpoints;
        }
    }

}
