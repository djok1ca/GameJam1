using UnityEngine;

public class knight_script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void move_a()
    {
        Debug.Log("heloo");
        Vector3 a = new Vector3(this.gameObject.transform.position.x+2.5f, this.gameObject.transform.position.y,this.gameObject.transform.position.z);
        this.gameObject.transform.position = a;
    }
}
