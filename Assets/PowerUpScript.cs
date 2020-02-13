using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour {

    public enum PowerUpType { DoubleSize, SlowBall, SteelBall, GlueShip, MultiplyBall}


    public float speed = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += Vector3.down * Time.deltaTime * speed;
	}
}
