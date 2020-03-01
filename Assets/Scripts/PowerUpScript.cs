using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour {

    public enum PowerUpType { DoubleSize, SlowBall, SteelBall, GlueShip, MultiplyBall, InvertControls, HealthUp }

    public PowerUpType powerupType;

    private GameObject m_ship;
    private GameObject m_Ball;
    private GameObject m_GameManager;
    private float m_speed = 2f;

    private SoundManager m_PowerUpSound;


	// Use this for initialization
	void Start () {
        m_ship = GameObject.FindGameObjectWithTag("Player");
        m_Ball = GameObject.FindGameObjectWithTag("Ball");
        m_GameManager = GameObject.FindGameObjectWithTag("GameController");
        m_PowerUpSound = GameObject.FindGameObjectWithTag("PowerUpSound").GetComponent<SoundManager>();
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position += Vector3.down * Time.deltaTime * m_speed;

        if(IntersectBounds(this.GetComponent<SpriteRenderer>(), m_ship.GetComponent<SpriteRenderer>()))
        {
            m_PowerUpSound.m_AS.clip = m_PowerUpSound.m_PowerUpSound;
            m_PowerUpSound.m_AS.Play();

            switch (powerupType)
            {
                case PowerUpType.DoubleSize:
                    m_ship.GetComponent<Ship>().DoubleSize();
                    DestroyPwrup();
                    print("Double Size");
                    break;
                case PowerUpType.GlueShip:
                    m_Ball.GetComponent<Ball>().GlueBall();
                    DestroyPwrup();
                    print("GlueShip");
                    break;
                case PowerUpType.MultiplyBall:
                    m_Ball.GetComponent<Ball>().TripleBall();
                    DestroyPwrup();
                    break;
                case PowerUpType.SlowBall:
                    m_Ball.GetComponent<Ball>().SlowBall();
                    DestroyPwrup();
                    print("SlowBall");
                    break;
                case PowerUpType.SteelBall:
                    m_Ball.GetComponent<Ball>().SteelBall();
                    DestroyPwrup();
                    print("SteelBall");
                    break;
                case PowerUpType.InvertControls:
                    m_ship.GetComponent<Ship>().InvertControls();
                    DestroyPwrup();
                    print("InvertControls");
                    break;
                case PowerUpType.HealthUp:
                    m_GameManager.GetComponent<GameManager>().HealthUp();
                    DestroyPwrup();
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

    void DestroyPwrup()
    {
        GameObject particles = (GameObject)Resources.Load("BrickParticles");
        ParticleSystem.MainModule pamain = particles.GetComponent<ParticleSystem>().main;
        pamain.startColor = new Color(14/255f, 236/255f, 231/255f);

        Instantiate(particles, this.transform.position, Quaternion.Euler(-90,0,0));

        Destroy(this.gameObject);
        
    }
}
