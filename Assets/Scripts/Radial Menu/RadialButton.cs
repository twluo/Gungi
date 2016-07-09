using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RadialButton : MonoBehaviour {

    public Image iconBackground;
    public Image icon;
    private GameObject piece;
    private Tile tile;
    private Player player;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setData(Player player, GameObject piece, Tile tile) {
        this.player = player;
        this.piece = piece;
        this.tile = tile;
    }
    public void spawnCharacter() {
        GameObject newPiece = Instantiate(piece);
        newPiece.transform.SetParent(player.transform);
        newPiece.GetComponent<Piece>().location = tile;
        newPiece.transform.position = tile.transform.position + new Vector3(0f, piece.transform.localScale.y/2 + piece.transform.localScale.y * tile.numberPieces, 0f);
        tile.numberPieces++;
    }

    public void closeMenu() {
        RadialMenuSpawner.getInstance().closeMenu();
    }
}
