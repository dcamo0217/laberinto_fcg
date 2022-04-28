using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum Direction 
{    
    up, down, left, right
}

public class PlayerMovement : MonoBehaviour
{
    public Cell cell;
    public LayerMask casilla;
    Animator anim;
    Vector2 targetPosition;
    Direction direction;
    public LayerMask obstacle;
    public GameObject player; 
    public float speed;
    public System.DateTime startTime;

    void Start()
    {
        anim = GetComponent<Animator>();
        targetPosition = transform.position;
        direction = Direction.down;
        startTime = System.DateTime.UtcNow;
    }
    
    void Update (){
        System.TimeSpan ts = System.DateTime.UtcNow - startTime;
        anim.SetInteger("Direction", 4);
        Vector2 axisDirection = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        anim.SetInteger("Direction", (int)direction);
        Debug.Log(direction);
        if(axisDirection != Vector2.zero && targetPosition == (Vector2)transform.position){
            if  (Mathf.Abs(axisDirection.x) > Mathf.Abs(axisDirection.y)){
                if(axisDirection.x > 0){
                    direction = Direction.right;
                    player.GetComponent<SpriteRenderer>().flipX = false;
                    if(!checkCollision)
                    
                        targetPosition += Vector2.right;
                }else{
                    direction = Direction.left;
                    player.GetComponent<SpriteRenderer>().flipX = true;
                    if(!checkCollision)
                        targetPosition -= Vector2.right;
                }
            }else{
                if(axisDirection.y > 0){
                    direction = Direction.up;
                    if(!checkCollision)
                        targetPosition += Vector2.up;
                }else{
                    direction = Direction.down;
                    if(!checkCollision)
                        targetPosition -= Vector2.up;
                }

            }
        }
        transform.position = Vector2.MoveTowards(transform.position,targetPosition,speed * Time.deltaTime); //MoveTowards is a function that moves the object towards a target position

        if (Physics2D.OverlapCircle(transform.position, 0.1f))
        {   
            //Obtener la colision
            Collider2D col = Physics2D.OverlapCircle(transform.position, 0.1f);
            cell = col.GetComponent<Cell>();  
            if (cell.getFinishGrid()){
                Debug.Log("Estoy en la ultima casilla");
                Debug.Log ("Tiempo transcurrido: "+ts.Seconds.ToString ()+" segundos");

            }else{
                if (cell.getStartGrid()){
                    Debug.Log("Estoy en la primera casilla");
                }else{
                    if (cell.getStartGrid() == false && cell.getFinishGrid() == false){
                        Debug.Log("Estoy en una casilla normal");
                    }
                }
            }
        }


    }

    private void onCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Enemy"))
        {
            Debug.Log("Me he chocado con un enemigo");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
            
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("Enemy"))
        {
            Debug.Log("Me he chocado con un enemigo");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    bool checkCollision
    {
        get
        {
            RaycastHit2D rh;

            Vector2 dir = Vector2.zero;

            if(direction == Direction.down){
                dir = Vector2.down;
            }else if(direction == Direction.up){
                dir = Vector2.up;
            }else if(direction == Direction.left){
                dir = Vector2.left;
            }else{
                dir = Vector2.right;
            }


            rh = Physics2D.Raycast(transform.position, dir,1, obstacle);

            return rh.collider != null;
        }
    }
}
