using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    ParticleSystem Cap;
    private void Start()
    {
        Cap = GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ParticleWait());
        }
    }
    public IEnumerator ParticleWait()
    {
        yield return new WaitForSeconds(0.3f);
        Cap.Play();
        yield return new WaitForSeconds(0.6f);
        Cap.Stop();
    }

}
