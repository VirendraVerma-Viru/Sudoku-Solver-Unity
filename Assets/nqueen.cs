using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nqueen : MonoBehaviour
{
    int n = 4;
    int[,] a = new int[999, 998];
    int[,] b = new int[998, 998];

    public GameObject[] ButtonImage;
    public GameObject[] TempButtonImage;
    public GameObject Grid;
    private float speed;

    public Text QueenNumberText;
    public Text AttemptNumber;
    private int attempt;

    void Start()
    {
        n = 8;
        speed = 0.5f;
        ButtonN(4);
        ButtonSpeed(1);

        //
    }

    void NQueen()
    {
        InitializeWithZero();
        QueenNumberText.text = " ";
        AttemptNumber.text = "Attempt : 0";

        StartCoroutine(BacktrackingProg());//my backtracking
        //StartCoroutine(BruteForceProg());
        
        
    }



    IEnumerator BacktrackingProg()
    {
        //yield return new WaitForSeconds(speed);
        int flagCol = 0; int flagRow = 0; int x = 0, y = 0;
        int g = 0; int h = 0;
        int w = 0, e = 0;
        int QueenNumber = 0;
        //main program
        int u = 0;
        attempt = 0;
        while (u==0)
        {
            QueenNumber = 0;
            for (int i = g; i < n; i++)
            {
                for (int j = h; j < n; j++)
                {
                    flagCol = 999; flagRow = 999;

                    for (int p = 0; p < n; p++) //check col
                    {
                        if (a[i, p] == 1)
                        {
                            flagRow = i;
                            flagCol = p;
                            break;
                        }
                    }
                    for (int q = 0; q < n; q++) //check row
                    {
                        if (a[q, j] == 1)
                        {
                            flagRow = q;
                            flagCol = j;
                            break;
                        }
                    }
                    //for diagonals
                    x = i;
                    y = j;

                    while ((x >= 0 && x < n) && (y >= 0 && y < n))//for left upward
                    {
                        if (a[x, y] == 1)
                        {
                            flagRow = x;
                            flagCol = y;
                            break;
                        }

                        x--; y--;

                    }

                    x = i;
                    y = j;

                    while ((x >= 0 && x < n) && (y >= 0 && y < n))//for right upward
                    {
                        if (a[x, y] == 1)
                        {
                            flagRow = x;
                            flagCol = y;
                            break;
                        }
                        x--; y++;

                    }

                    x = i;
                    y = j;

                    while ((x >= 0 && x < n) && (y >= 0 && y < n))//for left downwords
                    {
                        if (a[x, y] == 1)
                        {
                            flagRow = x;
                            flagCol = y;
                            break;
                        }
                        x++; y--;
                    }

                    x = i;
                    y = j;

                    while ((x >= 0 && x < n) && (y >= 0 && y < n))//for right downwords
                    {
                        if (a[x, y] == 1)
                        {
                            flagRow = x;
                            flagCol = y;
                            break;
                        }
                        x++; y++;
                    }





                    if (flagRow == 999 && flagCol == 999)
                    {
                        if (b[i, j] == 0)
                        {
                            a[i, j] = 1;
                        }
                        else
                        {
                            b[i, j] = 0;
                        }
                    }

                }//end of j
                if (i == g)
                    h = 0;
            }



            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (a[i, j] == 1)
                    {
                        QueenNumber++;
                        g = i; h = j;

                    }
                }
            }
            print(QueenNumber);
            printArray();

            yield return new WaitForSeconds(speed);
            if (QueenNumber == n)// means all queen satisfy
            {
                u = 1;
                attempt++;
                QueenNumberText.text = QueenNumber.ToString();
                AttemptNumber.text = "Attempt : " + attempt.ToString();
            }
            else
            {
                a[g, h] = 0;    //means not satisfy clear last one and branch bound  
                b[g, h] = 1;
                attempt++;
                QueenNumberText.text = QueenNumber.ToString();
                AttemptNumber.text = "Attempt : " + attempt.ToString();
            }
            printArray();
            yield return new WaitForSeconds(speed);
            
        }
        printArray1();
    }

    void InitializeWithZero()
    {
        for(int i=0;i<n;i++)
        {
            for(int j=0;j<n;j++)
            {
                a[i, j] = 0;
                b[i, j] = 0;
            }
        }
    }


    void printArray()
    {
        int d=0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                
                if (a[i, j] == 1)
                    ButtonImage[d].GetComponent<Image>().color = Color.green;
                else
                    ButtonImage[d].GetComponent<Image>().color = Color.white;
                //print("i="+i+",j="+j+"|"+a[i, j]);
                d++;
            }
        }
    }

    void printArray1()
    {
        int d = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {

                if (a[i, j] == 1)
                    ButtonImage[d].GetComponent<Image>().color = Color.red;
                else
                    ButtonImage[d].GetComponent<Image>().color = Color.white;
                //print("i="+i+",j="+j+"|"+a[i, j]);
                d++;
            }
        }
    }

    void VisibleOnlynNumber()
    {

        foreach (GameObject g in TempButtonImage)
        {
            g.SetActive(true);

        }
        print(n * n);
        ButtonImage = GameObject.FindGameObjectsWithTag("QueenIcons");
        for (int i = 0; i < ButtonImage.Length; i++)
        {
            
            if (i >= (n * n))
            {
                print(i);
                ButtonImage[i].SetActive(false);
            }
            else
            {
                
            }
            
        }
        ButtonImage = GameObject.FindGameObjectsWithTag("QueenIcons");
    }

    public GameObject[] QueenButtn;
    public GameObject[] SpeedButton;
    private int numv;
    private int speedv;

    void ClearButtonSelect()
    {
        for(int i=0;i<QueenButtn.Length;i++)
        {
            if (i != numv)
            {
                QueenButtn[i].GetComponent<Image>().color = Color.white;
                QueenButtn[i].GetComponentInChildren<Text>().color = Color.black;
            }
        }
        for (int i = 0; i < SpeedButton.Length; i++)
        {
            if (i != speedv)
            {
                SpeedButton[i].GetComponent<Image>().color = Color.white;
                SpeedButton[i].GetComponentInChildren<Text>().color = Color.black;
            }
        }
    }

    public void ButtonN(int z)
    {
        numv = z - 4;
        ClearButtonSelect();
        //ButtonImage = GameObject.FindGameObjectsWithTag("QueenIcons");
        QueenButtn[z - 4].GetComponent<Image>().color = Color.blue;
        QueenButtn[z - 4].GetComponentInChildren<Text>().color = Color.white;
        n = z;
        SetUp();
        InitializeWithZero();
        printArray();
    }
    public void ButtonSpeed(int z)
    {
        speedv = z - 1;
        ClearButtonSelect();
        //ButtonImage = GameObject.FindGameObjectsWithTag("QueenIcons");
        SpeedButton[z - 1].GetComponent<Image>().color = Color.blue;
        SpeedButton[z - 1].GetComponentInChildren<Text>().color = Color.white;
        if (z == 1)
            speed = 0.5f;
        else if (z == 2)
            speed = 0.1f;
        else if (z == 3)
            speed = 0.00000001f;
        
    }

    public void StartButton()
    {
        NQueen();
    }

    void SetUp()
    {
        
        VisibleOnlynNumber();
        print(n);
        Grid.GetComponent<GridLayoutGroup>().constraintCount = n;
    }
    IEnumerator BruteForceProg()
    {
        int flagCol = 0; int flagRow = 0; int x = 0, y = 0;
        int g = 0; int h = 0;
        int w = 0, e = 0;
        int QueenNumber = 0;
        //main program
        int u = 0;
        for (g = 0; g < n; g++)
        {
            for (h = 0; h < n; h++)
            {
                QueenNumber = 0;
                for (int i = g; i < n; i++)
                {
                    for (int j = h; j < n; j++)
                    {
                        flagCol = 999; flagRow = 999;

                        for (int p = 0; p < n; p++) //check col
                        {
                            if (a[i, p] == 1)
                            {
                                flagRow = i;
                                flagCol = p;
                                break;
                            }
                        }
                        for (int q = 0; q < n; q++) //check row
                        {
                            if (a[q, j] == 1)
                            {
                                flagRow = q;
                                flagCol = j;
                                break;
                            }
                        }
                        //for diagonals
                        x = i;
                        y = j;

                        while ((x >= 0 && x < n) && (y >= 0 && y < n))//for left upward
                        {
                            if (a[x, y] == 1)
                            {
                                flagRow = x;
                                flagCol = y;
                                break;
                            }

                            x--; y--;

                        }

                        x = i;
                        y = j;

                        while ((x >= 0 && x < n) && (y >= 0 && y < n))//for right upward
                        {
                            if (a[x, y] == 1)
                            {
                                flagRow = x;
                                flagCol = y;
                                break;
                            }
                            x--; y++;

                        }

                        x = i;
                        y = j;

                        while ((x >= 0 && x < n) && (y >= 0 && y < n))//for left downwords
                        {
                            if (a[x, y] == 1)
                            {
                                flagRow = x;
                                flagCol = y;
                                break;
                            }
                            x++; y--;
                        }

                        x = i;
                        y = j;

                        while ((x >= 0 && x < n) && (y >= 0 && y < n))//for right downwords
                        {
                            if (a[x, y] == 1)
                            {
                                flagRow = x;
                                flagCol = y;
                                break;
                            }
                            x++; y++;
                        }





                        if (flagRow == 999 && flagCol == 999)
                        {
                            if (b[i, j] == 0)
                            {
                                a[i, j] = 1;
                            }
                            else
                            {
                                b[i, j] = 0;
                            }
                        }

                    }
                }



                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (a[i, j] == 1)
                        {
                            QueenNumber++;


                        }
                    }
                }
                print(QueenNumber);
                printArray();

                yield return new WaitForSeconds(0.01f);
                if (QueenNumber == n)// means all queen satisfy
                {
                    g = n;
                    h = n;
                }
                else
                {
                    InitializeWithZero();  //means not satisfy clear last one and branch bound  

                }
                printArray();
                yield return new WaitForSeconds(0.01f);

            }
        }
        printArray1();
    }
}


