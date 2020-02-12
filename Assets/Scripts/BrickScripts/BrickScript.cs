﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour {

    public enum Brick_Type { normal, steel, desert, future, dimensional, corrupted, chest, mimic, obsidian}

    public Brick_Type brickType;

    private GameObject m_Ball;
    private SpriteRenderer m_BallRenderer;
    private SpriteRenderer m_Brick;

    private Ball ballScript;

    public int m_brickHealth;

    private bool isHitting = false;

	// Use this for initialization
	void Start () {
        m_Ball = GameObject.FindGameObjectWithTag("Ball");
        m_BallRenderer = m_Ball.GetComponent<SpriteRenderer>();
        m_Brick = this.GetComponent<SpriteRenderer>();
        ballScript = m_Ball.GetComponent<Ball>();
        print("bhcsabchjsabhcbaj"+m_BallRenderer.bounds.size.x / 2);
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
	}

    public void Bounce(SpriteRenderer b, SpriteRenderer k)
    {
       
        if ((b.bounds.max.x < k.bounds.min.x + 0.1f) && (Vector3.Distance(b.gameObject.transform.position, k.gameObject.transform.position - k.bounds.size/2) < b.bounds.size.x) 
            && (b.transform.position.y > k.bounds.min.y) && (b.transform.position.y < k.bounds.max.y)){

            ballScript.x = -1;
        }
        else if ((b.bounds.min.x > k.bounds.max.x - 0.1f) && (Vector3.Distance(b.gameObject.transform.position, k.gameObject.transform.position + k.bounds.size / 2) < b.bounds.size.x)
            && (b.transform.position.y > k.bounds.min.y) && (b.transform.position.y < k.bounds.max.y))
        {
            ballScript.x = 1;
        }
        else if ((b.bounds.max.y > k.bounds.min.y) && (b.bounds.min.y < k.bounds.max.y)
            && (b.transform.position.x > k.bounds.min.x) && (b.transform.position.x < k.bounds.max.x))
        {
            ballScript.y = -1;
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
        m_brickHealth--;
        if (m_brickHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SteelBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick);

    }

    public void DesertBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick);

    }

    public void FutureBehaviour()
    {

    }

    public void CorruptedBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick);

    }

    public void DimBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick);

    }

    public void ChestBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick);

    }

    public void MimicBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick);

    }

    public void ObsidianBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick);

    }

}
