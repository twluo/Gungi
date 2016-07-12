using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [System.Serializable]
    public class Piece {
        public GameObject piece;
        public int count;
    }

    public bool player;
    public Piece[] frontPieces;
    public Piece[] backPieces;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
