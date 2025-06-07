using UnityEngine;

public class Spawn_freaze : MonoBehaviour
{
    public bool spawned;
    public GameObject obj1;
    public GameObject obj2;
    GameObject[] list;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        list = new GameObject[2];
        list[0] = obj1;
        list[1] = obj2;
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
        int val = Random.Range(0, 2);
        
        GameObject shape = Instantiate(list[val]); //TODO add plase that it spawns

    }
}
