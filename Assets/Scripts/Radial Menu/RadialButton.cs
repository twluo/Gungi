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
        icon.sprite = piece.GetComponent<Piece>().frontSprite;
    }

    public void spawnCharacter() {
        if (!tile.canInsert())
            return;
        GameObject newPiece = Instantiate(piece);
        newPiece.transform.SetParent(player.transform);
        Piece pieceScript = newPiece.GetComponent<Piece>();
        pieceScript.location = tile;
        pieceScript.tier = tile.addPiece(newPiece);
        newPiece.transform.position = tile.transform.position + new Vector3(0f, piece.transform.localScale.y/2 + piece.transform.localScale.y * (pieceScript.tier - 1), 0f);
        if (player.player) {
            newPiece.tag = "White Piece";
            newPiece.transform.Rotate(new Vector3(0, 180, 0));
        }
        else
            newPiece.tag = "Black Piece";
        player.frontPieces[piece.GetComponent<Piece>().id].count--;
        tile.transform.gameObject.GetComponentInParent<Board>().endTurn();
        closeMenu();
    }

    public void closeMenu() {
        RadialMenuSpawner.getInstance().closeMenu();
    }
}
