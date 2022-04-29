using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ref: https://drive.google.com/file/d/1WiF2LwM-6WvEnas9vw32YrYPly9K0Qrv/view


public class Player : MonoBehaviour
{
    private Player p;
    private BoardManager board;
    List<Cell> path;
    [SerializeField]
    private float moveSpeed = 2f;

    public Vector2 GetPosition => transform.position;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(p);
        
        //board.MoveEnemy((int)p.GetPosition.x, (int)p.GetPosition.y);
        DoMovement();

        //Debug.Log("X: "+p.GetPosition.x);
        //Debug.Log("Y: "+p.GetPosition.y);
        //Debug.Log("Estoy en el update de Player");
            
        
            
        
    }

    public void GetPlayer(Player p)
    {
        this.p = p;
    }

    public void SetPath(List<Cell> path)
    {
        //ResetPosition();
        waypointIndex = 0;
        this.path = path;
    }

    public void getBoard(BoardManager board)
    {
        this.board = board;
    }

    public void ResetPosition()
    {
        transform.position = new Vector2(0, 0);
    }

    private void Move()
    {
        // If player didn't reach last waypoint it can move
        // If player reached last waypoint then it stops
        Debug.Log("path: "+path);
        if (path == null)
            return;

        if (waypointIndex <= path.Count - 1)
        {

            // Move player from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
               path[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            // If player reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and player starts to walk to the next waypoint
            if (transform.position == path[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
    }

    void OnEnable()
    {
        // Subscribe to onPlayerMovement event
        PlayerMovement.onPlayerMovement += DoMovement;
    }

    void OnDisable()
    {
        // Unsubscribe to onPlayerMovement event
        PlayerMovement.onPlayerMovement -= DoMovement;
    }

    void DoMovement()
    {
        Move();
        Debug.Log("Player moved");
    }
        // Do something
}
