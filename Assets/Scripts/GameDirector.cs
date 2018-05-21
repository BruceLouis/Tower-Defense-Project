using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GameDirector : MonoBehaviour {

    [SerializeField] GameObject towerMenu, gameOverPanel, victoryPanel;
    [SerializeField] AudioClip baseDestroyed, youLoseSound;
    [SerializeField] Button playAgainButton, nextLevelButton;

    private EnemySpawner enemySpawner;
    private bool active;

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();    
    }

    void Update()
    {
        CheckForVictory();
    }

    void CheckForVictory()
    {
        int enemyCountInList = enemySpawner.GetEnemiesList().Count;
        int enemiesLeftToSpawn = enemySpawner.GetNumEnemies()[enemySpawner.GetNumEnemies().Length - 1];

        if (enemyCountInList <= 0 && enemiesLeftToSpawn <= 0 && !FindObjectOfType<Base>().GetGameOver())
        {
            StartCoroutine(Victory());
        }
    }

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
        gameOverPanel.SetActive(true);
        AudioSource.PlayClipAtPoint(youLoseSound, Camera.main.transform.position);
        yield return new WaitForSecondsRealtime(1f);
        playAgainButton.gameObject.SetActive(true);
    }

    public IEnumerator Victory()
    {
        yield return new WaitForSecondsRealtime(4f);
        victoryPanel.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        nextLevelButton.gameObject.SetActive(true);
    }
}
