using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
    public int x;
    public int y;
    public GameObject Tier1 = null;
    public GameObject Tier2 = null;
    public GameObject Tier3 = null;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    }
    

    public void printName() {
        Debug.Log(x + "-" + y);
    }
   
    public int addPiece(GameObject piece) {
        if (Tier1 == null) {
            Tier1 = piece;
            return 1;
        }
        else if (Tier2 == null) {
            Tier2 = piece;
            return 2;
        }
        else if (Tier3 == null) {
            Tier3 = piece;
            return 3;
        }
        return 0;
    }

    public bool canInsert() {
        return Tier3 == null;
    }

    public void removePiece() {
        if (Tier3 != null)
            Tier3 = null;
        else if (Tier2 != null)
            Tier2 = null;
        else if (Tier1 != null)
            Tier1 = null;
    }

    public bool canRemove() {
        return Tier1 != null;
    }

}
