using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionRandomColor : MonoBehaviour {

    public Vector3[] Colors;

	// Use this for initialization
	void Start () {

        int rand = Random.Range(0, Colors.Length);
        print(rand);
        Vector3 selectedColor = Colors[rand];
        Color color = new Color(selectedColor.x, selectedColor.y, selectedColor.z);
        this.GetComponent<SpriteRenderer>().color = color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
