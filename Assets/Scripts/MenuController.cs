using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public InputField PlayerName;

    public void StartGame()
    {
        SetPlayerName();
    }

    public void SetPlayerName()
    {
        PlayerPrefs.SetString("playerName", PlayerName.text);
        PlayerPrefs.Save();
    }
}
