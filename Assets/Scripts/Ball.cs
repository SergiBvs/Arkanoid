using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour {

    [HideInInspector]public int x = 0;
    [HideInInspector]public int y = 0;

    public float speed;

    public SpriteRenderer m_NaveRenderer;
    public GameObject m_Nave;

    public SpriteRenderer m_TopWall;
    public SpriteRenderer m_LeftWall;
    public SpriteRenderer m_RightWall;
    private SpriteRenderer m_sr;

    private GameManager m_GameManager;

    [HideInInspector] public bool movementStarted = false;
    private bool glueBall = false;

    // Use this for initialization
    void Start () {

        m_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        m_sr = this.GetComponent<SpriteRenderer>();
        this.transform.position = new Vector3(m_Nave.transform.position.x, m_Nave.transform.position.y + m_Nave.transform.localScale.y/2.5f, 0);
        movementStarted = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (movementStarted)
        {
            if (IntersectBounds(m_sr, m_NaveRenderer))
            {
                if(!glueBall) BounceFromShip();
                else
                {
                    glueBall = false;
                    movementStarted = false;
                }
            }
            else if (IntersectBounds(m_sr, m_TopWall))
            {
                y = -1;
            }
            else if (IntersectBounds(m_sr, m_LeftWall))
            {
                x = 1;
            }
            else if (IntersectBounds(m_sr, m_RightWall))
            {
                x = -1;
            }
            else if (m_sr.bounds.max.y < m_NaveRenderer.bounds.min.y) // Por debajo de la nave
            {
                this.transform.position = new Vector3(m_Nave.transform.position.x, m_Nave.transform.position.y + m_Nave.transform.localScale.y / 2.5f, 0);
                movementStarted = false;
                m_GameManager.RestarVidas();
            }

            transform.position += new Vector3(x, y, 0) * Time.deltaTime * speed;
        } 
        else
        {
            this.transform.position = new Vector3(m_Nave.transform.position.x, m_Nave.transform.position.y + m_Nave.transform.localScale.y / 2.5f, 0);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                movementStarted = true;
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

    public void BounceFromShip()
    {
        Vector3 topLeft = new Vector3(m_NaveRenderer.bounds.min.x, m_NaveRenderer.GetComponent<SpriteRenderer>().bounds.max.y); //Esquina Izquierda Superior Nave
        Vector3 topRight = new Vector3(m_NaveRenderer.bounds.max.x, m_NaveRenderer.GetComponent<SpriteRenderer>().bounds.max.y); //Esquina Derecha Superior Nave

        float l_Distance = Vector3.Distance(this.GetComponent<SpriteRenderer>().bounds.center, topLeft);
        float l_Distance2 = Vector3.Distance(this.GetComponent<SpriteRenderer>().bounds.center, topRight);

        
        if (l_Distance < ((m_Nave.transform.localScale.x / 3)))
        {
            x = -1;
            y = 1;
        }
        else if (l_Distance2 < (m_Nave.transform.localScale.x / 3))
        {
            x = 1;
            y = 1;
        }
        else
        {
            x = 0;
            y = 1;
        }
    }


    //POWERUP
    public void GlueBall()
    {
        glueBall = true;
    }

    //POWERUP
    public void SlowBall()
    {
        speed -= 3;
        StartCoroutine(SlowBallTime());
    }

    public IEnumerator SlowBallTime()
    {
        yield return new WaitForSeconds(7);
        speed += 3;
    }

}
