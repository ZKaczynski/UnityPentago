using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    Board MainBoard = new Board();

    
   private Board.Move RootMinimax() {
        Board.Move best=new Board.Move(0L,0,true);
        int bestscore= -9999;
        List<Board.Move> moves = MainBoard.Getmoves();
        for (int i=0; i<moves.Count;i++) {
            int score;
            MainBoard.MakeMove(moves[i]);
            score = AlfaBetaMinimax(MainBoard, -9999, 9999, 2, 0, false, MainBoard.CurrentPlayer());
            MainBoard.UnDoMove(moves[i]);
            if (score > bestscore) {
                best = moves[i];
                bestscore = score;
            }
        }
        return best;
   }
    

    private int AlfaBetaMinimax(Board board, int alfa, int beta, int maxDepth, int currentDepth, bool maxing, bool player) {
        if (currentDepth == maxDepth||board.HasEnded()) {
            return board.Evaluate(player);
        }
        int score;
        if (maxing) {
            int a = alfa;
            List<Board.Move> moves = MainBoard.Getmoves();
            for (int i = 0; i < moves.Count; i++) {
                board.MakeMove(moves[i]);
                score = AlfaBetaMinimax(board, a, beta, maxDepth, currentDepth+1, !maxing, MainBoard.CurrentPlayer()); 
                board.UnDoMove(moves[i]);
                if (score > a) a = score;
                if (a >= beta) return a;
            }
            return a;
        } else {
            int b = beta;
            List<Board.Move> moves = MainBoard.Getmoves();
            for (int i = 0; i < moves.Count; i++) {
                board.MakeMove(moves[i]);
                score = AlfaBetaMinimax(board, alfa, b, maxDepth, currentDepth+1, !maxing, MainBoard.CurrentPlayer());
                board.UnDoMove(moves[i]);
                if (score < b) b = score;
                if (alfa >= b) return b;
            }
            return b;
        }
    }     





    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            
            Board.Move m = RootMinimax();
            MainBoard.MakeMove(m);
            //Debug.Log(m.Tile + " " + m.Quarter + " " + m.Clockwise);
            MainBoard.Print();
            /*
            MainBoard.MarkInt(21);
            int score = AlfaBetaMinimax(MainBoard, -9999, 9999, 1, 0, false);
            Debug.Log(score);*/
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            MainBoard.Print();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            MainBoard.Rotate(0, true);
            MainBoard.Print();
        }
    }

    void Awake() {
        
        MainBoard.MarkInt(30);
        MainBoard.MarkInt(31);
        /*MainBoard.MarkInt(32);
        MainBoard.MarkInt(33);
        MainBoard.MarkInt(34);
        MainBoard.MarkInt(35);*/
        MainBoard.Print();
        /*fo00reach (Board.Move move in MainBoard.Getmoves()) {
            Debug.Log(move.move+"|"+move.quarter+"|"+move.clockwise);
        }*/



    }

}
