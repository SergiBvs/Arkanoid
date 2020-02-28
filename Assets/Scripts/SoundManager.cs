using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource m_AS;
    public AudioClip m_BrickBounce;
    public AudioClip m_WallBounce;
    public AudioClip m_DeathSound;
    public AudioClip m_PowerUpSound;
	
	void Start ()
    {
        m_AS = GetComponent<AudioSource>();
    }
	
	
	void Update ()
    {
		
	}
}
