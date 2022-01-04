using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSoundController : MonoBehaviour
{
    public AudioClip wall;
    public AudioClip square;
    public AudioClip coin;
    public AudioClip shoot;
    public AudioClip freeze;

    private AudioSource audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    public void PlayWall()
    {
        audioPlayer.PlayOneShot(wall);
    }

    public void PlaySquare()
    {
        audioPlayer.PlayOneShot(square);
    }

    public void PlayCoin()
    {
        audioPlayer.PlayOneShot(coin);
    }

    public void PlayShoot()
    {
        audioPlayer.PlayOneShot(shoot);
    }

    public void PlayFreeze()
    {
        audioPlayer.PlayOneShot(freeze);
    }
}
