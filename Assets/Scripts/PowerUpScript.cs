using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour {

    public enum PowerUpType { DoubleSize, SlowBall, SteelBall, GlueShip, MultiplyBall, InvertControls, HealthUp }

    public PowerUpType powerupType;

    private GameObject m_ship;
    private GameObject m_Ball;
    private GameObject m_GameManager;
    public float m_speed = 2f;

    public Sprite[] sprites;

   

	// Use this for initialization
	void Start () {
        m_ship = GameObject.FindGameObjectWithTag("Player");
        m_Ball = GameObject.FindGameObjectWithTag("Ball");
        m_GameManager = GameObject.FindGameObjectWithTag("GameController");

        int rand = Random.Range(0, 7);
        //this.powerupType = (PowerUpType)rand;
        this.powerupType = PowerUpType.GlueShip; // for testing purposes only
        this.GetComponent<SpriteRenderer>().sprite = sprites[rand];
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position += Vector3.down * Time.deltaTime * m_speed;

        if(IntersectBounds(this.GetComponent<SpriteRenderer>(), m_ship.GetComponent<SpriteRenderer>()))
        {
            switch (powerupType)
            {
                case PowerUpType.DoubleSize:
                    m_ship.GetComponent<Ship>().DoubleSize();
                    Destroy(this.gameObject);
                    print("Double Size");
                    break;
                case PowerUpType.GlueShip:
                    m_Ball.GetComponent<Ball>().GlueBall();
                    Destroy(this.gameObject);
                    print("GlueShip");
                    break;
                case PowerUpType.MultiplyBall:
                    m_Ball.GetComponent<Ball>().TripleBall();
                    Destroy(this.gameObject);
                    break;
                case PowerUpType.SlowBall:
                    m_Ball.GetComponent<Ball>().SlowBall();
                    Destroy(this.gameObject);
                    print("SlowBall");
                    break;
                case PowerUpType.SteelBall:
                    m_Ball.GetComponent<Ball>().SteelBall();
                    Destroy(this.gameObject);
                    print("SteelBall");
                    break;
                case PowerUpType.InvertControls:
                    m_ship.GetComponent<Ship>().InvertControls();
                    Destroy(this.gameObject);
                    print("InvertControls");
                    break;
                case PowerUpType.HealthUp:
                    m_GameManager.GetComponent<GameManager>().HealthUp();
                    Destroy(this.gameObject);
                    print("HealthUp");
                    break;

            }
        }
	}

    

    public bool IntersectBounds(SpriteRenderer l_Ball, SpriteRenderer l_Nave)
    {
        return l_Ball.bounds.max.y > l_Nave.bounds.min.y
            && l_Ball.bounds.min.y < l_Nave.bounds.max.y
            && l_Ball.bounds.max.x > l_Nave.bounds.min.x
            && l_Ball.bounds.min.x < l_Nave.bounds.max.x;

    }
}
