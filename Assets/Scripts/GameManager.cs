using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    Text m_textScore;
    Text m_textLifes;
    GameObject m_NextLevelPanel;
    GameObject m_GameOverPanel;
    GameObject m_PausePanel;
    private Ball m_Ball;
    private Ship m_Ship;
    [HideInInspector] static public int m_score;
    [HideInInspector] static public int m_lifes = 5;
    [HideInInspector]public int m_CurrentScore = 0;

    public int BrickNumber;
    int CurrentBrickNumber = 0;

    [HideInInspector]public bool isOut = true;

    bool RestartGamePanel = false;
    bool NextLevelPanel = false;
    bool GameIsPaused = false;

    private SoundManager m_DeathSound;
    private SoundManager m_GameOverSound;

    void Start ()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            
            m_textScore = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
            m_textLifes = GameObject.FindGameObjectWithTag("Lifes").GetComponent<Text>();
            m_Ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
            m_Ship = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();

            m_textScore.text = "Score:" + (m_CurrentScore + m_score);
        
            m_textLifes.text = "Lifes:" + m_lifes;
            m_NextLevelPanel = GameObject.FindGameObjectWithTag("NextLevelPanel");
            m_GameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");
            m_PausePanel = GameObject.FindGameObjectWithTag("PausePanel");
            m_PausePanel.SetActive(false);
            m_GameOverPanel.SetActive(false);
            m_NextLevelPanel.SetActive(false);

            m_DeathSound = GameObject.FindGameObjectWithTag("DeathSound").GetComponent<SoundManager>();
            m_GameOverSound = GameObject.FindGameObjectWithTag("GameOverSound").GetComponent<SoundManager>();
        }
	}
	
	
	void Update ()
    {
        if(RestartGamePanel)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }

        if(NextLevelPanel)
        {
            if(Input.GetKeyDown(KeyCode.N))
            {
                NextLevel();
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                ResumeGame();
            }
            else if(!GameIsPaused)
            {
                PauseGame();
            }
        }
    }

    public void SumarPuntos(int punts)
    {

        m_CurrentScore += punts;
        m_textScore.text = "Score:" + (m_CurrentScore + m_score);
        CurrentBrickNumber++;

        if(CurrentBrickNumber >= BrickNumber)
        {
            m_NextLevelPanel.SetActive(true);
            NextLevelPanel = true;
            m_Ball.movementStarted = false;
            m_Ball.speed = 0;
            m_Ship.ShipSpeed = 0;
        }
    }

    public void RestarVidas()
    {
        m_lifes--;
        m_textLifes.text = "Lifes:" + m_lifes;

        m_DeathSound.m_AS.clip = m_DeathSound.m_DeathSound;
        m_DeathSound.m_AS.Play();
        
        if (m_lifes <= 0)
        {
            m_GameOverSound.m_AS.clip = m_GameOverSound.m_GameOverSound;
            m_GameOverSound.m_AS.Play();
            m_Ball.movementStarted = false;
            m_Ball.speed = 0;
            m_Ship.ShipSpeed = 0;
            GameObject.FindGameObjectWithTag("Telon").GetComponent<Animator>().SetTrigger("Transition");
            StartCoroutine(TelonWaitGameOver());
        }

    }

    //public 

    public void HealthUp()
    {
        if(m_lifes<5)
        {
            m_lifes += 1;
            m_textLifes.text = "Lifes:" + m_lifes;
        }
    }

    public void RestartGame()
    {
        m_CurrentScore = 0;
        m_lifes = 5;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator TelonWaitGameOver()
    {
        yield return new WaitForSeconds(1.6f);
        m_GameOverPanel.SetActive(true);
        RestartGamePanel = true;
    }

    public void NextLevel()
    {
        m_lifes++;
        m_score +=m_CurrentScore;
        GameObject.FindGameObjectWithTag("Telon").GetComponent<Animator>().SetTrigger("Transition");
        StartCoroutine(TelonWait());
    }

    public IEnumerator TelonWait()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }

    public void ResumeGame()
    {
        m_PausePanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void PauseGame()
    {
        m_PausePanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        m_lifes = 3;
        m_score = 0;
        m_CurrentScore = 0;
    }



}
