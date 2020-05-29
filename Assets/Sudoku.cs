using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Sudoku : MonoBehaviour
{
    int[] givenNo = new int[81];
    int[] givenNo1 = new int[] { 8, 0, 7, 1, 5, 0, 0, 9, 6, 0, 6, 5, 3, 0, 7, 1, 4, 0, 3, 4, 1, 0, 8, 0, 7, 0, 2, 5, 9, 3, 4, 6, 8, 2, 7, 0, 4, 0, 0, 0, 1, 0, 0, 0, 9, 0, 1, 8, 9, 7, 2, 4, 3, 5, 7, 0, 6, 0, 3, 0, 9, 1, 4, 0, 5, 4, 7, 0, 6, 8, 2, 0, 2, 3, 0, 0, 4, 1, 5, 0, 7 };
    int[] givenNo2 = new int[] { 0, 0, 4, 6, 0, 8, 9, 1, 2, 0, 7, 2, 0, 0, 0, 3, 4, 8, 1, 0, 0, 3, 4, 2, 5, 0, 7, 0, 5, 9, 7, 0, 1, 4, 2, 0, 0, 2, 6, 0, 5, 0, 7, 9, 0, 0, 1, 3, 9, 0, 4, 8, 5, 0, 9, 0, 1, 5, 3, 7, 0, 0, 4, 2, 8, 7, 0, 0, 0, 6, 3, 0, 3, 4, 5, 2, 0, 6, 1, 0, 0 };
    int[] givenNo3 = new int[] { 0, 6, 0, 3, 5, 4, 0, 9, 0, 9, 0, 5, 8, 0, 0, 6, 0, 3, 0, 4, 0, 0, 0, 0, 0, 8, 0, 6, 0, 0, 4, 0, 0, 0, 1, 0, 8, 7, 2, 0, 9, 0, 0, 6, 0, 0, 0, 0, 0, 3, 6, 8, 0, 9, 3, 0, 1, 7, 0, 2, 0, 0, 0, 0, 0, 6, 0, 0, 0, 9, 3, 0, 4, 0, 0, 9, 0, 3, 1, 0, 6 };
    int[] givenNo4 = new int[] { 0, 0, 0, 0, 0, 9, 8, 0, 0, 0, 1, 8, 4, 0, 0, 0, 2, 0, 0, 0, 4, 0, 7, 0, 0,0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 1, 8, 0, 7, 0, 2, 0, 5, 1, 8, 0, 0, 0, 9, 3, 9, 7, 0, 0, 3, 0, 0, 0, 4, 0, 3, 0, 0, 6, 0, 0, 0, 0, };

    public int[,] sudokuNo = new int[9, 9];
    public int[,] solvedSudoku = new int[9, 9];
    public int[,] sudokuCopy = new int[9, 9];
    public int[] realElements = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    int length = 9;

    public Image[] sudokuBox;
    void Start()
    {
        Set0TOGivenNo();
        
        //InitializeSudok1();
        //InitializeSudoku2();
        //InitializeSudoku3();
        InitializeSudoku3();
        PrintSudoku();
    }

    public void ChangeNumberButtonPressed(int a)
    {
        string num = sudokuBox[a].GetComponentInChildren<Text>().text;
        int nnn = Convert.ToInt32(num);
        nnn++;
        if (nnn > 9)
            nnn = 0;
        givenNo[a] = nnn;
        sudokuBox[a].GetComponentInChildren<Text>().text = nnn.ToString();
    }

    public void SolveSudokuButtonPressed()
    {
        InitializeSudoku3();
        StartCoroutine(SolveSudokuBruteForce());
    }

    IEnumerator SolveSudoku()
    {
        int flagCol, flagRow, x, y, SudokuCounter = 0;
        int u = 1, g = 0, h = 0;
        int currentValue = 0;
        
        while (u == 1)
        {
            SudokuCounter = 0;
            for (int i = g; i < length; i++)
            {
                for (int j = h; j < length; j++)
                {
                    for (int k = currentValue; k < length; k++)
                    {
                        if (sudokuNo[i, j] == 99)
                        {
                            flagCol = 999; flagRow = 999;

                            for (int p = 0; p < length; p++) //check col
                            {
                                if (sudokuNo[i, p] == realElements[k])
                                {
                                    flagRow = i;
                                    flagCol = p;
                                    break;
                                }
                            }
                            for (int q = 0; q < length; q++) //check row
                            {
                                if (sudokuNo[q, j] == realElements[k])
                                {
                                    flagRow = q;
                                    flagCol = j;
                                    break;
                                }
                            }

                            //for each block
                            int oo = i / 3; int pp = j / 3;

                            if (oo == 0) { oo = 0; } else if (oo == 1) { oo = 3; } else { oo = 6; }
                            if (pp == 0) { pp = 0; } else if (pp == 1) { pp = 3; } else { pp = 6; }

                            int tempLengthoo = oo + 3;
                            int tempLengthpp = pp + 3;
                            for (int a = oo; a < tempLengthoo; a++)
                            {
                                for (int b = pp; b < tempLengthpp; b++)
                                {
                                    if (sudokuNo[a, b] == realElements[k])
                                    {
                                        flagRow = realElements[k];

                                    }
                                }
                            }


                            if (flagRow == 999 && flagCol == 999)
                            {
                                if (solvedSudoku[i, j] == 0)
                                {
                                    sudokuNo[i, j] = realElements[k];
                                    solvedSudoku[i, j] = realElements[k];

                                    //g = i; h = j;
                                    PrintSudoku();
                                }
                                //yield return new WaitForSeconds(0.005f);
                            }
                            //yield return new WaitForSeconds(0.05f);

                        }

                    }//end of k
                    if(g==i&&h==j)
                        currentValue = 0;
                }//end of j
                h = 0;
            }
            PrintSudoku();

            
            //check number of empty spaces in main array
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (sudokuNo[i, j] == 99)
                    {
                        SudokuCounter++;
                    }
                }
            }
           
            if (SudokuCounter == 0)
            {
                //means sudoku is solved
                u = 0;
                //leave the loop
            }
            else //if(SudokuCounter==1)
            {
                //increment the last index of sudoku
                bool flag = false;
                g = 0; h = 0;
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        if (sudokuNo[i, j] != 99 && flag == false && solvedSudoku[i, j] != 0)
                        {
                            g = i; h = j;
                            
                        }
                        else if (sudokuNo[i, j] == 99)
                        {
                            flag = true;
                        }
                        
                    }
                }
                print(SudokuCounter + "|" + "g=" + g + "h=" + h);
                

                currentValue = sudokuNo[g, h];
                if (currentValue >= 9)
                {
                    solvedSudoku[g, h] = 0;
                    for (int i = 0; i < length; i++)
                    {
                        for (int j = 0; j < length; j++)
                        {
                            if (solvedSudoku[i, j] != 0)
                            {
                                g=i;h=j;
                            }
                        }
                    }

                    currentValue = sudokuNo[g, h];
                }
                
                solvedSudoku[g, h] = 0;
                sudokuNo[g, h] = 99;
                /*
                if (sudokuNo[0, 0] != 99 && solvedSudoku[0, 0]!=0&&g==0&&h==0)
                {
                    currentValue = sudokuNo[0, 0];
                    solvedSudoku[0, 0] = 0;
                    sudokuNo[0, 0] = 99;
                }
                else if (sudokuNo[0, 0] != 99)
                {
                    currentValue = 0;
                }
                */
                for (int i = g; i < length; i++)
                {
                    for (int j = h; j < length; j++)
                    {
                        if (solvedSudoku[i, j] != 0)
                        {
                            sudokuNo[i, j] = 99;
                            solvedSudoku[i, j] = 0;
                        }
                    }
                }

                
            }

            yield return new WaitForSeconds(0.00001f);

        }//end of while loop
        
    }


    IEnumerator SolveSudokuBruteForce()
    {
        int flagCol, flagRow, x, y, SudokuCounter = 0;
        int u = 1, g = 0, h = 0;
        int currentValue = 0;

        while (u == 1)
        {
            SudokuCounter = 0;
            for (int i = g; i < length; i++)
            {
                for (int j = h; j < length; j++)
                {
                    for (int k = currentValue; k < length; k++)
                    {
                        if (sudokuNo[i, j] == 99)
                        {
                            flagCol = 999; flagRow = 999;

                            for (int p = 0; p < length; p++) //check col
                            {
                                if (sudokuNo[i, p] == realElements[k])
                                {
                                    flagRow = i;
                                    flagCol = p;
                                    break;
                                }
                            }
                            for (int q = 0; q < length; q++) //check row
                            {
                                if (sudokuNo[q, j] == realElements[k])
                                {
                                    flagRow = q;
                                    flagCol = j;
                                    break;
                                }
                            }

                            //for each block
                            int oo = i / 3; int pp = j / 3;

                            if (oo == 0) { oo = 0; } else if (oo == 1) { oo = 3; } else { oo = 6; }
                            if (pp == 0) { pp = 0; } else if (pp == 1) { pp = 3; } else { pp = 6; }

                            int tempLengthoo = oo + 3;
                            int tempLengthpp = pp + 3;
                            for (int a = oo; a < tempLengthoo; a++)
                            {
                                for (int b = pp; b < tempLengthpp; b++)
                                {
                                    if (sudokuNo[a, b] == realElements[k])
                                    {
                                        flagRow = realElements[k];

                                    }
                                }
                            }


                            if (flagRow == 999 && flagCol == 999)
                            {
                                if (solvedSudoku[i, j] == 0)
                                {
                                    sudokuNo[i, j] = realElements[k];
                                    solvedSudoku[i, j] = realElements[k];

                                    g = i; h = j;
                                    PrintSudoku();
                                }
                                //yield return new WaitForSeconds(0.005f);
                            }
                            //yield return new WaitForSeconds(0.05f);

                        }

                    }//end of k
                    currentValue = 0;
                }//end of j
                h = 0;
            }
            PrintSudoku();


            //check number of empty spaces in main array
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (sudokuNo[i, j] == 99)
                    {
                        SudokuCounter++;
                    }
                }
            }
            print(SudokuCounter + "|" + "g=" + g + "h=" + h);
            if (SudokuCounter == 0)
            {
                //means sudoku is solved
                u = 0;
                //leave the loop
            }
            else
            {
                //increment the last index of sudoku
                currentValue = sudokuNo[g, h];
                if (currentValue >= 9)
                {
                    solvedSudoku[g, h] = 0;
                    for (int i = 0; i < length; i++)
                    {
                        for (int j = 0; j < length; j++)
                        {
                            if (solvedSudoku[i, j] != 0)
                            {
                                g = i; h = j;
                            }
                        }
                    }

                    currentValue = sudokuNo[g, h];
                }

                solvedSudoku[g, h] = 0;
                sudokuNo[g, h] = 99;

            }

            

        }//end of while loop
        yield return new WaitForSeconds(0.000001f);
    }

    void InitializeSudoku()
    {
        int d = 0;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                
                solvedSudoku[i, j] = 0;
                if (givenNo[d] == 0)
                    sudokuNo[i, j] = 99;
                else
                    sudokuNo[i, j] = givenNo[d];
                d++;
            }
        }
    }

    void InitializeSudoku1()
    {
        int d=0;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                sudokuCopy[i, j] = 0;
                solvedSudoku[i, j] = 0;
                if (givenNo1[d] == 0)
                    sudokuNo[i, j] = 99;
                else
                    sudokuNo[i, j] = givenNo1[d];
                d++;
            }
        }
    }

    void InitializeSudoku2()
    {
        int d = 0;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                sudokuCopy[i, j] = 0;
                solvedSudoku[i, j] = 0;
                if (givenNo2[d] == 0)
                    sudokuNo[i, j] = 99;
                else
                    sudokuNo[i, j] = givenNo2[d];
                d++;
            }
        }
    }

    void InitializeSudoku3()
    {
        int d = 0;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                sudokuCopy[i, j] = 0;
                solvedSudoku[i, j] = 0;
                if (givenNo3[d] == 0)
                    sudokuNo[i, j] = 99;
                else
                    sudokuNo[i, j] = givenNo3[d];
                d++;
            }
        }
    }

    void InitializeSudoku4()
    {
        int d = 0;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                sudokuCopy[i, j] = 0;
                solvedSudoku[i, j] = 0;
                if (givenNo4[d] == 0)
                    sudokuNo[i, j] = 99;
                else
                    sudokuNo[i, j] = givenNo4[d];
                d++;
            }
        }
    }

    void Set0TOGivenNo()
    {
        for (int i = 0; i < 81; i++)
        {
            givenNo[i] = 0;
        }
    }

    void PrintSudoku()
    {
        int d=0;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                if (sudokuNo[i, j] != 99)
                {
                    sudokuBox[d].GetComponentInChildren<Text>().text = sudokuNo[i, j].ToString();
                    sudokuBox[d].GetComponentInChildren<Text>().color = Color.blue;
                }
                else
                {
                    sudokuBox[d].GetComponentInChildren<Text>().text = " ";
                }

                if (solvedSudoku[i, j] != 0)
                {
                    sudokuBox[d].GetComponentInChildren<Text>().text = solvedSudoku[i, j].ToString();
                    sudokuBox[d].GetComponentInChildren<Text>().color = Color.black;
                }
                d++;
            }
        }
    }

   
}
/*
                            //for diagonals
                            x = i;
                            y = j;
                            kkkk = 0;
                            while ((x >= 0 && x < length) && (y >= 0 && y < length) && kkkk < 3)//for left upward
                            {
                                if (sudokuNo[x, y] == realElements[k])
                                {
                                    flagRow = x;
                                    flagCol = y;
                                    break;
                                }
                                kkkk++;
                                x--; y--;

                            }

                            x = i;
                            y = j;
                            kkkk = 0;

                            while ((x >= 0 && x < length) && (y >= 0 && y < length) && kkkk < 3)//for right upward
                            {
                                if (sudokuNo[x, y] == realElements[k])
                                {
                                    flagRow = x;
                                    flagCol = y;
                                    break;
                                }
                                x--; y++;
                                kkkk++;
                            }

                            x = i;
                            y = j;
                            kkkk = 0;

                            while ((x >= 0 && x < length) && (y >= 0 && y < length) && kkkk < 3)//for left downwords
                            {
                                if (sudokuNo[x, y] == realElements[k])
                                {
                                    flagRow = x;
                                    flagCol = y;
                                    break;
                                }
                                x++; y--;
                                kkkk++;
                            }

                            x = i;
                            y = j;
                            kkkk = 0;
                            while ((x >= 0 && x < length) && (y >= 0 && y < length) && kkkk < 3)//for right downwords
                            {
                                if (sudokuNo[x, y] == realElements[k])
                                {
                                    flagRow = x;
                                    flagCol = y;
                                    break;
                                }
                                x++; y++;
                                kkkk++;
                            }
                            */





/*

IEnumerator SolveSudoku()
    {
        int flagCol, flagRow, x, y, SudokuCounter = 0;
        int u = 1, g = 0, h = 0;
        int currentValue = 0;
        
        while (u == 1)
        {
            SudokuCounter = 0;
            for (int i = g; i < length; i++)
            {
                for (int j = h; j < length; j++)
                {
                    for (int k = currentValue; k < length; k++)
                    {
                        if (sudokuNo[i, j] == 99)
                        {
                            flagCol = 999; flagRow = 999;

                            for (int p = 0; p < length; p++) //check col
                            {
                                if (sudokuNo[i, p] == realElements[k])
                                {
                                    flagRow = i;
                                    flagCol = p;
                                    break;
                                }
                            }
                            for (int q = 0; q < length; q++) //check row
                            {
                                if (sudokuNo[q, j] == realElements[k])
                                {
                                    flagRow = q;
                                    flagCol = j;
                                    break;
                                }
                            }

                            //for each block
                            int oo = i / 3; int pp = j / 3;

                            if (oo == 0) { oo = 0; } else if (oo == 1) { oo = 3; } else { oo = 6; }
                            if (pp == 0) { pp = 0; } else if (pp == 1) { pp = 3; } else { pp = 6; }

                            int tempLengthoo = oo + 3;
                            int tempLengthpp = pp + 3;
                            for (int a = oo; a < tempLengthoo; a++)
                            {
                                for (int b = pp; b < tempLengthpp; b++)
                                {
                                    if (sudokuNo[a, b] == realElements[k])
                                    {
                                        flagRow = realElements[k];

                                    }
                                }
                            }


                            if (flagRow == 999 && flagCol == 999)
                            {
                                if (solvedSudoku[i, j] == 0)
                                {
                                    sudokuNo[i, j] = realElements[k];
                                    solvedSudoku[i, j] = realElements[k];

                                    g = i; h = j;
                                    PrintSudoku();
                                }
                                //yield return new WaitForSeconds(0.005f);
                            }
                            //yield return new WaitForSeconds(0.05f);

                        }

                    }//end of k
                    currentValue = 0;
                }//end of j
                h = 0;
            }
            PrintSudoku();

            
            //check number of empty spaces in main array
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (sudokuNo[i, j] == 99)
                    {
                        SudokuCounter++;
                    }
                }
            }
            print(SudokuCounter+"|"+"g="+g+"h="+h);
            if (SudokuCounter == 0)
            {
                //means sudoku is solved
                u = 0;
                //leave the loop
            }
            else
            {
                //increment the last index of sudoku
                currentValue = sudokuNo[g, h];
                if (currentValue >= 9)
                {
                    solvedSudoku[g, h] = 0;
                    for (int i = 0; i < length; i++)
                    {
                        for (int j = 0; j < length; j++)
                        {
                            if (solvedSudoku[i, j] != 0)
                            {
                                g=i;h=j;
                            }
                        }
                    }

                    currentValue = sudokuNo[g, h];
                }
                
                solvedSudoku[g, h] = 0;
                sudokuNo[g, h] = 99;
                
            }

            yield return new WaitForSeconds(0.000001f);

        }//end of while loop
        
    }

*/