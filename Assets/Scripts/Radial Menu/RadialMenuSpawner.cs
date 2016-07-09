using UnityEngine;
using System.Collections;

public class RadialMenuSpawner : MonoBehaviour {

    private static RadialMenuSpawner ins;
    public RadialMenu menu;
    private RadialMenu currMenu;
    
	void Awake() {
        ins = this;
	}

    public void openInitialPhaseMenu(Player player, Tile tile) {
        RadialMenu newMenu = Instantiate(menu);
        newMenu.transform.SetParent(transform, false);
        newMenu.transform.position = Input.mousePosition;
        currMenu = newMenu;
        currMenu.openInitialPhaseMenu(player, tile);
    }

    public void closeMenu() {
        if (currMenu != null)
            Destroy(currMenu.gameObject);
        Board.closeMenu();
    }

    public static RadialMenuSpawner getInstance() {
        return ins;
    }
}
