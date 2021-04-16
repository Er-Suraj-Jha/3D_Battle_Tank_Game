using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lobby_Controller : MonoBehaviour
{
    public Button Red_button;
    public Button Green_button;
    public Button Blue_button;
    public Button Orange_button;
    public string Red_Scene;
    public static bool Red=false;
    public static bool Green=false;
    public static bool Blue=false;
    public static bool Orange=false;

    void Start()
    {
        Red_button.onClick.AddListener(OnButtonClickRed);
        Green_button.onClick.AddListener(OnButtonClickGreen);
        Blue_button.onClick.AddListener(OnButtonClickBlue);
        Orange_button.onClick.AddListener(OnButtonClickOrange);
    }

private void OnButtonClickRed()
  {
      Debug.Log("Player entered the game with RED TANK");
      Red=true;
      Green=false;
      Blue=false;
      Orange=false;
      SceneManager.LoadScene(Red_Scene);
  }

  private void OnButtonClickGreen()
  {
      Debug.Log("Player entered the game with GREEN TANK");
      Green=true;
      Red=false;
      Blue=false;
      Orange=false;
      SceneManager.LoadScene(Red_Scene);
  }

  private void OnButtonClickBlue()
  {
      Debug.Log("Player entered the game with BLUE TANK");
      Blue=true;
      Red=false;
      Green=false;
      Orange=false;
      SceneManager.LoadScene(Red_Scene);
  }

  private void OnButtonClickOrange()
  {
      Debug.Log("Player entered the game with ORANGE TANK");
      Orange=true;
      Red=false;
      Green=false;
      Blue=false;
      SceneManager.LoadScene(Red_Scene);
  }
    
}