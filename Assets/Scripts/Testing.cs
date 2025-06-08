using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.Jobs;
using UnityEngine.Rendering.Universal;
public class Testing : MonoBehaviour
{
    private Grid grid;

    public int width = 8;
    public int height = 4;
    // private int[,] gridArray;
    private int time = 0;
    private int prev = 0;
    private int next = 0;
    public bool end = false;

    public GameObject Warrior_Blue_;
    public GameObject Warrior_Red_0;
    public Spawn_freaze shape_spawn;
    public GameObject Explosion;
    public GameObject[,] eksplozije;
    public bool[,] eksplo;
    private void Start()
    {
        grid = new Grid(width, height, 2.5f, Warrior_Blue_, Warrior_Red_0, Explosion);
        eksplozije = new GameObject[width+5, height+5];
        eksplo = new bool[width+5, height+5];
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
        if (time == 35)
        {
            for (int j = 0; j < height; j++)
            {

                for (int i = 0; i < width; i++)
                {
                    if (eksplo[i, j])
                    {
                        eksplo[i, j] = false;
                        Destroy(eksplozije[i, j]);
                    }
                }
            }
        }
        ++time;
        if (time == 70)
        {
            shape_spawn.call_spawn();
            time = 0;
            //pomeranje
            for (int j = 0; j < height; j++)
            {
                prev = 0;
                next = 0;
                for (int i = 0; i < width; i++)
                {
                    if (grid.gridArray[i, j] < 0 && grid.flagMatrix[i, j] == false)//ako je dobar vitez
                    {

                        prev = next;
                        next = 0;
                        if (grid.gridArray[i + 1, j] >= 0)//moze bez granjanja //ako je i ako je sledecin los vitez
                        {
                            if (grid.flagMatrix[i + 1, j] == true)//ako je zaledjen
                            {
                                grid.setValue(i + 1, j, grid.gridArray[i, j]);
                                grid.setValue(i, j, prev);
                                prev = 0;
                            }
                            else //ako nije zaledjen
                            {
                                grid.setValue(i + 1, j, grid.gridArray[i + 1, j] + grid.gridArray[i, j]);
                                grid.setValue(i, j, prev);
                                prev = 0;
                            }

                        }
                        else //ako sledeci
                        {
                            if (grid.flagMatrix[i + 1, j])
                            {
                                grid.setValue(i + 1, j, grid.gridArray[i + 1, j] + grid.gridArray[i, j]);
                                grid.setValue(i, j, prev);
                                prev = 0;
                            }
                            else
                            {
                                next = grid.gridArray[i + 1, j];
                                grid.setValue(i + 1, j, grid.gridArray[i, j]);
                                grid.setValue(i, j, prev);
                                prev = 0;
                            }

                        }
                        if ((i + 1) == width - 1)
                        {
                            Debug.Log("Pobedio igrac 1");
                            end = true;
                            grid.ended = true;
                            time = 1000;
                        }
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
                        prev = next;
                        next = 0;
                        grid.setValue(i, j, grid.gridArray[i, j] + prev);
                        prev = 0;
                        if (grid.gridArray[i, j] > 0)//los vitez potez
                        {
                            // gridArray[i, j] = 0;
                            // gridArray[i - 1, j] = 2;

                            if (grid.flagMatrix[i - 1, j] == true && grid.gridArray[i - 1, j] < 0)
                            {
                                grid.setValue(i - 1, j, grid.gridArray[i, j]);
                                grid.setValue(i, j, 0);
                            }

                            else
                            {
                                if (grid.gridArray[i - 1, j] + grid.gridArray[i, j] == 0)
                                {
                                    Debug.Log("STVROI EKSPOZIJU");
                                    // Instantiate(Explosion, new Vector3(1, 1, 1), Quaternion.identity);
                                    eksplozije[i, j] = Instantiate(Explosion, grid.GetWorldPositionKnight(i - 1, j), Quaternion.identity); ;
                                    eksplo[i, j] = true;
                                }
                                grid.setValue(i - 1, j, grid.gridArray[i - 1, j] + grid.gridArray[i, j]);
                                grid.setValue(i, j, 0);
                            }


                            if ((i - 1) == 0)
                            {
                                Debug.Log("Pobedio igrac 2");
                                end = true;
                                grid.ended = true;
                                time = 1000;
                            }
                        }




                    }
                }

                for (int k = 0; k < width; k++)
                {
                    grid.flagMatrix[k, j] = false;
                    grid.rewindMatrix[k, j] = false;
                }

            }
            if (end == false)
            {
                grid.TeleportGive();

                //stvaranje vojnika
                int rn = Random.Range(0, height);
                //gridArray[0, rn] = 1;
                grid.setValue(0, rn, -1);
                rn = Random.Range(0, height);
                grid.setValue(width - 1, rn, 1);
                rn = Random.Range(0, height);
                grid.setValue(width - 1, rn, 1);
                //gridArray[width - 1, rn] = 2;
            }
            /////////////////////////////////////
            //set mode
            ////////////////////////////////////
            for (int j = 0; j < height; j++)
            {
                prev = 0;
                next = 0;
                for (int i = 0; i < width; i++)
                {
                    if (grid.gridArray[i, j] > 0)
                    {
                        grid.vitezovi[i, j].GetComponent<red_warrior>().mode = 0;
                    }
                    if (grid.gridArray[i, j] > 0)
                        {
                            if (grid.gridArray[i - 1, j] < 0)
                            {
                                Debug.Log("Sudar na" + i + " ," + j);
                                grid.vitezovi[i, j].GetComponent<red_warrior>().mode = 1;
                                grid.vitezovi[i - 1, j].GetComponent<blue_warrior>().mode = 1;
                            }
                            else
                            {
                                grid.vitezovi[i, j].GetComponent<red_warrior>().mode = 0;
                            }

                        }
                   /* else
                    {
                        grid.vitezovi[i, j].GetComponent<red_warrior>().mode = 0;
                    }*/
                }
            }
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
            grid.rewindMatrix[fx1, fy1] = true;
            grid.rewindMatrix[fx2, fy2] = true;
            grid.rewindMatrix[fx3, fy3] = true;
            grid.rewindMatrix[fx4, fy4] = true;

            grid.Rewind(fx1, fy1);
            grid.Rewind(fx2, fy2);
            grid.Rewind(fx3, fy3);
            grid.Rewind(fx4, fy4);

            grid.rewindMatrix[fx1, fy1] = false;
            grid.rewindMatrix[fx2, fy2] = false;
            grid.rewindMatrix[fx3, fy3] = false;
            grid.rewindMatrix[fx4, fy4] = false;
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
       /* Debug.Log("freze squre are :"+fx1+","+fy1 +" " + fx2+","+fy2+" " + fx3+","+fy3+" " + fx4+","+fy4);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Debug.Log(i + " " + j);
                Debug.Log(grid.flagMatrix[j, i].ToString());
            }
        }*/

    }

}
