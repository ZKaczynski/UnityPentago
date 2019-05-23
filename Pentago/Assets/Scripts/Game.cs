using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game {

    public Board MainBoard = new Board();


    private Board.Move RootMinimax() {
        Board.Move best = new Board.Move(0L, 0, true);
        int bestscore = -9999;
        List<Board.Move> moves = MainBoard.Getmoves();
        for (int i = 0; i < moves.Count; i++) {
            int score;
            MainBoard.MakeMove(moves[i]);
            score = AlfaBetaMinimax(MainBoard, -99999, 99999, 3, 0, false, !MainBoard.CurrentPlayer());
            MainBoard.UnDoMove(moves[i]);
            if (score > bestscore) {
                best = moves[i];
                bestscore = score;
            }
        }
        return best;
    }


    private int AlfaBetaMinimax(Board board, int alfa, int beta, int maxDepth, int currentDepth, bool maxing, bool player) {
        if (currentDepth == maxDepth || board.HasEnded()) {
            return board.Evaluate(player);
        }
        int score;
        if (maxing) {
            int a = alfa;
            List<Board.Move> moves = MainBoard.Getmoves();
            for (int i = 0; i < moves.Count; i++) {
                board.MakeMove(moves[i]);
                score = AlfaBetaMinimax(board, a, beta, maxDepth, currentDepth + 1, !maxing, player);
                board.UnDoMove(moves[i]);
                if (score > a) a = score;
                if (a >= beta) {
                    return a;
                }
            }
            return a;
        } else {
            int b = beta;
            List<Board.Move> moves = MainBoard.Getmoves();
            for (int i = 0; i < moves.Count; i++) {
                board.MakeMove(moves[i]);
                score = AlfaBetaMinimax(board, alfa, b, maxDepth, currentDepth + 1, !maxing, player);
                board.UnDoMove(moves[i]);
                if (score < b) b = score;
                if (alfa >= b) {
                    return b;
                }
            }
            return b;
        }
    }

    public void MakeAiMove() {
        Board.Move m = RootMinimax();
        MainBoard.MakeMove(m);
        Debug.Log(m.move + " " + m.quarter + " " + m.clockwise);
        MainBoard.Print();
    }

    public void PrintMoves() {
        foreach (Board.Move move in MainBoard.Getmoves()) {
            Debug.Log(move.move + "|" + move.quarter + "|" + move.clockwise);
        }
    }
}
    /*
        if (Input.GetKeyDown(KeyCode.Space)) {
            
            Board.Move m = RootMinimax();
            MainBoard.MakeMove(m);
            //Debug.Log(m.Tile + " " + m.Quarter + " " + m.Clockwise);
            MainBoard.Print();
           
            MainBoard.MarkInt(21);
            int score = AlfaBetaMinimax(MainBoard, -9999, 9999, 1, 0, false);
            Debug.Log(score);
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            MainBoard.Print();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            MainBoard.Rotate(0, true);
            MainBoard.Print();
        }*/
    

    
        /*
       MainBoard.MarkInt(10);
       MainBoard.MarkInt(11);
       MainBoard.MarkInt(12);
         MainBoard.MarkInt(3);

       MainBoard.MarkInt(14);
       MainBoard.MarkInt(15);
       MainBoard.Print();*/
        /*
        }*/



    

//}
