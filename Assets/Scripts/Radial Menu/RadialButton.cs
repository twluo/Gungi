using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RadialButton : MonoBehaviour {

    public Image iconBackground;
    public Image icon;
    private GameObject piece;
    private Tile tile;
    private Player player;
    private Board board;
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
        setBoard(tile.GetComponentInParent<Board>());
    }

    public void setBoard(Board b) {
        board = b;
    }

    public void spawnCharacter() {
        if (!tile.canInsert() || !tile.canDrop(player.player) )
            return;
        Debug.Log("Button PRess");
        GameObject newPiece = Instantiate(piece);
        newPiece.transform.SetParent(player.transform);
        Piece pieceScript = newPiece.GetComponent<Piece>();
        pieceScript.location = tile;
        pieceScript.tier = tile.addPiece(newPiece);
        newPiece.transform.position = tile.transform.position + new Vector3(0f, piece.transform.localScale.y/2 + piece.transform.localScale.y * (pieceScript.tier - 1), 0f);
        pieceScript.board = this.board;
        if (player.player) {
            newPiece.tag = "White Piece";
            newPiece.transform.Rotate(new Vector3(0, 180, 0));
        }
        else
            newPiece.tag = "Black Piece";
        player.frontPieces[pieceScript.id].count--;
        board.endTurn();
        closeMenu();
    }

    public void closeMenu() {
        RadialMenuSpawner.getInstance().closeMenu();
    }

    public void onEnter() {
        board.uiHit = true;

    }

    public void onExit() {
        board.uiHit = false;

    }
}
