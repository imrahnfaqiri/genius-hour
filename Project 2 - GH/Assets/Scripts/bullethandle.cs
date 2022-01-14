using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullethandle : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bullet;
    Rigidbody2D rb2d;

    public int speed = 7;
    public int moveDir = 1;

    public int direction;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void move()
    {
        rb2d.velocity = new Vector2(speed * moveDir, rb2d.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
}
