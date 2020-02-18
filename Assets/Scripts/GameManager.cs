using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    Text m_textScore;
    Text m_textLifes;
    [HideInInspector] public int m_score;
    [HideInInspector] public int m_lifes;
    
    public bool isOut = true;

    void Start ()
    {
        m_textScore = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        m_textLifes = GameObject.FindGameObjectWithTag("Lifes").GetComponent<Text>();

        m_textScore.text = "Score:" + m_score;
        m_lifes = 3;
        m_textLifes.text = "Lifes:" + m_lifes;
	}
	
	
	void Update () {

       

    }

    public void SumarPuntos(int punts)
    {
        m_score += punts;
        m_textScore.text = "Score:" + m_score;
    }

    public void RestarVidas()
    {
        m_lifes--;
        m_textLifes.text = "Lifes:" + m_lifes;
        

        if (m_lifes <= 0)
        {
            RestartGame();
        }

    }

    public void HealthUp()
    {
        if(m_lifes<3)
        {
            m_lifes += 1;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
