using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject gameWinMenu;
    public GameObject gameLoseMenu; 


    public GameObject coinsParent;
    

    [Range(0f, 1f)] public float coinsToWinInPercent;
    private int numOfCoins;
    public int coinsToWin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale=1;
       gameWinMenu.SetActive(false);
       gameLoseMenu.SetActive(false); 

       numOfCoins = coinsParent.transform.childCount;  
       coinsToWin =Mathf.RoundToInt(numOfCoins * coinsToWinInPercent); 
    }

    public void WinGame(){
        gameWinMenu.SetActive(true);
        Time.timeScale=0;
        Cursor.lockState=CursorLockMode.None;
        Cursor.visible=true;

    }

    public void LoseGame(){
        gameLoseMenu.SetActive(true);
        Time.timeScale=0;
        Cursor.lockState=CursorLockMode.None;
        Cursor.visible=true;

        

    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }

    public void QuitGame(){
        Application.Quit();
    }
}
