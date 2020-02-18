using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public int x = 0;
    public int y = 0;

    public float speed;

    private SpriteRenderer m_NaveRenderer;
    private GameObject m_Nave;

    private SpriteRenderer m_TopWall;
    private SpriteRenderer m_LeftWall;
    private SpriteRenderer m_RightWall;
    private SpriteRenderer m_sr;

    private GameManager m_GameManager;

    public bool movementStarted = false;

    //POWERUPS
    private bool glueBall = false;
    private bool steelActivator = false;
    public bool steelBall = false;
    public GameObject[] ballClones; //Up Right Left
    private bool tripleActivator=false;
    public bool tripleBall = false;
    public bool isThisAClone;

    // Use this for initialization
    void Start () {

        m_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        m_sr = this.GetComponent<SpriteRenderer>();

        m_TopWall = GameObject.FindGameObjectWithTag("TopWall").GetComponent<SpriteRenderer>();
        m_LeftWall = GameObject.FindGameObjectWithTag("LeftWall").GetComponent<SpriteRenderer>();
        m_RightWall = GameObject.FindGameObjectWithTag("RightWall").GetComponent<SpriteRenderer>();

        m_Nave = GameObject.FindGameObjectWithTag("Player");
        m_NaveRenderer = m_Nave.GetComponent<SpriteRenderer>();


       // this.transform.position = new Vector3(m_Nave.transform.position.x, m_Nave.transform.position.y + m_Nave.transform.localScale.y / 2.5f, 0);
    }
	
	// Update is called once per frame
	void Update () {

        if (movementStarted)
        {
            speed += 0.0001f;
            if (IntersectBounds(m_sr, m_NaveRenderer))
            {
                if(!glueBall) BounceFromShip();
                else if (glueBall)
                {
                    glueBall = false;
                    movementStarted = false;
                }

                if (steelActivator)
                {
                    steelBall = true;
                    steelActivator = false;
                    StartCoroutine(SteelBallTime());
                }
            }
            else if (IntersectBounds(m_sr, m_TopWall))
            {
                if (steelBall) steelBall = false;
                y = -1;
            }
            else if (IntersectBounds(m_sr, m_LeftWall))
            {
                if (steelBall) steelBall = false;
                x = 1;
            }
            else if (IntersectBounds(m_sr, m_RightWall))
            {
                if (steelBall) steelBall = false;
                x = -1;
            }
            else if (m_sr.bounds.max.y < m_NaveRenderer.bounds.min.y) // Por debajo de la nave
            {
                if (steelBall) steelBall = false;
                if (!isThisAClone)
                {
                    this.transform.position = new Vector3(m_Nave.transform.position.x, m_Nave.transform.position.y + m_Nave.transform.localScale.y / 2.5f, 0);
                    movementStarted = false;
                    m_GameManager.RestarVidas();
                }
                else
                {
                    Destroy(this.gameObject);
                }
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
            if (tripleActivator)
            {
                Instantiate(ballClones[0], new Vector3(this.transform.position.x, this.transform.position.y + 0.1f, 0), Quaternion.identity);
                Instantiate(ballClones[1], new Vector3(this.transform.position.x, this.transform.position.y + 0.1f, 0), Quaternion.identity);
                tripleActivator = false;
            }
        }
        else if (l_Distance2 < (m_Nave.transform.localScale.x / 3))
        {
            x = 1;
            y = 1;
            if (tripleActivator)
            {
                Instantiate(ballClones[1], new Vector3(this.transform.position.x, this.transform.position.y + 0.1f, 0), Quaternion.identity);
                Instantiate(ballClones[0], new Vector3(this.transform.position.x, this.transform.position.y + 0.1f, 0), Quaternion.identity);
                tripleActivator = false;
            }
        }
        else
        {
            x = 0;
            y = 1;
            if (tripleActivator)
            {
                Instantiate(ballClones[1], new Vector3(this.transform.position.x, this.transform.position.y + 0.1f, 0), Quaternion.identity);
                Instantiate(ballClones[2], new Vector3(this.transform.position.x, this.transform.position.y + 0.1f, 0), Quaternion.identity);
                tripleActivator = false;
            }
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

    public void SteelBall()
    {
        steelActivator = true;
    }

    public void TripleBall()
    {
        if(!isThisAClone) tripleActivator = true;
    }

    public IEnumerator SteelBallTime()
    {
        yield return new WaitForSeconds(2);
        steelBall = false;
    }
}
