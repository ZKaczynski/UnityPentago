using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Board{

    private static readonly long[] til; 
    private static readonly long[] win;
    private static readonly long[] qrt;
    private long X;
    private long O;
    public bool Xturn; //should be pruivate

    public bool[] qsym = new bool[4];

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
    }

    public Board() { 
        X = 0L;
        O = 0L;
        Xturn = true;
        qsym[0] = true;
        qsym[1] = true;
        qsym[2] = true;
        qsym[3] = true;
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

    public void MakeMove(int tile, int quarter, bool clockwise) {
        Mark(til[tile]);
        Rotate(quarter, clockwise);
        Xturn = !Xturn;
    }

    public void UnDoMove(int tile, int quarter, bool clockwise) {
        Xturn = !Xturn;
        Rotate(quarter, !clockwise);
        Clear(til[tile]);
    }

    public int Evaluate(bool player) {
        int xscore = 0;
        int oscore = 0;
        for (int i = 0; i < 32; i++) {
            long Xline = X & win[i];
            if (Xline != 0) {
                int points =NumberOfSetBits(Xline);
                if (points == 5) {
                    xscore = 999;
                    break;
                } else if (points > 1) {
                    //Debug.Log("Scored " + points + " on " + i);
                    xscore += points * points;
                }
            }
        }
        
        for (int i = 0; i < 32; i++) {
            long Oline = O & win[i];
            if (Oline != 0) {
                int points = NumberOfSetBits(Oline);
                if (points == 5) {
                    oscore = 999;
                    break;
                } else if (points > 1) {
                    oscore += points * points;
                }
            }
        }
        
        if (!player) return oscore - xscore;
        else return xscore - oscore;
    }

    private void Mark(long tile) {
        if (Xturn) {
            X = (long)((long)X | (long)tile);
        } else {
            O = O | tile;
        }
    }

    public void MarkInt(int t) {
        Mark(til[t]);

        long b = X | O;
        if ((b & qrt[0]) == 0L) qsym[0]=true;
        else qsym[0] = false;
        if ((b & qrt[1]) == 0L) qsym[1]=true;
        else qsym[1] = false;
        if ((b & qrt[2]) == 0L) qsym[2] = true;
        else qsym[2] = false;
        if ((b & qrt[3]) == 0L) qsym[3] = true;
        else qsym[3] = false;
        // Xturn = !Xturn;
    }

    private void Clear(long tile) {
        if (Xturn) {
            X = X & (~tile);
        } else {
            O = O & (~tile);
        }
    }

    public void Print() {

        string s = ""; 
         //   = "X score:" + Evaluate(true) + "\n"+ "O score:" + Evaluate(false) + "\n";
        for (int i = 0; i < 36; i++) {
            if   ((O & til[i]) == til[i]) s = s + "O";
            else if ((X & til[i]) == til[i]) s = s + "X";
            else s = s + "-";
            if ((i+1)%6==0) s = s + "\n";
        }
        Debug.Log(s);
    }

    private int NumberOfSetBits(long i) {
        i = i - ((i >> 1) & 0x5555555555555555);
        i = (i & 0x3333333333333333) + ((i >> 2) & 0x3333333333333333);
        return (int)(((i + (i >> 4)) & 0xF0F0F0F0F0F0F0F) * 0x101010101010101) >> 56;
    }
    public bool CurrentPlayer() {
        return Xturn;
    }

    public List<int> GetMoves() {
        List<int> l = new List<int>();
        long sum = ~(X | O);
        for (int i = 0; i < 36; i++) {
            if ((sum & til[i]) != 0L) l.Add(i);
        }
        return l;
    } 
    public List<int> GetQuarters() {
        List<int> l = new List<int>();
        long b = X | O;
        if ((b & qrt[0]) == 0L) l.Add(0);
        if ((b & qrt[1]) == 0L) l.Add(1);
        if ((b & qrt[2]) == 0L) l.Add(2);
        if ((b & qrt[3]) == 0L) l.Add(3);
        return l;
    }
}
