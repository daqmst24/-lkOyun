using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Player player;
    //Uı
    public Slider healtBar;
    public Text points;
    void Start()
    {
        player = FindObjectOfType<Player>();
        healtBar.maxValue = player.maxPlayerHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        points.text =player.CurrrentPoint.ToString();

       if (player.isDead)
    {
          Invoke("RestartGame", 5);
       }
        UpdateUI();
    }
   public void RestartGame()
   {

      Scene scene = SceneManager.GetActiveScene();
       SceneManager.LoadScene(scene.name);
    }
    void UpdateUI()
    {
        healtBar.value = player.currentPlayerHealth;

        if (player.currentPlayerHealth <= 0)
            healtBar.minValue = 0;

    }

}
