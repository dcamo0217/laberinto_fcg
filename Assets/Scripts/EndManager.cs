using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndManager : MonoBehaviour
{

    private int isEnd = 0;
    private BoardManager board;
    private Text levelReachedText;


    private string BoolFinish = "EndBoolFinish";

    void Start()
    {
        isEnd = PlayerPrefs.GetInt("isEndBM");
        levelReachedText = GameObject.Find("levelReached").GetComponent<Text>();
    }

    void Update()
    {   
        int a = PlayerPrefs.GetInt("numNiveles")+1;
        levelReachedText.text = "Nivel alcanzado: " + a;
    }
    public void onClick()
    {
        isEnd = 1;
        SceneManager.LoadScene("StartScene");
        PlayerPrefs.SetInt(BoolFinish, isEnd);  
    }

    
}
