using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class shape_move : MonoBehaviour
{
      private bool dragging = false;
      public float x = 1.25f;
    public float increment = 2.5f;
    public bool exists;
    public Spawn_freaze a;
  private Vector3 offset;

    void Start()
    {
        GameObject spawner = GameObject.FindWithTag("Shape_spawner");
        
       //a = spawner;
    }

    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        // On mouse down: check ONLY this object's collider directly
        if (Input.GetMouseButtonDown(0))
        {
            if (GetComponent<Collider2D>().OverlapPoint(mouseWorldPos))
            {
                offset = transform.position - mouseWorldPos;
         
                Debug.Log(offset);
                    dragging = true;
            }
        }

        // Release mouse
        if (Input.GetMouseButtonUp(0) && dragging)
        {
            Destroy(this.gameObject);
            exists = false;
            dragging = false;
            a.spawned = false;
        }

        // Move object if dragging
        if (dragging)
        {
            float closestx;
            float closesty;
            transform.position = mouseWorldPos + offset;
            if (transform.position.x > 0f && transform.position.x < 13f && transform.position.y > -1.25f && transform.position.y < 8.75f)
            {
                closestx = Mathf.Round((transform.position.x - 1.25f) / increment) * increment;
                 closesty = Mathf.Round((transform.position.y - 1.25f) / increment) * increment;
            }
            else
            {closestx = transform.position.x;
                closesty = transform.position.y;
                }
            transform.position = new Vector3(closestx+1.25f,closesty+1.25f,transform.position.z);
        }
    }
}





