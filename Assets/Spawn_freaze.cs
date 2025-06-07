using System.Linq.Expressions;
using UnityEngine;

public class Spawn_freaze : MonoBehaviour
{
    public bool spawned;
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public GameObject obj5;
    public GameObject obj6;
    public Testing cone;
    GameObject[] list;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        list = new GameObject[6];
        list[0] = obj1;
        list[1] = obj2;
        list[2] = obj3;
        list[3] = obj4;
        list[4] = obj5;
        list[5] = obj6;
        //spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            spawn();
            spawned = true;
        }
    }
    void spawn()
    {
        int val = Random.Range(0, 6);

        Vector3 offset = new Vector3(-5, -1, 0);
        GameObject shape = Instantiate(list[val]); //TODO add plase that it spawns
        shape.transform.position = offset;
        shape_move s = shape.GetComponent<shape_move>();
        s.con = cone;
        s.a = this;
    }
}
