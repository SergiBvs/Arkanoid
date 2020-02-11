using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    int x = 0;
    int y = 0;

    public SpriteRenderer m_Nave;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position += new Vector3(x, y, 0);
        if (IntersectBounds(this.gameObject.GetComponent<SpriteRenderer>(), m_Nave))
        {
            
            Vector3 topLeft = new Vector3(m_Nave.bounds.min.x, m_Nave.GetComponent<SpriteRenderer>().bounds.max.y);
            Vector3 topRight = new Vector3(m_Nave.bounds.max.x, m_Nave.GetComponent<SpriteRenderer>().bounds.max.y);
            float l_Distance = Vector3.Distance(this.GetComponent<SpriteRenderer>().bounds.center, topLeft);
            float l_Distance2 = Vector3.Distance(this.GetComponent<SpriteRenderer>().bounds.center, topRight);

            print(l_Distance + "  " + l_Distance2);
          

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
