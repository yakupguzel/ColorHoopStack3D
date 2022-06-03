using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource gameMusic;
    [SerializeField] AudioSource backtoSocket;
    [SerializeField] AudioSource gotoNext;


    public void BackToSocket()
    {
        backtoSocket.Stop();
        backtoSocket.Play();
    }

    public void GoToNext()
    {
        gotoNext.Stop();
        gotoNext.Play();
    }
}
