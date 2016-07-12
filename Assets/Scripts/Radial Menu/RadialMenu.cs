using UnityEngine;
using System.Collections;

public class RadialMenu : MonoBehaviour {

    public RadialButton pieceButton;
    public RadialButton backButton;

    public void openInitialPhaseMenu(Player player, Tile tile) {    
        for (int i = 0; i < player.frontPieces.Length; i++) {
            RadialButton newButton = Instantiate(pieceButton);
            newButton.transform.SetParent(transform, false);
            float theta = (2 * Mathf.PI / player.frontPieces.Length) * i;
            float x = Mathf.Sin(theta);
            float y = Mathf.Cos(theta);
            newButton.transform.localPosition = new Vector3(x, y, 0f) * 100;
            newButton.setData(player, player.frontPieces[i].piece, tile);
        }
        RadialButton newBackButton = Instantiate(backButton);
        newBackButton.transform.SetParent(transform, false);

    }
    void Update() {

    }

}
