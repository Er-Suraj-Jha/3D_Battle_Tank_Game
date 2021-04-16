using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BattleTank.Tanks;

public class Enter_Again : MonoBehaviour
{
    public Button Restart;
    public Button Lobby;
    private TankView tankview;
    void Start()
    {
       // TankService.Instance.onDeathofPlayer += PlayerDied;
        Debug.Log("Enter_Again is called");
        Restart.onClick.AddListener(OnButtonClick1);
        Lobby.onClick.AddListener(OnButtonClick2);
    }

    public void PlayerDied()
    {
        Debug.Log("Ener_Again called");
        gameObject.SetActive(true);
    }

private void OnButtonClick1()
  {
      Scene scene = SceneManager.GetActiveScene();
      SceneManager.LoadScene(scene.buildIndex);
  }

  private void OnButtonClick2()
  {
     SceneManager.LoadScene("Lobby");
  }
    
}