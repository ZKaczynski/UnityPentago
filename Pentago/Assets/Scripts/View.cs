using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour{

    public Quarter[] qrts;
    
    private Tile[] tiles;


    public Sprite empty;
    public Sprite firstPlayer;
    public Sprite secondPlayer;


    void Awake(){
        qrts = GetComponentsInChildren<Quarter>();
        tiles = GetComponentsInChildren<Tile>();

        for (int q = 0; q < 4; q++) {
            for (int i = 0; i < 9; i++) {
                int viewIdx = 9 * q + i;
                int shift;
                if (q == 0) shift = 0;
                else if (q == 1) shift = 3;
                else if (q == 2) shift = 18;
                else shift = 21;
                int modelIdx = shift + 6 * (i / 3) + (i % 3);
                tiles[viewIdx].id = modelIdx;
            }
        }
    }


    public void Display(int[] info) {

        for (int i = 0; i < 36; i++) {

            if (info[tiles[i].id] == 2) tiles[i].GetComponent<SpriteRenderer>().sprite = firstPlayer;
            else if (info[tiles[i].id] == 1) tiles[i].GetComponent<SpriteRenderer>().sprite = secondPlayer;
            else tiles[i].GetComponent<SpriteRenderer>().sprite = empty;
            
        }

    }

}
