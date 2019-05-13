﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCon : MonoBehaviour{

    private View view;
    private Game model;

    void Start() {
        view = GetComponentInChildren<View>();
        model = GetComponentInChildren<Game>();
    }

    void Update(){


        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null) {
                Debug.Log(hit.collider.gameObject.name);
               
            }
        }
        


        if (Input.GetKeyDown(KeyCode.A)) {
            view.Display(model.MainBoard.GetInfo());
        }

    }
}