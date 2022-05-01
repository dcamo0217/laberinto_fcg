using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{
    public PlayerMovement pm;
    public static BoardManager Instance;
    [SerializeField] private Cell CellPrefab;

    [SerializeField] private PlayerMovement HeroPrefab;

    [SerializeField] private Player EnemyPrefab;
    private Grid grid;
    private PlayerMovement player;
    private Player enemy;
    [SerializeField]
    private int gridSize;

    private static float timeStart = 0;

    private Text timeTextBox;

    private Text levelTextBox;
    private List<Cell> path;

    private List<Player> enemys = new List<Player>();

    private static int numEnemys=0;

    private WinMenuManager winMenuManager = new WinMenuManager();

    private EndManager endManager = new EndManager();
    private static float sumTime=0;

    private string NumNiveles = "numNiveles";
    private string SumTime = "sumTime";

    private int isEnd = 0;

    private string IsEndBM = "isEndBM";


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerPrefs.SetInt(NumNiveles, numEnemys);
        
        if(numEnemys>3){
            numEnemys=0;
            timeStart=0;
            SceneManager.LoadScene("WinScene");
            PlayerPrefs.SetFloat(SumTime, sumTime);
        }
        
        timeTextBox= GameObject.Find("timer").GetComponent<Text>();
        levelTextBox= GameObject.Find("level").GetComponent<Text>();
        gridSize = PlayerPrefs.GetInt("DropdownValue");
        grid = new Grid(gridSize, gridSize, 1, CellPrefab);
        player = Instantiate(HeroPrefab, new Vector2(0, (float)0), Quaternion.identity);
        numEnemys++;
        for (int i = 0; i < numEnemys; i++)
        {
            List<Cell> obs = grid.GetGridsObtacles();
            int randomPositionX = Random.Range(gridSize/2, gridSize);
            int randomPositionY = Random.Range(gridSize/2, gridSize);
            //bool aux = true;
            enemy = Instantiate(EnemyPrefab, new Vector2(randomPositionX-1, (float)(randomPositionY-1+0.2)), Quaternion.identity);
            enemy.getBoard(this);
            enemys.Add(enemy);
            
        }
        timeTextBox.text = timeStart.ToString();
        levelTextBox.text = "Nivel: 1";
        
        sumTime+=Mathf.Round(timeStart);
        
        
        
         
        
        
        
    }

    public int GetNumEnemys()
    {
        return numEnemys;
    }

    public void ResetTotalTime()
    {
        sumTime = 0;
        timeStart = 0;
    }
    public void ResetNumEnemys(){
        numEnemys=0;
    }

    void Update()
    {
        timeStart += Time.deltaTime;
        timeTextBox.text="Tiempo: "+Mathf.Round(timeStart).ToString()+" seg";
        levelTextBox.text="Nivel "+numEnemys;
        
        
    }

    
    public float getSumTime(){
        return sumTime;
    }

    public void MoveEnemy(int x, int y)
    {
        foreach (var enemy in enemys)
        {
            List<Cell> path = PathManager.Instance.FindPath(grid, (int)enemy.GetPosition.x, (int)enemy.GetPosition.y, x, y);
            enemy.SetPath(path);
        }
        
    }

    // set path
    public Grid SetGrid()
    {
       return grid;
    }
    



}
