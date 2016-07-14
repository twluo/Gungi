using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour {

    private const bool BLACK = false;
    private const bool WHITE = true;
    private const int INITIALPHASETURNLIMIT = 46;
    public Camera whiteCamera;
    public Camera blackCamera;
    public GameObject tileObject;
    public Player whitePlayer;
    public Player blackPlayer;
    private bool player;
    public bool initialPhase;
    public bool uiHit;
    public List<GameObject> interactableTiles;
    private bool populateList;
    private GameObject selectedPiece;
    private int turnCount;

    // Use this for initialization
    void Start () {
        turnCount = 0;
        populateList = true;
        uiHit = false;
        initialPhase = true;
        player = WHITE;
        setUpCamera();
        setUpBoard();
        interactableTiles = new List<GameObject>();
        selectedPiece = null;
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < transform.childCount && initialPhase && populateList; i++) {
            GameObject T = transform.GetChild(i).gameObject;
            Tile Tscript = T.GetComponent<Tile>();
            if (T.tag.Contains("White") && player == WHITE && Tscript.canDrop(player)) {
                interactableTiles.Add(T);
                T.GetComponent<Renderer>().material.color = Color.magenta;
            }
            else if (T.tag.Contains("Black") && player == BLACK && Tscript.canDrop(player)) {
                interactableTiles.Add(T);
                T.GetComponent<Renderer>().material.color = Color.magenta;
            }
        }
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (uiHit) {
                uiHit = false;
                return;
            }
            if (Physics.Raycast(ray, out hit, 150.0f)) {
                RadialMenuSpawner.getInstance().closeMenu();
                GameObject hitObj = hit.transform.gameObject;
                if (initialPhase) {
                    handlePlacingUnits(hitObj);
                }
                else {
                    Debug.Log("Action Phase " + hitObj.tag);
                    Piece piece;
                    GameObject tile;
                    if (hitObj.tag.Contains("Piece")) {
                        piece = hitObj.transform.GetComponent<Piece>().location.getPiece(((player) ? "White" : "Black"));
                        tile = hitObj.transform.GetComponent<Piece>().getTile().gameObject;
                    }
                    else {
                        piece = hitObj.transform.GetComponent<Tile>().getPiece((player) ? "White" : "Black");
                        tile = hitObj;
                    }
                    if (interactableTiles.Contains(tile) && selectedPiece != null) {
                        move(selectedPiece, tile.GetComponent<Tile>());
                        endTurn();
                        return;
                    }
                    if (piece == null)
                        return;
                    selectPiece(piece.gameObject);
                    Debug.Log("Selected " + ((player) ? "White " : "Black ") + piece.name);
                }
            }
            else {
                RadialMenuSpawner.getInstance().closeMenu();
            }
        }
        populateList = false;
    }

    private void move(GameObject piece, Tile target) {
        Piece pieceScript = piece.GetComponent<Piece>();
        pieceScript.location.removePiece();
        pieceScript.location = target;
        pieceScript.tier = target.addPiece(piece);
        piece.transform.position = target.transform.position + new Vector3(0f, piece.transform.localScale.y / 2 + piece.transform.localScale.y * (pieceScript.tier - 1), 0f);
    }
    private void selectPiece(GameObject piece) {
        for (int i = 0; i < interactableTiles.Count; i++) {
            interactableTiles[i].GetComponent<Renderer>().material.color = Color.white;
        }
        selectedPiece = piece;
        interactableTiles = selectedPiece.GetComponent<Piece>().getAvailableMoves();
        for (int i = 0; i < interactableTiles.Count; i++) {
            interactableTiles[i].GetComponent<Renderer>().material.color = Color.cyan;
        }
    }
    private void handlePlacingUnits(GameObject hitObj) {
        Debug.Log("Initial Phase " + hitObj.tag);
        Tile tile;
        if (hitObj.tag.Contains("Territory"))
            tile = hitObj.transform.GetComponent<Tile>();
        else
            tile = hitObj.transform.GetComponent<Piece>().getTile();
        if (hitObj.tag.Contains("White") && player == WHITE) {
            tile.printName();
            RadialMenuSpawner.getInstance().openInitialPhaseMenu(whitePlayer, tile);
        }
        else if (hitObj.tag.Contains("Black") && player == BLACK) {
            tile.printName();
            RadialMenuSpawner.getInstance().openInitialPhaseMenu(blackPlayer, tile);
        }
    }

    public void endTurn() {
        player = !player;
        whiteCamera.gameObject.SetActive(!whiteCamera.gameObject.activeSelf);
        blackCamera.gameObject.SetActive(!blackCamera.gameObject.activeSelf);
        for (int i = 0; i < interactableTiles.Count; i++) {
            interactableTiles[i].GetComponent<Renderer>().material.color = Color.white;
        }
        interactableTiles.Clear();
        selectedPiece = null;
        populateList = true;
        turnCount++;
        if (turnCount == INITIALPHASETURNLIMIT)
            initialPhase = false;
    }

    public bool inCheck(int x, int y) {
        return false;
    }
    public int convertCoords (int x, int y) {
        return y * 9 + x;
    }

    void setUpCamera() {
        whiteCamera.gameObject.SetActive(player);
        blackCamera.gameObject.SetActive(!player);
    }

    void setUpBoard() {
        Vector3 pos = transform.localPosition;
        Vector3 scale = transform.localScale;
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                GameObject tile = Instantiate(tileObject);
                Vector3 newpos = pos + new Vector3(j * (scale.x * 10 + 0.5f), 0, i * (scale.z * 10 + 0.5f));
                tile.transform.localPosition = newpos;
                tile.transform.name = j + "-" + i;
                tile.transform.parent = this.transform;
                BoxCollider collider = tile.AddComponent<BoxCollider>();
                Destroy(tile.GetComponent<MeshCollider>());
                collider.isTrigger = true;
                if (i < 3) {
                    tile.tag = "White Territory";
                }
                else if (i > 5) {
                    tile.tag = "Black Territory";
                }
                Tile tileScript = tile.GetComponent<Tile>();
                tileScript.x = j;
                tileScript.y = i;

            }
        }
    }
}