using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Use to stop other aspects of game when GameOver == true
    // such as camera or enemy spawner
    public static bool gameEnded;

    public GameObject gameOverUI;

    // Use this for initialization
    void Start()
    {
        gameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
            return;

        if (Input.GetKeyDown("o"))
        {
            EndGame();
        }

        // Set parameter to determine end game(time, lives)
        if (false)//life == 0)
        {
            EndGame();
        }


    }

    public void EndGame()
    {
        gameEnded = true;

        gameOverUI.SetActive(true);
    }
}
