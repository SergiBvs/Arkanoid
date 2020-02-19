using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    Text m_textScore;
    Text m_textLifes;
    GameObject m_NextLevelPanel;
    GameObject m_GameOverPanel;
    [HideInInspector] static public int m_score;
    [HideInInspector] static public int m_lifes = 3;
    public int m_CurrentScore;

    public int BrickNumber;
    int CurrentBrickNumber = 0;

    public bool isOut = true;

    void Start ()
    {
        m_textScore = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        m_textLifes = GameObject.FindGameObjectWithTag("Lifes").GetComponent<Text>();

        m_textScore.text = "Score:" + m_score;
        
        m_textLifes.text = "Lifes:" + m_lifes;
        m_NextLevelPanel = GameObject.FindGameObjectWithTag("NextLevelPanel");
        m_GameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");
        m_GameOverPanel.SetActive(false);
        m_NextLevelPanel.SetActive(false);
	}
	
	
	void Update () {

       

    }

    public void SumarPuntos(int punts)
    {
        m_score += punts;
        m_textScore.text = "Score:" + m_score;
        CurrentBrickNumber++;

        if(CurrentBrickNumber >= BrickNumber)
        {
            m_NextLevelPanel.SetActive(true);
        }
    }

    public void RestarVidas()
    {
        m_lifes--;
        m_textLifes.text = "Lifes:" + m_lifes;
        

        if (m_lifes <= 0)
        {
            GameObject.FindGameObjectWithTag("Telon").GetComponent<Animator>().SetTrigger("Transition");
            StartCoroutine(TelonWaitGameOver());
            
        }

    }

    //public 

    public void HealthUp()
    {
        if(m_lifes<3)
        {
            m_lifes += 1;
            m_textLifes.text = "Lifes:" + m_lifes;
        }
    }

    public void RestartGame()
    {
        m_lifes = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public IEnumerator TelonWaitGameOver()
    {
        yield return new WaitForSeconds(1.6f);
        m_GameOverPanel.SetActive(true);
    }

    public void NextLevel()
    {
        GameObject.FindGameObjectWithTag("Telon").GetComponent<Animator>().SetTrigger("Transition");
        StartCoroutine(TelonWait());

    }

    public IEnumerator TelonWait()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



}
