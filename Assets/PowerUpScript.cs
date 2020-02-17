﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour {

    public enum PowerUpType { DoubleSize, SlowBall, SteelBall, GlueShip, MultiplyBall}

    public PowerUpType powerupType;

    private GameObject m_ship;
    private GameObject m_Ball;
    public float m_speed = 2f;

   

	// Use this for initialization
	void Start () {
        m_ship = GameObject.FindGameObjectWithTag("Player");
        m_Ball = GameObject.FindGameObjectWithTag("Ball");
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
                    break;
                case PowerUpType.GlueShip:
                    m_Ball.GetComponent<Ball>().GlueBall();
                    Destroy(this.gameObject);
                    break;
                case PowerUpType.MultiplyBall:
                    break;
                case PowerUpType.SlowBall:
                    m_Ball.GetComponent<Ball>().SlowBall();
                    Destroy(this.gameObject);
                    break;
                case PowerUpType.SteelBall:
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
