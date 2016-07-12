using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {
    public const bool FRONT = true;
    public const bool BACK = false;

    public Tile location;
    public Sprite frontSprite;
    public Sprite backSprite;
    public int id;
    public string frontName;
    public string backName;
    public bool frontBack;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Tile getTile() {
        return location;
    }

    public Sprite getSprite() {
        return (frontBack) ? frontSprite : backSprite;
    }

    public Sprite getSprite(bool side) {
        return (side) ? frontSprite : backSprite;
    }

    public string getName() {
        return (frontBack) ? frontName : backName;
    }

    public string getName(bool side) {
        return (side) ? frontName : backName;
    }
}
