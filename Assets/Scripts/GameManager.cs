using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Text m_textScore;
    [HideInInspector] public int m_score;

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
}
