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
            int b = model.MainBoard.Evaluate(model.MainBoard.CurrentPlayer());
            Debug.Log(b);
            bool x = model.MainBoard.HasEnded();
            Debug.Log(x);
            bool p = model.MainBoard.CurrentPlayer();
            Debug.Log(p);
            model.PrintMoves();
        }

    }

    public void NWClocwiseRot() {
        if (!block.IsBlocked()) {
            model.MainBoard.Rotate(0, true);
            view.qrts[0].RotateClockwise();
        }

    }
}
