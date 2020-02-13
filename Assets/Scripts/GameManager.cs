using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Text m_textScore;
    public Text m_textLifes;
    [HideInInspector] public int m_score;
    [HideInInspector] public int m_lifes;
    
    public bool isOut = true;

    void Start () {
		
	}
	
	
	void Update () {

       

    }

    public void SumarPuntos(int punts)
    {
        m_score += punts;
        m_textScore.text = "Score:" + m_score;
    }

    public void RestarVidas(int vidas)
    {
        m_lifes--;

        if (m_lifes <= 0)
        {
            RestartGame();
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
