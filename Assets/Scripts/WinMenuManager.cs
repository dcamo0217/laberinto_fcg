using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenuManager : MonoBehaviour
{
    private int isEnd=0;
    private BoardManager board;
    private Text totalTimeText;

    private string BoolFinish = "WinBoolFinish";

    void Start()
    {
        totalTimeText = GameObject.Find("totalT").GetComponent<Text>();
    }

    void Update()
    {
        int a = PlayerPrefs.GetInt("sumTime");
        totalTimeText.text = "Tiempo total: " + a;
    }
    public void onClick()
    {
       isEnd=1;
       SceneManager.LoadScene("StartScene");
       PlayerPrefs.GetInt(BoolFinish, isEnd);
       
    }

    
}
