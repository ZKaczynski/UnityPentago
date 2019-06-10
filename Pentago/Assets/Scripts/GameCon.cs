using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCon : MonoBehaviour{

    private View view;
    private Game model;
    private InputBlock block;

    

    void Start() {
        view = GetComponentInChildren<View>();
        model = new Game();
        block = GetComponent<InputBlock>();

        
    }

    void Update(){

        if (block.ShouldRefresh()) {
            view.ResetTransform();
            block.AlreadyRefreshed();
            view.Display(model.MainBoard.GetInfo());
            model.MainBoard.Print();
           
        }


        if (Input.GetMouseButtonDown(0)&&!block.IsBlocked()) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null) {
                int x = hit.collider.gameObject.GetComponent<Tile>().id;

                model.MainBoard.MarkInt(x);
                view.Display(model.MainBoard.GetInfo());

            }
        }
        


        if (Input.GetKeyDown(KeyCode.A)) {
            view.Display(model.MainBoard.GetInfo());
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
           
            model.MakeAiMove();
            view.Display(model.MainBoard.GetInfo());
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            model.MainBoard.Print();
        }

        if (Input.GetKeyDown(KeyCode.M)) {

            model.PrintMoves();
        }
    }

    public void NWClocwiseRot() {
        if (!block.IsBlocked()) {
            model.MainBoard.Rotate(0, true);
            view.qrts[0].RotateClockwise();
        }
    }

    public void NWCounterClocwiseRot() {
        if (!block.IsBlocked()) {
            model.MainBoard.Rotate(0, false);
            view.qrts[0].RotateCounterClockwise();
        }
    }

    public void NEClocwiseRot() {
        if (!block.IsBlocked()) {
            model.MainBoard.Rotate(1, true);
            view.qrts[1].RotateClockwise();
        }
    }

    public void NECounterClocwiseRot() {
        if (!block.IsBlocked()) {
            model.MainBoard.Rotate(1, false);
            view.qrts[1].RotateCounterClockwise();
        }
    }

    public void SWClocwiseRot() {
        if (!block.IsBlocked()) {
            model.MainBoard.Rotate(2, true);
            view.qrts[2].RotateClockwise();
        }
    }

    public void SWCounterClocwiseRot() {
        if (!block.IsBlocked()) {
            model.MainBoard.Rotate(2, false);
            view.qrts[2].RotateCounterClockwise();
        }
    }

    public void SEClocwiseRot() {
        if (!block.IsBlocked()) {
            model.MainBoard.Rotate(3, true);
            view.qrts[3].RotateClockwise();
        }
    }

    public void SECounterClocwiseRot() {
        if (!block.IsBlocked()) {
            model.MainBoard.Rotate(3, false);
            view.qrts[3].RotateCounterClockwise();
        }
    }
}
