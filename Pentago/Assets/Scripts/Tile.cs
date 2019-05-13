using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour{

    private Quaternion initial;
    private SpriteRenderer sr;
    public int id;

    void Awake() {
        initial = transform.rotation;
        sr = GetComponent<SpriteRenderer>();

    }

    public void SetSprite(Sprite s) {
        sr.sprite = s;
    }

    public void ResetTransform() {
        transform.rotation = initial;
    }

    public void RotClockwise() {
        StartCoroutine("CRotClockwise");
    }

    public void RotCounterClockwise() {
        StartCoroutine("CRotCounterClockwise");
    }

    IEnumerator CRotClockwise() {
        for (float i = 0; i < 90; i += 1f) {
            transform.Rotate(Vector3.back, 1f);
            yield return null;
        }
    }

    IEnumerator CRotCounterClockwise() {
        for (float i = 0; i < 90; i += 1f) {
            transform.Rotate(Vector3.forward, 1f);
            yield return null;
        }
    }
}
