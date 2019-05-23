using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {

    private static readonly long[] til;
    private static readonly long[] win;
    private static readonly long[] qrt;
    private static readonly long qrts;
    private long X;
    private long O;
    private bool Xturn; 


    public struct Move{
        public readonly long move;
        public readonly int quarter;
        public readonly bool clockwise;

        public Move(long m, int q, bool c) {
            move = m;
            quarter = q;
            clockwise = c;
        }
    }


    static Board() {
        til = new long[36];
        win = new long[32];
        qrt = new long[4];
        for (int i = 0; i < 36; i++) {
            til[i] = 1L << i;
        }
       
        //Horizontal  
        for (int i = 0; i < 6; i++) {
            win[i] = til[6*i] | til[6*i + 1] | til[6*i + 2] | til[6*i + 3] | til[6*i + 4];
        }
        for (int i = 0; i < 6; i++) {
            win[6 + i] = til[6*i + 1] | til[6*i + 2] | til[6*i + 3] | til[6*i + 4] | til[6*i + 5];
        }
        //Vertical
        for (int i = 0; i < 6; i++) {
            win[12 + i] = til[i] | til[i + 6] | til[i + 12] | til[i + 18] | til[i + 24];
        }
        for (int i = 0; i < 6; i++) {
            win[18 + i] = til[i + 6] | til[i + 12] | til[i + 18] | til[i + 24] | til[i + 30];
        }
        win[24] = til[0] | til[7] | til[14] | til[21] | til[28];
        win[25] = til[7] | til[14] | til[21] | til[28] | til[35];
        win[26] = til[5] | til[10] | til[15] | til[20] | til[25];
        win[27] = til[10] | til[15] | til[20] | til[25] | til[30];
        win[28] = til[1] | til[8] | til[15] | til[22] | til[29];
        win[29] = til[6] | til[13] | til[20] | til[27] | til[34];
        win[30] = til[4] | til[9] | til[14] | til[19] | til[24];
        win[31] = til[11] | til[16] | til[21] | til[26] | til[31];

        qrt[0] = til[0] | til[1] | til[2] | til[6] | til[8] | til[12] | til[13] | til[14];
        qrt[1] = til[3] | til[4] | til[5] | til[9] | til[11] | til[15] | til[16] | til[17];
        qrt[2] = til[18] | til[19] | til[20] | til[24] | til[26] | til[30] | til[31] | til[32];
        qrt[3] = til[21] | til[22] | til[23] | til[27] | til[29] | til[33] | til[34] | til[35];
        qrts = qrt[0] | qrt[1] | qrt[2] | qrt[3];
    }

    public Board() { 
        X = 0L;
        O = 0L;
        Xturn = true;
    }

    public bool HasEnded() {
        for (int i = 0; i < 32; i++) {
            if ((X & win[i]) == win[i]) return true;
            if ((O & win[i]) == win[i]) return true;
        }
        if ((X | O) == ~0L) return true;
        return false;
    }

    public void Rotate(int quarter, bool clockwise) {
        long[] shift = new long[16];
        int delta=0;
        if (quarter == 0) delta = 0;
        else if (quarter == 1) delta = 3;
        else if (quarter == 2) delta = 18;
        else if (quarter == 3) delta = 21;


        shift[0] = X & til[0+delta];
        shift[1] = X & til[1 + delta];
        shift[2] = X & til[2 + delta];
        shift[3] = X & til[8 + delta];
        shift[4] = X & til[14 + delta];
        shift[5] = X & til[13 + delta];
        shift[6] = X & til[12 + delta];
        shift[7] = X & til[6 + delta];
        shift[8] = O & til[0 + delta];
        shift[9] = O & til[1 + delta];
        shift[10] = O & til[2 + delta];
        shift[11] = O & til[8 + delta];
        shift[12] = O & til[14 + delta];
        shift[13] = O & til[13 + delta];
        shift[14] = O & til[12 + delta];
        shift[15] = O & til[6 + delta];
        X = X & (~qrt[quarter]);
        O = O & (~qrt[quarter]);
        if (clockwise) {
            if (shift[0] != 0) X = X | til[2 + delta];
            if (shift[1] != 0) X = X | til[8 + delta];
            if (shift[2] != 0) X = X | til[14 + delta];
            if (shift[3] != 0) X = X | til[13 + delta];
            if (shift[4] != 0) X = X | til[12 + delta];
            if (shift[5] != 0) X = X | til[6 + delta];
            if (shift[6] != 0) X = X | til[0 + delta];
            if (shift[7] != 0) X = X | til[1 + delta];
            if (shift[8] != 0) O = O | til[2 + delta];
            if (shift[9] != 0) O = O | til[8 + delta];
            if (shift[10] != 0) O = O | til[14 + delta];
            if (shift[11] != 0) O = O | til[13 + delta];
            if (shift[12] != 0) O = O | til[12 + delta];
            if (shift[13] != 0) O = O | til[6 + delta];
            if (shift[14] != 0) O = O | til[0 + delta];
            if (shift[15] != 0) O = O | til[1 + delta];
        } else {
            if (shift[0] != 0) X = X | til[12 + delta];
            if (shift[1] != 0) X = X | til[6 + delta];
            if (shift[2] != 0) X = X | til[0 + delta];
            if (shift[3] != 0) X = X | til[1 + delta];
            if (shift[4] != 0) X = X | til[2 + delta];
            if (shift[5] != 0) X = X | til[8 + delta];
            if (shift[6] != 0) X = X | til[14 + delta];
            if (shift[7] != 0) X = X | til[13 + delta];
            if (shift[8] != 0) O = O | til[12 + delta];
            if (shift[9] != 0) O = O | til[6 + delta];
            if (shift[10] != 0) O = O | til[0 + delta];
            if (shift[11] != 0) O = O | til[1 + delta];
            if (shift[12] != 0) O = O | til[2 + delta];
            if (shift[13] != 0) O = O | til[8 + delta];
            if (shift[14] != 0) O = O | til[14 + delta];
            if (shift[15] != 0) O = O | til[13 + delta];
        }
        
    }



    public void MakeMove(Move m) {
        Mark(m.move);
        Rotate(m.quarter, m.clockwise);
        Xturn = !Xturn;
    }




    public void UnDoMove(Move m) {
        Xturn = !Xturn;
        Rotate(m.quarter, !m.clockwise);
        Clear(m.move);
    }

    public int Evaluate(bool player) {
        int xscore = 0;
        int oscore = 0;
        for (int i = 0; i < 32; i++) {
            long Xline = X & win[i];
            if (Xline != 0) {
                int points =NumberOfSetBits(Xline);
                if (points == 5) {
                    if (!player) return -9999;        //Posible error around there
                    else return 9999;
                } else if (points > 1) {
                    xscore += points * points;
                }
            }
        }
        
        for (int i = 0; i < 32; i++) {
            long Oline = O & win[i];
            if (Oline != 0) {
                int points = NumberOfSetBits(Oline);
                if (points == 5) {
                    if (!player) return -9999;
                    else return 9999;
                } else if (points > 1) {
                    oscore += points * points;
                }
            }
        }
        
        if (!player) return oscore - xscore;
        else return xscore - oscore;
    }

    public void MarkInt(int t) {
        Mark(til[t]);
    }

    private void Mark(long tile) {
        if (Xturn) {
            X = (long)((long)X | (long)tile);
        } else {
            O = O | tile;
        }
    }


    private void Clear(long tile) {
        if (Xturn) {
            X = X & (~tile);
        } else {
            O = O & (~tile);
        }
    }

    public void Print() {
        string s= "X score:" + Evaluate(true) + "\n"+ "O score:" + Evaluate(false) + "\n";
        for (int i = 0; i < 36; i++) {
            if   ((O & til[i]) == til[i]) s = s + "O";
            else if ((X & til[i]) == til[i]) s = s + "X";
            else s = s + "-";
            if ((i+1)%6==0) s = s + "\n";
        }
        Debug.Log(s);
    }

    private int NumberOfSetBits(long i) {
        int count = 0;
        while (i!=0L) {
            count += (int)(i & 1L);
            i >>= 1;
        }
        return count;
    }

    public bool CurrentPlayer() {
        return Xturn;
    }

    public long MoveToState(Move m) {
        long sum = X | O | m.move;

        long[] shift = new long[8];
        int delta = 0;
        if (m.quarter == 0) delta = 0;
        else if (m.quarter == 1) delta = 3;
        else if (m.quarter == 2) delta = 18;
        else if (m.quarter == 3) delta = 21;


        shift[0] = sum & til[0 + delta];
        shift[1] = sum & til[1 + delta];
        shift[2] = sum & til[2 + delta];
        shift[3] = sum & til[8 + delta];
        shift[4] = sum & til[14 + delta];
        shift[5] = sum & til[13 + delta];
        shift[6] = sum & til[12 + delta];
        shift[7] = sum & til[6 + delta];

        sum = sum & (~qrt[m.quarter]);
        
        if (m.clockwise) {
            if (shift[0] != 0) sum |= til[2 + delta];
            if (shift[1] != 0) sum |= til[8 + delta];
            if (shift[2] != 0) sum |= til[14 + delta];
            if (shift[3] != 0) sum |= til[13 + delta];
            if (shift[4] != 0) sum |= til[12 + delta];
            if (shift[5] != 0) sum |= til[6 + delta];
            if (shift[6] != 0) sum |= til[0 + delta];
            if (shift[7] != 0) sum |= til[1 + delta];
           
        } else {
            if (shift[0] != 0) sum |= til[12 + delta];
            if (shift[1] != 0) sum |= til[6 + delta];
            if (shift[2] != 0) sum |= til[0 + delta];
            if (shift[3] != 0) sum |= til[1 + delta];
            if (shift[4] != 0) sum |= til[2 + delta];
            if (shift[5] != 0) sum |= til[8 + delta];
            if (shift[6] != 0) sum |= til[14 + delta];
            if (shift[7] != 0) sum |= til[13 + delta];           
        }

        return sum;
    }

    public List<Move> Getmoves() {
        List<Move> moveList = new List<Move>();
        List<long> gameStates = new List<long>();
        long emptytiles = ~(X | O);     
        for (int i = 0; i < 36; i++) { 
            if ((emptytiles & til[i]) != 0L) {
                for (int k = 0; k < 4; k++) {
                    Move m = new Move(til[i], k, true);
                    long state = MoveToState(m);
                    if (gameStates.IndexOf(state) < 0) {
                        gameStates.Add(state);
                        moveList.Add(m);
                    }
                }
                for (int k = 0; k < 4; k++) {
                    Move m = new Move(til[i], k, false);
                    long state = MoveToState(m);
                    if (gameStates.IndexOf(state) < 0) {
                        gameStates.Add(state);
                        moveList.Add(m);
                    }
                }
            } 
        }
        return moveList;
    }




    /*
    public List<Move> Getmoves() {
        List <Move> moveList = new List<Move>();
        List<long> gameStates = new List<long>();
        long emptytiles = ~(X | O);     //all tiles that are empty are 1, 0 if occupied
        for (int i = 0; i < 36; i++) {  // for each tile on board
            if ((emptytiles & til[i]) != 0L) {  // if tile is not occupied
                long sum = X | O | til[i];  // all occpied tiles (with tile we marked) are 1, 0 if empty
                if ((sum & qrts) == 0L) {     // if only quarter centers are occupied
                    Move m = new Move(til[i], 0, true);
                    long state = MoveToState(m);
                    if (gameStates.IndexOf(state) < 0) {
                        gameStates.Add(state);
                        moveList.Add(m);  // add this move and just one rotation  
                    }
                } else {                                            //otherwise 
                    if ((sum & qrt[0]) != 0L) {                     // if at least one tile in first quarter is occpied 
                        moveList.Add(new Move(til[i], 0, true));
                        moveList.Add(new Move(til[i], 0, false));
                    }else {
                        moveList.Add(new Move(til[i], 0, true));
                    }
                    if ((sum & qrt[1]) != 0L) {
                        moveList.Add(new Move(til[i], 1, true));
                        moveList.Add(new Move(til[i], 1, false));
                    } else {
                        moveList.Add(new Move(til[i], 1, true));
                    }
                    if ((sum & qrt[2]) != 0L) {
                        moveList.Add(new Move(til[i], 2, true));
                        moveList.Add(new Move(til[i], 2, false));
                    } else {
                        moveList.Add(new Move(til[i], 2, true));
                    }
                    if ((sum & qrt[3]) != 0L) {
                        moveList.Add(new Move(til[i], 3, true));
                        moveList.Add(new Move(til[i], 3, false));
                    } else {
                        moveList.Add(new Move(til[i], 3, true));
                    }
                }
            } 
        }
        return moveList;
    }*/

    public int[] GetInfo() {
        int[] info = new int[36];
        for (int i = 0; i < 36; i++) {
            if ((O & til[i]) == til[i]) info[i] = 1;
            else if ((X & til[i]) == til[i]) info[i] = 2;
            else info[i] = 0;
        }
        return info;
    }
}
