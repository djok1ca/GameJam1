using UnityEngine;

public class Vojska : MonoBehaviour
{
    public int width = 6;
    public int height = 4;
    private int[,] gridArray;
    private int time = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridArray = new int[width, height];
}

    // Update is called once per frame
   /* void FixedUpdate()
    {
        Debug.Log("nes");
        ++time;
        if (time == 10000)
        {
            time = 0;
            //pomeranje
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (gridArray[i, j] == 1)
                    {
                        gridArray[i, j] = 0;
                        gridArray[i + 1, j] = 1;
                    }
                    if (gridArray[i, j] == 2)
                    {
                        gridArray[i, j] = 0;
                        gridArray[i - 1, j] = 2;
                    }
                }
            }

            //stvaranje vojnika
            int rn = Random.Range(0, 5);
            gridArray[0, rn] = 1;
            //setValue(0, rn, 1);
            rn = Random.Range(0, 5);
            gridArray[width - 1, rn] = 2;
            //setValue(width - 1, rn, 1);
        }
    }*/
}
