using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bumtiBumti : MonoBehaviour
{
    public AudioClip[] music;
    [SerializeField] AudioSource source;

    [SerializeField] int i;


    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            i = Random.Range(0, music.Length);
            source.PlayOneShot(music[i]);
        }
    }
}
