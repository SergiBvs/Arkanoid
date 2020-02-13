﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour {

    public enum Brick_Type { normal, steel, desert, future, dimensional, corrupted, chest, mimic, obsidian}
    public Brick_Type brickType;

    //----For Steel Brick----//
    public Sprite m_BrokenBrick;

    //----For CorruptedBrick----//
    public GameObject m_CorruptionObject;
    public GameObject[] m_NearbyBricks;

    //----For Chest Brick----//
    public GameObject ChestPowerUps;

    //----For Mimic Brick----//
    public GameObject MimicPowerUps;

    //----For Desert Brick----//
    private bool sandFalling = false;
    public float sandSpeed;

    //----For Dimensional Brick----//
    public GameObject m_connectedPortal;
    bool isOut = true;

    //----For Future Brick----/
    private GameObject[] bricks;

    //----For All Bricks----//
    private GameObject m_Ball;
    private SpriteRenderer m_BallRenderer;
    private SpriteRenderer m_Brick;
    private GameManager m_GameManager;
    public bool m_IsCorrupted = false;

    private Ball ballScript;


    public int m_brickHealth;

    private bool isHitting = false;

	// Use this for initialization
	void Start () {
        m_Ball = GameObject.FindGameObjectWithTag("Ball");
        m_BallRenderer = m_Ball.GetComponent<SpriteRenderer>();
        m_Brick = this.GetComponent<SpriteRenderer>();
        ballScript = m_Ball.GetComponent<Ball>();
        m_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        
	}
	
	// Update is called once per frame
	void Update () {
		if(IntersectBounds(this.GetComponent<SpriteRenderer>(), m_BallRenderer))
        {
            if (!isHitting) //para controlar que no pase mas de una vez en el golpe.
            {
                isHitting = true;
                switch (brickType)
                {
                    case Brick_Type.normal:
                        NormalBehaviour();
                        break;
                    case Brick_Type.steel:
                        SteelBehaviour();
                        break;
                    case Brick_Type.desert:
                        DesertBehaviour();
                        break;
                    case Brick_Type.future:
                        FutureBehaviour();
                        break;
                    case Brick_Type.corrupted:
                        CorruptedBehaviour();
                        break;
                    case Brick_Type.dimensional:
                        DimBehaviour();
                        break;
                    case Brick_Type.chest:
                        ChestBehaviour();
                        break;
                    case Brick_Type.mimic:
                        MimicBehaviour();
                        break;
                    case Brick_Type.obsidian:
                        ObsidianBehaviour();
                        break;
                }

            }
        }
        else
        {
            //Cuando la bola sale del brick
            isHitting = false;
        }

        if(sandFalling == true)
        {
            this.transform.position += Vector3.down * Time.deltaTime * sandSpeed;
        }
	}

    public void Bounce(SpriteRenderer b, SpriteRenderer k)
    {
       /*
        if ((b.bounds.max.x < k.bounds.min.x + 0.8f) && (Vector3.Distance(b.gameObject.transform.position, k.gameObject.transform.position - k.bounds.size/2) < b.bounds.size.x) 
            && (b.transform.position.y > k.bounds.min.y) && (b.transform.position.y < k.bounds.max.y)){

            ballScript.x = -1;
        }
        else if ((b.bounds.min.x > k.bounds.max.x - 0.8f) && (Vector3.Distance(b.gameObject.transform.position, k.gameObject.transform.position + k.bounds.size / 2) < b.bounds.size.x)
            && (b.transform.position.y > k.bounds.min.y) && (b.transform.position.y < k.bounds.max.y))
        {
            ballScript.x = 1;
        }
        else if ((b.bounds.max.y > k.bounds.min.y) && (b.bounds.min.y < k.bounds.max.y)
            && (b.bounds.min.x < k.bounds.max.x) && (b.bounds.max.x > k.bounds.min.x))
        {
            ballScript.y = -1;
        }

        else if ((b.bounds.min.y < k.bounds.max.y) && (b.bounds.max.y > k.bounds.min.y)
            && (b.bounds.min.x < k.bounds.max.x) && (b.bounds.max.x > k.bounds.min.x))
        {
            ballScript.y = 1;
        }*/

        //Como ya hemos detectado que esta colisionando no hace falta volverlo a detectar sino mirar en que posicion estaba la pelota cuando lo ha tocado.

        if(b.transform.position.x < k.transform.position.x - k.bounds.size.x / 2)
        {
            ballScript.x = -1;
        }
        else if (b.transform.position.x > k.transform.position.x + k.bounds.size.x / 2)
        {
            ballScript.x = 1;
        }
        else if (b.transform.position.y < k.transform.position.y - k.bounds.size.y / 2)
        {
            ballScript.y = -1;
        }
        else if (b.transform.position.y > k.transform.position.x + k.bounds.size.x / 2)
        {
            ballScript.y = 1;
        }
        else
        {
            ballScript.y = -1;
        }

    }

    public void HealthCheck()
    {
        if (this.m_IsCorrupted)
        {
            m_CorruptionObject.SetActive(false);
            m_IsCorrupted = false;
        }
        else
        {
            this.m_brickHealth--;
        }
    }

    public bool IntersectBounds(SpriteRenderer l_Ball, SpriteRenderer l_Nave)
    {
        return l_Ball.bounds.max.y > l_Nave.bounds.min.y
            && l_Ball.bounds.min.y < l_Nave.bounds.max.y
            && l_Ball.bounds.max.x > l_Nave.bounds.min.x
            && l_Ball.bounds.min.x < l_Nave.bounds.max.x;
    }


    public void NormalBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick);

        HealthCheck();
        if (m_brickHealth <= 0)
        {
            m_GameManager.SumarPuntos(10);
            Destroy(this.gameObject);
        }
    }

    public void SteelBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick);

        HealthCheck();
        if(m_brickHealth == 1)
        {
            m_Brick.sprite = m_BrokenBrick;
        }
        else if (m_brickHealth <= 0)
        {
            m_GameManager.SumarPuntos(10);
            Destroy(this.gameObject);
        }
    }

    public void DesertBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick);

        HealthCheck();       
        if(m_brickHealth <= 0)
        {
            sandFalling = true;
        }

        if(this.transform.position.y <= -5.51f)
        {
            m_GameManager.SumarPuntos(10);
            Destroy(this.gameObject);
        }

    }

    public void FutureBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick);
        HealthCheck();
        
        if (m_brickHealth <= 0)
        {
            m_GameManager.SumarPuntos(10);
            Destroy(this.gameObject);
        }
    }

    public void CorruptedBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick);

        m_brickHealth--;
        if (m_brickHealth == 1)
        {
            m_CorruptionObject.SetActive(false);

            int rand = Random.Range(0, m_NearbyBricks.Length);
            for (int i = 0; i <= rand; i++)
            {
                int rand2 = Random.Range(0, m_NearbyBricks.Length);
                m_NearbyBricks[rand2].GetComponent<BrickScript>().m_CorruptionObject.SetActive(true);
                m_NearbyBricks[rand2].GetComponent<BrickScript>().m_IsCorrupted = true;
            }
        }
        else if (m_brickHealth <= 0)
        {
            m_GameManager.SumarPuntos(10);
            Destroy(this.gameObject);
        }
    }

    public void DimBehaviour()
    {
        //Bounce(m_BallRenderer, m_Brick);
        if (m_GameManager.isOut)
        {
            m_GameManager.isOut = false;
           
            m_Ball.transform.position = m_connectedPortal.transform.position;
            StartCoroutine(DimensionalCooldown());
        }
    }

    public void ChestBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick);
        Instantiate(ChestPowerUps, this.transform.position, Quaternion.identity);
        Destroy(this.)
    }

    public void MimicBehaviour()
    { 
        Bounce(m_BallRenderer, m_Brick);
    }

    public void ObsidianBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick);
    }

    IEnumerator DimensionalCooldown()
    {
        yield return new WaitForSeconds(0.3f);
        m_GameManager.isOut = true;
    }

}
