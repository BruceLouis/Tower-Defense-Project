using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GameDirector : MonoBehaviour {

    [SerializeField] GameObject towerMenu;
    [SerializeField] AudioClip baseDestroyed;
    [SerializeField] Text gameOverText, victoryText;
    [SerializeField] Button playAgainButton, nextLevelButton;

    private bool active;

    public void ActivateMenu()
    {
        active = !active;
        towerMenu.SetActive(active);
    }

    public void PlayAgain() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator GameOverSon()
    {
        AudioSource.PlayClipAtPoint(baseDestroyed, Camera.main.transform.position + new Vector3(0f, 0f, -10f));
        yield return new WaitForSecondsRealtime(3f);
        gameOverText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        playAgainButton.gameObject.SetActive(true);
    }

    public IEnumerator Victory()
    {
        victoryText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        nextLevelButton.gameObject.SetActive(true);
    }
}
