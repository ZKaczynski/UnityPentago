using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quarter : MonoBehaviour{
    public Tile[] tiles;

    private Quaternion initial;

    void Awake() {
        initial = transform.rotation;
    }

    void Start(){
        tiles = GetComponentsInChildren<Tile>();
    }



    public void ResetTransform() {
        transform.rotation = initial;
        foreach(Tile t in tiles) {
            t.ResetTransform();
        }
    }


    public void RotateClockwise() {
        RotateTilesCouterClockwise();
        StartCoroutine("RotClockwise");
    }

    public void RotateCounterClockwise() {
        RotateTilesClockwise();
        StartCoroutine("RotCounterClockwise");
    }


    private void RotateTilesClockwise() {
        foreach (Tile t in tiles) {
            t.RotClockwise();
        }
    }

    private void RotateTilesCouterClockwise() {
        foreach (Tile t in tiles) {
            t.RotCounterClockwise();
        }
    }

    IEnumerator RotClockwise() {
        for (float i = 0; i < 90; i += 1f) {
            transform.Rotate(Vector3.back, 1f);
            yield return null;
        }
    }

    IEnumerator RotCounterClockwise() {
        for (float i = 0; i < 90; i += 1f) {
            transform.Rotate(Vector3.forward, 1f);
            yield return null;
        }
    }

}
