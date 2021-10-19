using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public GameObject gameEndUI;
    public Text candyCountText;
    public Text gameEndText;
    private Animator anim;

    public void Start()
    {
        
    }

    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Restart();
        }
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenEndGameScreen()
    {
        gameEndUI.SetActive(true);
        anim = GameObject.FindGameObjectWithTag("Anim").gameObject.GetComponent<Animator>();

        anim.SetTrigger("PumpkinTrigger");

        StartCoroutine(WaitForAnimCoroutine());
    }

    IEnumerator WaitForAnimCoroutine()
    {
        //The purpose of this corotuine is to allow the pumpkin animation to run before pausing time on the game

        //Debug.Log("Coroutine running");
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
        PlayerController player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>();
        candyCountText.text = "Score: " + player.getCandyCount();
        if (player.getCandyCount() > 0)
        {
            gameEndText.text = "Spooktacular effort!";
        } else
        {
            gameEndText.text = "Better luck next time";
        }
    }

}
