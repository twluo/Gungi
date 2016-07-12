using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

    private const bool BLACK = false;
    private const bool WHITE = true;
    public Camera whiteCamera;
    public Camera blackCamera;
    public GameObject tileObject;
    public Player whitePlayer;
    public Player blackPlayer;
    private bool player;
    private bool initialPhase;
    private static bool menuFlag;
    // Use this for initialization
    void Start () {
        menuFlag = false;
        initialPhase = true;
        player = WHITE;
        setUpCamera();
        setUpBoard();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            endTurn();
        }

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 150.0f) && !menuFlag) {
                GameObject hitObj = hit.transform.gameObject;
                if (initialPhase) {
                    Debug.Log(hitObj.tag);
                    Tile tile;
                    if (hitObj.tag.Contains("Territory"))
                        tile = hitObj.transform.GetComponent<Tile>();
                    else
                        tile = hitObj.transform.GetComponent<Piece>().getTile();
                    if (hitObj.tag.Contains("White") && player == WHITE) {
                        tile.printName();
                        RadialMenuSpawner.getInstance().openInitialPhaseMenu(whitePlayer, tile);
                        menuFlag = true;
                    }
                    else if (hitObj.tag.Contains("Black") && player == BLACK) {
                        tile.printName();
                        RadialMenuSpawner.getInstance().openInitialPhaseMenu(blackPlayer, tile);
                        menuFlag = true;
                    }
                }
            }
        }
    }

    public static void closeMenu() {
        menuFlag = false;
    }

    void endTurn() {
        player = !player;
        whiteCamera.gameObject.SetActive(!whiteCamera.gameObject.activeSelf);
        blackCamera.gameObject.SetActive(!blackCamera.gameObject.activeSelf);
    }

    int convertCoords (int x, int y) {
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
                    tile.GetComponent<Renderer>().material.color = Color.red;
                    tile.tag = "White Territory";
                }
                else if (i > 5) {
                    tile.GetComponent<Renderer>().material.color = Color.blue;
                    tile.tag = "Black Territory";
                }
                Tile tileScript = tile.GetComponent<Tile>();
                tileScript.x = j;
                tileScript.y = i;

            }
        }
    }
}