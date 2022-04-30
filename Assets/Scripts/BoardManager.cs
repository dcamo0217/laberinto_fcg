using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoardManager : MonoBehaviour
{
    public PlayerMovement pm;
    public static BoardManager Instance;
    [SerializeField] private Cell CellPrefab;
    [SerializeField] private Player PlayerPrefab;

    [SerializeField] private Player HeroPrefab;

    [SerializeField] private Player EnemyPrefab;
    private Grid grid;
    private Player player;
    private Player enemy;
    [SerializeField]
    private float moveSpeed = 2f;
    private int gridSize;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gridSize = PlayerPrefs.GetInt("DropdownValue");
        grid = new Grid(gridSize, gridSize, 1, CellPrefab);

        player = Instantiate(HeroPrefab, new Vector2(0, (float)0), Quaternion.identity); 
        enemy = Instantiate(EnemyPrefab, new Vector2(gridSize-1, (float)(gridSize-1+0.2)), Quaternion.identity);
        enemy.getBoard(this);
        enemy.GetPlayer(player);
    }

    public void MoveEnemy(int x, int y)
    {
        List<Cell> path = PathManager.Instance.FindPath(grid, (int)enemy.GetPosition.x, (int)enemy.GetPosition.y, x, y);
        enemy.SetPath(path);
    }



}
