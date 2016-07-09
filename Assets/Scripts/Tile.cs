using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
    public int x;
    public int y;
    public int numberPieces;
	// Use this for initialization
	void Start () {
        numberPieces = 0;
	}
	
	// Update is called once per frame
	void Update () {

    }
    

    public void printName() {
        Debug.Log(x + "-" + y);
    }
   


}
