using UnityEngine;
using CodeMonkey.Utils;
public class Testing : MonoBehaviour
{
    private Grid grid;
    private void Start()
    { 
        grid = new Grid(6, 4, 2.5f);
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
}
