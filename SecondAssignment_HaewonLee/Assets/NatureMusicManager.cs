using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class NatureMusicManager : MonoBehaviour
{
    private AudioSource audioN;

    public Player player;
    // Start is called before the first frame update
    private void Awake()
    {
        audioN = GetComponent<AudioSource>();
    }
    void Start()
    {
        audioN.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isOver)
        {
            audioN.Stop();
        }
    }
}
