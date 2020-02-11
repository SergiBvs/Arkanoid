using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    public int ShipSpeed;

	void Start ()
    {
		
	}
	
	
	void Update ()
    {
		if(Input.GetAxisRaw("Horizontal") > 0 )//move right
        {
            this.transform.position += Vector3.right * Time.deltaTime * ShipSpeed;
        }

        if(Input.GetAxisRaw("Horizontal")<0)// move left
        {
            this.transform.position += Vector3.left * Time.deltaTime * ShipSpeed;
        }
	}
}
