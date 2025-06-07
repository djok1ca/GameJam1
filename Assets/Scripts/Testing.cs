using UnityEngine;
using CodeMonkey.Utils;
public class Testing : MonoBehaviour
{
    private Grid grid;

    public int width = 6;
    public int height = 4;
   // private int[,] gridArray;
    private int time = 0;

 

    private void Start()
    { 
        grid = new Grid(width, height, 2.5f);
       // gridArray = new int[width, height];
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            grid.setValue(UtilsClass.GetMouseWorldPosition(), 56);
        }

        if(Input.GetMouseButtonDown (1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }
    void FixedUpdate()
    {
        
        ++time;
        if (time == 100)
        {
            Debug.Log("nes");
            time = 0;
            //pomeranje
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (grid.gridArray[i,j] < 0)//fixati da proverava da li je sledece polje -1 i da ga prebaci ako nije zaledjeno
                    {
                        
                            // gridArray[i, j] = 0;
                            //  gridArray[i + 1, j] = 1;
                        if (grid.gridArray[i+1, j] > 0 )//moze bez granjanja
                        {
                            //grid.gridArray[i + 1, j] += grid.gridArray[i, j];
                           // grid.gridArray[i, j] = 0;
                            grid.setValue(i + 1, j, grid.gridArray[i + 1, j] + grid.gridArray[i, j]);
                            grid.setValue(i, j, 0);
                        }
                        else
                        {
                     
                            grid.setValue(i + 1, j, grid.gridArray[i, j]);
                            grid.setValue(i, j, 0);
                        }
                        if ((i + 1) == width-1)
                        { Debug.Log("Pobedio igrac 1"); }
                        ++i;
                        
                    }

                   
                    

                    if (grid.gridArray[i, j] > 0)
                    {
                        // gridArray[i, j] = 0;
                        // gridArray[i - 1, j] = 2;
                        if (grid.gridArray[i - 1, j] < 0)//moze bez granjanja
                        {
                           // grid.gridArray[i - 1, j] += grid.gridArray[i, j];
                           // grid.gridArray[i, j] = 0;

                            grid.setValue(i-1, j, grid.gridArray[i - 1, j] + grid.gridArray[i, j]);
                            grid.setValue(i, j, 0);
                        }
                        else
                        {
                            grid.setValue(i - 1, j, grid.gridArray[i, j]);
                            grid.setValue(i, j, 0);
                        }
                        if ((i - 1) == 0)
                        { Debug.Log("Pobedio igrac 2"); }
                    }
                }
            }

            //stvaranje vojnika
            int rn = Random.Range(0, 4);
            //gridArray[0, rn] = 1;
            grid.setValue(0, rn, -1);
            rn = Random.Range(0, 4);
            grid.setValue(width-1, rn, 1);
            //gridArray[width - 1, rn] = 2;
        }
    }
}
