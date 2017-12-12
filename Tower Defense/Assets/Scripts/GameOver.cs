using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public Text wavesText;

    private void OnEnable()
    {
        // Link to where waves is initiated
        //wavesText.text = PlayerStats.Waves.ToString();
    }

    public void Retry()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void Menu()
    {
        Debug.Log("Go to menu");
    }

}
