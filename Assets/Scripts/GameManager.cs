using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public GameObject gameEndUI;
    private Animator anim;

    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Restart();
        }
        
    }

    void Restart()
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
    }

}
