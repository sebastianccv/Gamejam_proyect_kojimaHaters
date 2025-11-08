using System.Collections;
using UnityEngine;

public class Sondo : MonoBehaviour
{
    public AudioSource quienEmite;
    public AudioClip eIArchiv0QueBaje;
    public float volumen = 2;
    private bool sonidoReproduciendose = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!sonidoReproduciendose)
        {
            sonidoReproduciendose = true;
            quienEmite.PlayOneShot(eIArchiv0QueBaje, volumen);
            StartCoroutine(ResetSonidoPlaying());
        }
    }

    private IEnumerator ResetSonidoPlaying()
    {
        yield return new WaitForSeconds(eIArchiv0QueBaje.length);
        sonidoReproduciendose = false;
    }
}
