using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    int x = 0;
    int y = 0;

    public float speed;

    public SpriteRenderer m_NaveRenderer;
    public GameObject m_Nave;

    public SpriteRenderer m_TopWall;
    public SpriteRenderer m_LeftWall;
    public SpriteRenderer m_RightWall;
    SpriteRenderer m_sr;

    // Use this for initialization
    void Start () {
        m_sr = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        
        if (IntersectBounds(m_sr, m_NaveRenderer))
        {
            BounceFromShip();
        }
        else if(IntersectBounds(m_sr, m_TopWall)){
            y = -1;
        }
        else if(IntersectBounds(m_sr, m_LeftWall))
        {
            x = 1;
        }
        else if(IntersectBounds(m_sr, m_RightWall))
        {
            x = -1;
        }

        transform.position += new Vector3(x, y, 0) * Time.deltaTime *speed;
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

        print(l_Distance + "  " + l_Distance2);
        if (l_Distance < ((m_Nave.transform.localScale.x / 3)))
        {
            print("LEFT");
            x = -1;
            y = 1;
        }
        else if (l_Distance2 < (m_Nave.transform.localScale.x / 3))
        {
            print("RIGHT");
            x = 1;
            y = 1;
        }
        else
        {
            print("CENTER");
            x = 0;
            y = 1;
        }
    }

}
