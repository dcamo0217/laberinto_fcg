using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    public Dropdown nDropdown;

    public Dropdown mDropdown;
    private string nDropdownPrefsKey = "nDropdownValue";

    private string mDropdownPrefsKey = "mDropdownValue";

    // Update is called once per frame
    public void SaveData(){
        PlayerPrefs.SetInt(nDropdownPrefsKey, int.Parse(nDropdown.options[nDropdown.value].text));
        PlayerPrefs.SetInt(mDropdownPrefsKey, int.Parse(mDropdown.options[mDropdown.value].text));
    }

    public void NextScene(){
        SceneManager.LoadScene("GameScene");
    }
}
