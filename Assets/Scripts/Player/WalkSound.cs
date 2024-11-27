using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    /*
     * 플레이어가 이동할 때마다 발걸음 소리를 재생하는 스크립트입니다.
     */
    public GameObject audioSourceObject;
    private AudioSource audioSource;
    public AudioClip walkSounds;
    private int soundIndex = 0;
    
    private void Start()
    {
        audioSource = audioSourceObject.GetComponent<AudioSource>();
        // walkSounds = Resources.LoadAll<AudioClip>("Sounds/Walk");
        audioSource.clip = walkSounds;
    }
    
    private void PlayWalkSound()
    {
        audioSource.Play();
        
    }
    
    //플레이어가 바닥에 붙어있고 직전 위치에서 어느 이상 이동했을 때 소리 재생
    private void Update()
    {
        if(!audioSource.isPlaying && GetComponent<CharacterController>().velocity.magnitude > 0.1f && transform.GetComponent<PlayerMovement>().isGround == true)
        {
            PlayWalkSound();
        }
    }
}
