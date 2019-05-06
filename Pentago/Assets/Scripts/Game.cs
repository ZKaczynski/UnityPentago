using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    Board MainBoard = new Board();

     public struct BestMove{
        public BestMove(int t, int q, bool c) {
            Tile = t;
            Quarter = q;
            Clockwise = c;
        }
        public int Tile { get; }
        public int Quarter { get; }
        public bool Clockwise { get; }
     }
    
   private BestMove RootMinimax() {
        BestMove best=new BestMove(0,0,true);
        int bestscore= -9999;
        List<int> moves = MainBoard.GetMoves();
        for (int i=0; i<moves.Count;i++) {

            for (int j = 0; j < 4; j++) {
                MainBoard.MakeMove(moves[i], j, true);
                int score = AlfaBetaMinimax(MainBoard, -9999, 9999, 3, 0, false, MainBoard.CurrentPlayer());
                MainBoard.UnDoMove(moves[i], j, true);
                if (score > bestscore) {
                    best = new BestMove(moves[i], j, true);
                    bestscore = score;
                }
            }
            for (int j = 0; j < 4; j++) {
                MainBoard.MakeMove(moves[i], j, false);
                int score = AlfaBetaMinimax(MainBoard, -9999, 9999, 3, 0, false, MainBoard.CurrentPlayer());
                MainBoard.UnDoMove(moves[i], j, false);
                if (score > bestscore) {
                    best = new BestMove(moves[i], j, false);
                    bestscore = score;
                }
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
            List<int> moves = MainBoard.GetMoves();
            for (int i = 0; i < moves.Count; i++) {
                for (int j = 0; j < 4; j++) {
                    board.MakeMove(moves[i], j, true);
                    score = AlfaBetaMinimax(board, a, beta, maxDepth, currentDepth+1, !maxing, MainBoard.CurrentPlayer()); 
                    board.UnDoMove(moves[i], j, true);
                    if (score > a) a = score;
                    if (a >= beta) return a;
                }
                for (int j = 0; j < 4; j++) {
                    board.MakeMove(moves[i], j, false);
                    score = AlfaBetaMinimax(board, a, beta, maxDepth, currentDepth+1, !maxing, MainBoard.CurrentPlayer());
                    board.UnDoMove(moves[i], j, false);
                    if (score > a) a = score;
                    if (a >= beta) return a;
                }
            }
            return a;
        } else {
            int b = beta;
            List<int> moves = MainBoard.GetMoves();
            for (int i = 0; i < moves.Count; i++) {
                for (int j = 0; j < 4; j++) {
                    board.MakeMove(moves[i], j, true);
                    score = AlfaBetaMinimax(board, alfa, b, maxDepth, currentDepth+1, !maxing, MainBoard.CurrentPlayer());
                    board.UnDoMove(moves[i], j, true);
                    if (score < b) b = score;
                    if (alfa >= b) return b;
                }
                for (int j = 0; j < 4; j++) {
                    board.MakeMove(moves[i], j, false);
                    score = AlfaBetaMinimax(board, alfa, b, maxDepth, currentDepth+1, !maxing, MainBoard.CurrentPlayer());
                    board.UnDoMove(moves[i], j, false);
                    if (score < b) b = score;
                    if (alfa >= b) return b;
                }
            }
            return b;
        }
    }     


    public void MakeMove(BestMove bm) {
        MainBoard.MakeMove(bm.Tile, bm.Quarter, bm.Clockwise);
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            
           /* BestMove m = RootMinimax();
            MakeMove(m);
            Debug.Log(m.Tile + " " + m.Quarter + " " + m.Clockwise);
            MainBoard.Print();
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
        /*
        MainBoard.MarkInt(7);
        MainBoard.MarkInt(6);
        MainBoard.MarkInt(8);
        MainBoard.MarkInt(9);
        MainBoard.MarkInt(14);*/



    }

}
