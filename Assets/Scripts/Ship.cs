using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    public int ShipSpeed;
    public GameObject RightWall;
    public GameObject LeftWall;

    public bool InvertedControls = false;

    private float m_OriginalSizeX;
	void Start ()
    {
        m_OriginalSizeX = this.transform.localScale.x;
	}
	
	
	void Update ()
    {
        if(!InvertedControls)
        {
            if ((Input.GetAxisRaw("Horizontal") > 0) && (IntersectBoundsRight(GetComponent<SpriteRenderer>(), RightWall.GetComponent<SpriteRenderer>()) == false))//move right
            {
                this.transform.position += Vector3.right * Time.deltaTime * ShipSpeed;
            }
            if((Input.GetAxisRaw("Horizontal")<0) && (IntersectBoundsLeft(GetComponent<SpriteRenderer>(), LeftWall.GetComponent<SpriteRenderer>()) == false))// move left
            {
                this.transform.position += Vector3.left * Time.deltaTime * ShipSpeed;
            }
        }
        else if(InvertedControls)
        {
            if ((Input.GetAxisRaw("Horizontal") < 0) && (IntersectBoundsRight(GetComponent<SpriteRenderer>(), RightWall.GetComponent<SpriteRenderer>()) == false))//move right
            {
                this.transform.position += Vector3.right * Time.deltaTime * ShipSpeed;
            }
            if ((Input.GetAxisRaw("Horizontal") > 0) && (IntersectBoundsLeft(GetComponent<SpriteRenderer>(), LeftWall.GetComponent<SpriteRenderer>()) == false))// move left
            {
                this.transform.position += Vector3.left * Time.deltaTime * ShipSpeed;
            }
        }
	}

    public bool IntersectBoundsRight(SpriteRenderer l_RightWall, SpriteRenderer l_Ship)
    {
        return l_RightWall.bounds.max.x > l_Ship.bounds.min.x && l_RightWall.bounds.min.x < l_Ship.bounds.max.x;
    }

    public bool IntersectBoundsLeft(SpriteRenderer l_LeftWall, SpriteRenderer l_Ship)
    {
        return l_LeftWall.bounds.max.x > l_Ship.bounds.min.x && l_LeftWall.bounds.min.x < l_Ship.bounds.max.x;
    }

    public void DoubleSize()
    {
        this.transform.localScale += new Vector3(m_OriginalSizeX * 0.5f, 0, 0);
        StartCoroutine(PowerupTime());
    }

    public IEnumerator PowerupTime()
    {
        yield return new WaitForSeconds(5);
        this.transform.localScale -= new Vector3(m_OriginalSizeX * 0.5f, 0, 0);
    }

    public void InvertControls()
    {
        InvertedControls = true;
        StartCoroutine(InvertControlsTime());
    }

    public IEnumerator InvertControlsTime()
    {
        yield return new WaitForSeconds(4);
        InvertedControls = false;
    }

}
