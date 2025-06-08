using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.Jobs;
public class Testing : MonoBehaviour
{
    private Grid grid;

    public int width = 8;
    public int height = 4;
    // private int[,] gridArray;
    private int time = 0;
    private int prev = 0;
    private int next = 0;

    public GameObject Warrior_Blue_;
    public GameObject Warrior_Red_0;

    private void Start()
    {
        grid = new Grid(width, height, 2.5f,  Warrior_Blue_, Warrior_Red_0);
        // gridArray = new int[width, height];
    }

    private void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
              //grid.Freeze(UtilsClass.GetMouseWorldPosition());
        }


        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }
    void FixedUpdate()
    {

        ++time;
        if (time == 100)
        {
            
            time = 0;
            //pomeranje
            for (int j = 0; j < height; j++)
            {
                prev = 0;
                next = 0;
                for (int i = 0; i < width; i++)
                {
                    if (grid.gridArray[i, j] < 0 && grid.flagMatrix[i, j] == false)
                    {

                        prev = next;
                        next = 0;
                        if (grid.gridArray[i + 1, j] >= 0)//moze bez granjanja
                        {
                            if (grid.flagMatrix[i + 1, j] == true)
                            {
                                grid.setValue(i + 1, j, grid.gridArray[i, j]);
                                grid.setValue(i, j, prev);
                                prev = 0;
                            }
                            else
                            {
                                grid.setValue(i + 1, j, grid.gridArray[i + 1, j] + grid.gridArray[i, j]);
                                grid.setValue(i, j, prev);
                                prev = 0;
                            }
                            
                        }
                        else
                        {
                            if (grid.flagMatrix[i + 1, j])
                            {
                                grid.setValue(i + 1, j, grid.gridArray[i + 1, j] + grid.gridArray[i, j]);
                                grid.setValue(i, j, prev);
                                prev = 0;
                            }
                            else {
                                next = grid.gridArray[i + 1, j];
                                grid.setValue(i + 1, j, grid.gridArray[i, j]);
                                grid.setValue(i, j, prev);
                                prev = 0;
                            }
                         
                        }
                        if ((i + 1) == width - 1)
                        { Debug.Log("Pobedio igrac 1"); }
                        ++i;

                    }


                    if (grid.gridArray[i, j] == 0 && next != 0 && grid.flagMatrix[i, j] == false)
                    {
                        //grid.gridArray[i + 1, j] = prev;
                        prev = next;
                        next = 0;
                        //grid.gridArray[i + 1, j] = prev;
                        grid.setValue(i, j, prev);
                    }

                    if (grid.gridArray[i, j] > 0 && grid.flagMatrix[i, j] == false)
                    {
                        // gridArray[i, j] = 0;
                        // gridArray[i - 1, j] = 2;

                        if (grid.flagMatrix[i - 1, j] == true && grid.gridArray[i-1,j] < 0)
                        {
                            grid.setValue(i - 1, j, grid.gridArray[i, j]);
                            grid.setValue(i, j, 0);
                        }
                        
                        else
                        {
                            grid.setValue(i - 1, j, grid.gridArray[i - 1, j] + grid.gridArray[i, j]);
                            grid.setValue(i, j, 0);
                        }
                            

                        if ((i - 1) == 0)
                        { Debug.Log("Pobedio igrac 2"); }

                        
                            
                    }
                }

                for (int k = 0; k < width; k++)
                {
                    grid.flagMatrix[k, j] = false;
                    grid.rewindMatrix[k, j] = false;
                }
            
            }

            grid.TeleportGive();

            //stvaranje vojnika
            int rn = Random.Range(0, height);
            //gridArray[0, rn] = 1;
            grid.setValue(0, rn, -1);
            rn = Random.Range(0, height);
            grid.setValue(width-1, rn, 1);

            //gridArray[width - 1, rn] = 2;

        }
    }
        public void freaze(int fx1,int fy1, int fx2,int fy2, int fx3,int fy3, int fx4,int fy4,int tip)
        {
        if (tip == 1)
        { 
            grid.Freeze(fx1, fy1);
            grid.Freeze(fx2, fy2);
            grid.Freeze(fx3, fy3);
            grid.Freeze(fx4, fy4);

        }
        if(tip == 2)
        {

            grid.Rewind(fx1, fy1);
            grid.Rewind(fx2, fy2);
            grid.Rewind(fx3, fy3);
            grid.Rewind(fx4, fy4);
        }
        if (tip == 3)
        {

            grid.TeleportTake(fx1, fy1);
            grid.TeleportTake(fx2, fy2);
            grid.TeleportTake(fx3, fy3);
            grid.TeleportTake(fx4, fy4);
        }
        #region matrica
        /*grid.setValue(0, 0, 0);
        grid.setValue(1, 0, 1);
        grid.setValue(2, 0, 2);
        grid.setValue(3, 0, 3);
        grid.setValue(4, 0, 4);
        grid.setValue(5, 0, 5);
        grid.setValue(6, 0, 6);
        grid.setValue(7, 0, 7);

        grid.setValue(0, 1, 8);
        grid.setValue(1, 1, 9);
        grid.setValue(2, 1, 10);
        grid.setValue(3, 1, 11);
        grid.setValue(4, 1, 12);
        grid.setValue(5, 1, 13);
        grid.setValue(6, 1, 14);
        grid.setValue(7, 1, 15);

        grid.setValue(0, 2, 16);
        grid.setValue(1, 2, 17);
        grid.setValue(2, 2, 18);
        grid.setValue(3, 2, 19);
        grid.setValue(4, 2, 20);
        grid.setValue(5, 2, 21);
        grid.setValue(6, 2, 22);
        grid.setValue(7, 2, 23);

        grid.setValue(0, 3, 24);
        grid.setValue(1, 3, 25);
        grid.setValue(2, 3, 26);
        grid.setValue(3, 3, 27);
        grid.setValue(4, 3, 28);
        grid.setValue(5, 3, 29);
        grid.setValue(6, 3, 30);
        grid.setValue(7, 3, 31);

*/
        #endregion
        Debug.Log("freze squre are :"+fx1+","+fy1 +" " + fx2+","+fy2+" " + fx3+","+fy3+" " + fx4+","+fy4);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Debug.Log(i + " " + j);
                Debug.Log(grid.flagMatrix[j, i].ToString());
            }
        }

    }

}
