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
    public Testing con;
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
            if (transform.position.x > -1f && transform.position.x < 19.5f && transform.position.y > -2.25f && transform.position.y < 9.75f)
            {
                Destroy(this.gameObject);
                exists = false;
                a.spawned = false;
                int cx = (int)Mathf.Round((transform.position.x - 1.25f) / increment);
                int cy = (int)Mathf.Round((transform.position.y - 1.25f) / increment);
                if (this.gameObject.tag == "Shape_L")
                {
                    con.freaze(cx, 3 - cy, cx + 1, 3 - cy, cx, 2 - cy, cx, 1 - cy);

                }
                else if (this.gameObject.tag == "Shape_sq")
                {
                    con.freaze(cx, 3 - cy, cx + 1, 3 - cy, cx, 2 - cy, cx + 1, 2 - cy);

                }
                else if (this.gameObject.tag == "Shape_I")
                {
                    con.freaze(cx, 3 - cy, cx , 2 - cy, cx, 1 - cy, cx  , cy);
                }
                else
                {
                    Debug.Log(this.gameObject.tag);
                }
            }
            dragging = false;
        }

        // Move object if dragging
        if (dragging)
        {
            float closestx;
            float closesty;
            transform.position = mouseWorldPos + offset;
            if (transform.position.x > -1f && transform.position.x < 19.5f && transform.position.y > -2.25f && transform.position.y < 9.75f)
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





