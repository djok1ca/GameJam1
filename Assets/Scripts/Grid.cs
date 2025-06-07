using UnityEngine;
using CodeMonkey.Utils;
using System.Xml.Serialization;
using UnityEngine.PlayerLoop;
using Unity.VisualScripting;
using System.Security.Cryptography;
using static UnityEngine.Rendering.DebugUI;
public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    public int[,] gridArray;
    private TextMesh[,] debugTextArray;
    int time = 0;
    public bool[,] flagMatrix;
    public bool[,] rewindMatrix;


    [Header("Grid Stats")]
    [SerializeField] private int  _gridOffset = 0;
    
#region GridCreation  
    public Grid(int width, int height, float cellSize) 
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        

        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];
        flagMatrix = new bool[width, height];
        rewindMatrix = new bool[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++) 
        {
            for(int y = 0; y < gridArray.GetLength(1); y++)
            {
                
                debugTextArray[x, y] =  UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x - _gridOffset, y - _gridOffset) + new Vector3(cellSize,cellSize)*0.5f, 30, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x- _gridOffset, y - _gridOffset), GetWorldPosition(x - _gridOffset, y + 1 - _gridOffset), Color.white, float.PositiveInfinity);
                Debug.DrawLine(GetWorldPosition(x- _gridOffset, y - _gridOffset), GetWorldPosition(x+1 - _gridOffset, y  - _gridOffset), Color.white, float.PositiveInfinity);
            }
            Debug.DrawLine(GetWorldPosition(0 - _gridOffset, height - _gridOffset), GetWorldPosition(width - _gridOffset, height - _gridOffset), Color.white, float.PositiveInfinity);
            Debug.DrawLine(GetWorldPosition(width - _gridOffset, 0 - _gridOffset), GetWorldPosition(width - _gridOffset, height - _gridOffset), Color.white, float.PositiveInfinity);

            //setValue(2, 1, 56);
        }
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition.x + _gridOffset) / cellSize);
        y = Mathf.FloorToInt((worldPosition.y  + _gridOffset)/ cellSize);
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x,y) * cellSize; 
    }

    public void setValue(int x, int y, int value) 
    {
        if(x >= 0 && y >= 0 && x < width && y < height) 
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x,y].ToString();
        }
        
    }

    public void setValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        setValue(x, y, value); 

    }

    public int GetValue(int x,int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
            return gridArray[x, y];
        else return 0;
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    #endregion

    #region GameAbilities

    public void Freeze(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);

        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            flagMatrix[x, y] = true;
        }
    }

    public void Freeze(int x, int y)
    {
       
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            flagMatrix[x, y] = true;
        }
    }

    public void Rewind(int x, int y)
    {

        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            //vratiti na polje iza
            if (gridArray[x, y] < 0)
            {
                if(x == 0)
                {
                    setValue(x, y, 0);
                }
                else
                {
                    setValue(x - 1, y, gridArray[x, y] + gridArray[x-1,y]);
                    setValue(x, y, 0);
                }
            }
            if (gridArray[x, y] > 0)//fixati kao za -1 u normalnoj
            {
                if (x == width-1)
                {
                    setValue(x, y, 0);
                }
                else
                {
                    setValue(x + 1, y, gridArray[x, y] + gridArray[x + 1, y]);
                    setValue(x, y, 0);
                }
            }
        }
    }

    #endregion 

}
