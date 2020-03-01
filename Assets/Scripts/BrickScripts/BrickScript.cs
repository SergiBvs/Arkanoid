using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour {

    public enum Brick_Type { normal, steel, desert,   chest, future, dimensional, corrupted, obsidian }
    public Brick_Type brickType;

    

    //----For Steel Brick----//
    public Sprite m_BrokenBrick;

    //----For CorruptedBrick----//
    public GameObject m_CorruptionObject;
    public GameObject[] m_NearbyBricks;

    //----For Chest Brick----//
    public GameObject[] ChestPowerUps;

    //----For Mimic Brick----//
    public GameObject MimicPowerUps;

    //----For Desert Brick----//
    private bool sandFalling = false;
    public float sandSpeed;

    //----For Dimensional Brick----//
    public GameObject m_connectedPortal;
    public Vector2 newDirection;
    public bool needsNewDirectionX = false;
    public bool needsNewDirectionY = false;

    //----For Future Brick----/
    public Sprite[] sprites; // IMPORTANTE SEGUIR ORDEN: NORMAL, HIERRO, ARENA, COFRE.
   

    //----For All Bricks----//
    private GameObject m_Ball;
    public SpriteRenderer m_BallCloneLeft;
    public SpriteRenderer m_BallCloneRight;
    public SpriteRenderer m_BallCloneUp;
    private SpriteRenderer m_BallRenderer;
    private SpriteRenderer m_Brick;
    private GameManager m_GameManager;
    public bool m_IsCorrupted = false;

    private float r;
    private float g;
    private float b;

    private Ball ballScript;

    // sound stuff

    private SoundManager m_BrickBounce;
    private SoundManager m_PortalSound;


    public int m_brickHealth;

    private bool isHitting = false;

	// Use this for initialization
	void Start ()
    {
        m_BrickBounce = GameObject.FindGameObjectWithTag("BrickBounce").GetComponent<SoundManager>();
        m_PortalSound = GameObject.FindGameObjectWithTag("PortalSound").GetComponent<SoundManager>();

        m_Ball = GameObject.FindGameObjectWithTag("Ball");
        m_BallRenderer = m_Ball.GetComponent<SpriteRenderer>();
        m_Brick = this.GetComponent<SpriteRenderer>();

        r = m_Brick.color.r;
        g = m_Brick.color.g;
        b = m_Brick.color.b;
       
        m_BallCloneLeft = GameObject.FindGameObjectWithTag("BallCloneLeft").GetComponent<SpriteRenderer>();
        m_BallCloneRight = GameObject.FindGameObjectWithTag("BallCloneRight").GetComponent<SpriteRenderer>();
        m_BallCloneUp = GameObject.FindGameObjectWithTag("BallCloneUp").GetComponent<SpriteRenderer>();

        ballScript = m_Ball.GetComponent<Ball>();
        ballScript.BrickColisionCD = true;
        m_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        
        if(this.brickType == Brick_Type.future)
        {
            StartCoroutine(FutureBrickUpdater());
        }
	}
	
	
   

	void Update ()
    {
		if(IntersectBounds(this.GetComponent<SpriteRenderer>(), m_BallRenderer))//cooldown
        {

            ballScript.StartCoroutine(ballScript.brickCollisionCD());

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
                    case Brick_Type.obsidian:
                        ObsidianBehaviour();
                        break;
                }

            }
        }
        else if ((IntersectBounds(this.GetComponent<SpriteRenderer>(), m_BallCloneUp)) && (ballScript.BrickColisionCD == true)) //cooldown
        {
            ballScript.StartCoroutine(ballScript.brickCollisionCD());

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
                    case Brick_Type.obsidian:
                        ObsidianBehaviour();
                        break;
                }

            }
        }
        else if ((IntersectBounds(this.GetComponent<SpriteRenderer>(), m_BallCloneLeft)) && (ballScript.BrickColisionCD == true)) //cooldown
        {
            ballScript.StartCoroutine(ballScript.brickCollisionCD());

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
                    case Brick_Type.obsidian:
                        ObsidianBehaviour();
                        break;
                }

            }
        }
        else if ((IntersectBounds(this.GetComponent<SpriteRenderer>(), m_BallCloneRight)) && (ballScript.BrickColisionCD == true)) // cooldown 
        {
            if (!isHitting) //para controlar que no pase mas de una vez en el golpe.
            {
                ballScript.StartCoroutine(ballScript.brickCollisionCD());

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
            if (transform.position.y <= -5.51f)
            {
                Destroy(this.gameObject);
            }
        }
        
    }

    public void Bounce(SpriteRenderer b, SpriteRenderer k, SpriteRenderer bCloneLeft, SpriteRenderer bCloneRight, SpriteRenderer bCloneUp)
    {
        //Como ya hemos detectado que esta colisionando no hace falta volverlo a detectar sino mirar en que posicion estaba la pelota cuando lo ha tocado.
        if (!ballScript.steelBall)
        {
            if (b.transform.position.x < k.transform.position.x - k.bounds.size.x / 2)
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
            else if (b.transform.position.y > k.transform.position.y + k.bounds.size.y / 2)
            {
                ballScript.y = 1;
            }
            else
            {
                ballScript.y = -1;
            }
        }

        if ((bCloneLeft.transform.position.x < k.transform.position.x - k.bounds.size.x / 2))
        {
            bCloneLeft.GetComponent<Ball>().x = -1;
        }
        else if (bCloneLeft.transform.position.x > k.transform.position.x + k.bounds.size.x / 2)
        {
            bCloneLeft.GetComponent<Ball>().x = 1;
        }
        else if (bCloneLeft.transform.position.y < k.transform.position.y - k.bounds.size.y / 2)
        {
            bCloneLeft.GetComponent<Ball>().y = -1;
        }
        else if (bCloneLeft.transform.position.y > k.transform.position.y + k.bounds.size.y / 2)
        {
            bCloneLeft.GetComponent<Ball>().y = 1;
        }
        else
        {
            bCloneLeft.GetComponent<Ball>().y = -1;
        }


        if ((bCloneRight.transform.position.x < k.transform.position.x - k.bounds.size.x / 2))
        {
            bCloneRight.GetComponent<Ball>().x = -1;
        }
        else if (bCloneRight.transform.position.x > k.transform.position.x + k.bounds.size.x / 2)
        {
            bCloneRight.GetComponent<Ball>().x = 1;
        }
        else if (bCloneRight.transform.position.y < k.transform.position.y - k.bounds.size.y / 2)
        {
            bCloneRight.GetComponent<Ball>().y = -1;
        }
        else if (bCloneRight.transform.position.y > k.transform.position.y + k.bounds.size.y / 2)
        {
            bCloneRight.GetComponent<Ball>().y = 1;
        }
        else
        {
            bCloneRight.GetComponent<Ball>().y = -1;
        }

        if ((bCloneUp.transform.position.x < k.transform.position.x - k.bounds.size.x / 2))
        {
            bCloneUp.GetComponent<Ball>().x = -1;
        }
        else if (bCloneUp.transform.position.x > k.transform.position.x + k.bounds.size.x / 2)
        {
            bCloneUp.GetComponent<Ball>().x = 1;
        }
        else if (bCloneUp.transform.position.y < k.transform.position.y - k.bounds.size.y / 2)
        {
            bCloneUp.GetComponent<Ball>().y = -1;
        }
        else if (bCloneUp.transform.position.y > k.transform.position.y + k.bounds.size.y / 2)
        {
            bCloneUp.GetComponent<Ball>().y = 1;
        }
        else
        {
            bCloneUp.GetComponent<Ball>().y = -1;
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
        Bounce(m_BallRenderer, m_Brick, m_BallCloneLeft, m_BallCloneRight, m_BallCloneUp);

        if(ballScript.BrickColisionCD)
        {
            m_BrickBounce.m_AS.clip = m_BrickBounce.m_BrickBounce;
            m_BrickBounce.m_AS.Play();

            ballScript.BrickColisionCD = false;
            
            HealthCheck();
            if (m_brickHealth <= 0)
            {
                m_GameManager.SumarPuntos(10);
                DestroyBrick();
            }
        }
    }

    public void SteelBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick, m_BallCloneLeft, m_BallCloneRight, m_BallCloneUp);

        if (ballScript.BrickColisionCD)
        {
            m_BrickBounce.m_AS.clip = m_BrickBounce.m_BrickBounce;
            m_BrickBounce.m_AS.Play();

            ballScript.BrickColisionCD = false;

            HealthCheck();
            if(m_brickHealth == 1)
            {
                m_Brick.sprite = m_BrokenBrick;
            }
            else if (m_brickHealth <= 0)
            {
                m_GameManager.SumarPuntos(10);
                DestroyBrick();
            }
        }
    }

    public void DesertBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick, m_BallCloneLeft, m_BallCloneRight, m_BallCloneUp);

        if (ballScript.BrickColisionCD)
        {
            m_BrickBounce.m_AS.clip = m_BrickBounce.m_BrickBounce;
            m_BrickBounce.m_AS.Play();

            ballScript.BrickColisionCD = false;

            HealthCheck();       
            if(m_brickHealth <= 0 && !sandFalling)
            {
                sandFalling = true;
                m_GameManager.SumarPuntos(10);
            }
        }
    }

    public void FutureBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick, m_BallCloneLeft, m_BallCloneRight, m_BallCloneUp);
        if (ballScript.BrickColisionCD)
        {
            m_BrickBounce.m_AS.clip = m_BrickBounce.m_BrickBounce;
            m_BrickBounce.m_AS.Play();

            ballScript.BrickColisionCD = false;

            HealthCheck();
            if (m_brickHealth <= 0)
            {
                m_GameManager.SumarPuntos(10);
                DestroyBrick();
            }
        }
    }

    public IEnumerator FutureBrickUpdater()
    {
        yield return new WaitForSeconds(7);
        int rand = Random.Range(0, 4);
        this.brickType = (Brick_Type)rand;
        this.GetComponent<SpriteRenderer>().sprite = sprites[rand];

        if (this.brickType == Brick_Type.steel) m_brickHealth = 2;
        else m_brickHealth = 1;

        StartCoroutine(FutureBrickUpdater());
    }

    public void CorruptedBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick, m_BallCloneLeft, m_BallCloneRight, m_BallCloneUp);

        if (ballScript.BrickColisionCD)
        {
            m_BrickBounce.m_AS.clip = m_BrickBounce.m_BrickBounce;
            m_BrickBounce.m_AS.Play();

            ballScript.BrickColisionCD = false;

            m_brickHealth--;
            if (m_brickHealth == 1)
            {
                m_CorruptionObject.SetActive(false);

                int rand = Random.Range(0, m_NearbyBricks.Length);
                for (int i = 0; i <= rand; i++)
                {
                    int rand2 = Random.Range(0, m_NearbyBricks.Length);
                    if (m_NearbyBricks[rand2] != null)
                    {
                        m_NearbyBricks[rand2].GetComponent<BrickScript>().m_CorruptionObject.SetActive(true);
                        m_NearbyBricks[rand2].GetComponent<BrickScript>().m_IsCorrupted = true;
                    }
                }
            }
            else if (m_brickHealth <= 0)
            {
                m_GameManager.SumarPuntos(10);
                DestroyBrick();
            }
        }
    }

    public void DimBehaviour()
    {
        //Bounce(m_BallRenderer, m_Brick);
        if (m_GameManager.isOut)
        {
            m_PortalSound.m_AS.clip = m_PortalSound.m_PortalSound;
            m_PortalSound.m_AS.Play();
            m_GameManager.isOut = false;

            m_Ball.transform.position = m_connectedPortal.transform.position;
            if (needsNewDirectionX)
            {
                m_Ball.GetComponent<Ball>().x = (int)newDirection.x;
            }
            if (needsNewDirectionY)
            {
                m_Ball.GetComponent<Ball>().y = (int)newDirection.y;
            }
            
            StartCoroutine(DimensionalCooldown());
        }
    }

    public void ChestBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick, m_BallCloneLeft, m_BallCloneRight, m_BallCloneUp);
        if (ballScript.BrickColisionCD)
        {
            m_BrickBounce.m_AS.clip = m_BrickBounce.m_BrickBounce;
            m_BrickBounce.m_AS.Play();

            ballScript.BrickColisionCD = false;

            Instantiate(ChestPowerUps[Random.Range(0,8)], this.transform.position, Quaternion.identity);
            m_GameManager.SumarPuntos(10);
            DestroyBrick();
        }
    }

    public void ObsidianBehaviour()
    {
        Bounce(m_BallRenderer, m_Brick, m_BallCloneLeft, m_BallCloneRight, m_BallCloneUp);
    }

    IEnumerator DimensionalCooldown()
    {
        yield return new WaitForSeconds(0.3f);
        m_GameManager.isOut = true;
    }

    void DestroyBrick()
    {
        GameObject brickParticles = (GameObject)Resources.Load("BrickParticles");
        ParticleSystem ps = brickParticles.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = ps.main;
        float rand = Random.Range(0, 0.5f);
        main.startColor = new ParticleSystem.MinMaxGradient(new Color (r-rand,g-rand,b-rand), new Color(r+rand,g+rand,b+rand));
        Instantiate(brickParticles, this.transform.position, Quaternion.Euler(-90,0,0));
        Destroy(this.gameObject);
    }
}
