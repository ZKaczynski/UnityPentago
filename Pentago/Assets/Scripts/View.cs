using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour{

    [SerializeField]
    private Quarter[] qrts;
    
    private Tile[] tiles;


    public Sprite empty;
    public Sprite firstPlayer;
    public Sprite secondPlayer;


    void Awake(){
        qrts = GetComponentsInChildren<Quarter>();
        tiles = GetComponentsInChildren<Tile>();

        for (int i = 0; i < 36; i++) {
            tiles[i].id = i;
        }
    }//START HERE


    public void Display(int[] info) {

        for (int q = 0; q < 4; q++) {
            for (int i = 0; i < 9; i++) {
                int viewIdx = 9*q+i;
                int shift;
                if (q == 0) shift = 0;
                else if (q == 1) shift = 3;
                else if (q == 2) shift = 18;
                else  shift = 21;
                int modelIdx = shift + 6 * (i / 3) + (i % 3);
                if (info[modelIdx] == 2) tiles[viewIdx].GetComponent<SpriteRenderer>().sprite = firstPlayer;
                else if (info[modelIdx] == 1) tiles[viewIdx].GetComponent<SpriteRenderer>().sprite = secondPlayer;
                else tiles[viewIdx].GetComponent<SpriteRenderer>().sprite = empty;
            }
        }

    }

}
