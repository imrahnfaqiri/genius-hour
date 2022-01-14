using UnityEngine;

public class characterMove : MonoBehaviour
{

    float horizontal;
    float vertical;

    private Rigidbody2D rb2d;

    public float jumpVelocity;
    public float moveVelocity;
    public int moveDir = 0; // -1 = left, 1 = right, 0 = not moving
    public int lastDir = 1;

    private bool canJump = false;

    public GameObject character;
    public GameObject muzzle;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // just call all the movement functions so the character can start moving
        check_input();
        move_char();
        change_dir();
    }

    public void check_input() {
        if (Input.GetKeyDown(KeyCode.A)) {
            moveDir = -1;
            lastDir = 1;
        } else if (Input.GetKeyDown(KeyCode.D)) {
            moveDir = 1;
            lastDir = -1;
        }
        if (Input.GetKeyUp(KeyCode.A) || (Input.GetKeyUp(KeyCode.D))) {
            moveDir = 0;
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            jump();
        }
        // shoot the bullet
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject myInstance = GameObject.Instantiate(bulletPrefab, muzzle.transform.position, character.transform.rotation) as GameObject;
            float sped = (moveVelocity * lastDir) + 5;


            myInstance.GetComponent<bullethandle>().moveDir = -lastDir;
            // GameObject.Instantiate(bulletPrefab, muzzle.transform.position, character.transform.rotation);

        }
    }

    public void move_char() {
        if (moveDir == -1){ // left
            move();
        }
        else if (moveDir == 0){ // no moving
           // Debug.Log("not moving");
        }
        else if (moveDir == 1){ // right
            move();
        }
    }

    public void jump() {
        if (canJump == true) {
            rb2d.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse); // rb2d.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        }
    }

    public void move() {
        rb2d.velocity = new Vector2(moveVelocity * moveDir, rb2d.velocity.y);
    }

    public void change_dir() {
        if (moveDir == -1)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else if (moveDir == 1) {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor") {
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor") {
            canJump = false;
        }
    }
}
