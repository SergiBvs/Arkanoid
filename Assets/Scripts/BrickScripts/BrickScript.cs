using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour {

    public enum Brick_Type { normal, steel, desert, future, dimensional, corrupted, chest, mimic}

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
                }

            }
        }
        else
        {
            //Cuando la bola sale del brick
            isHitting = false;
        }
	}

    public void Bounce()
    {

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
        Bounce();
        m_brickHealth--;
        if (m_brickHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SteelBehaviour()
    {

    }

    public void DesertBehaviour()
    {

    }

    public void FutureBehaviour()
    {

    }

    public void CorruptedBehaviour()
    {

    }

    public void DimBehaviour()
    {

    }

    public void ChestBehaviour()
    {

    }

    public void MimicBehaviour()
    {

    }



}
