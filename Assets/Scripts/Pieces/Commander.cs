using UnityEngine;
using System.Collections.Generic;

public class Commander : Piece {

	// Use this for initialization
	void Start () {
        id = 0;
        frontBack = FRONT;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override List<GameObject> getAvailableMoves() {
        int x = location.x;
        int y = location.y;
        List<GameObject> availableMoves = new List<GameObject>();
        for (int i = -1; i < 2; i++) {
            if ((x + i) < 0 || (x + i) > 8)
                continue;
            for (int j = -1; j < 2; j++) {
                if ((y + j) < 0 || (y + j) > 8)
                    continue;
                GameObject tile = board.transform.GetChild(board.convertCoords(x + i, y + j)).gameObject;
                if (tile.GetComponent<Tile>().canMove(transform.tag.Contains("White")) && !board.inCheck(x + i, y + j))
                    availableMoves.Add(tile);
            }
        }
        return availableMoves;
    }
}
