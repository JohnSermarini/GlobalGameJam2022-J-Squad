using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticles : MonoBehaviour
{
    public float duration = 0.5f;
    private bool bDestroyThis = false;

    private PhotonView photonView;
    private ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        particles = GetComponent<ParticleSystem>();

        // Set rotation to default
        transform.rotation = Quaternion.identity;
        transform.rotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));

        // Set duration to variable
        if(particles.isPlaying)
            particles.Stop();
        var main = particles.main;
        main.duration = duration;
        particles.Play();

        if(photonView.IsMine)
            StartCoroutine(DestroyCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine && bDestroyThis)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    public IEnumerator DestroyCountdown()
    {
        yield return new WaitForSeconds(duration);
        bDestroyThis = true;
        yield return null;
    }
}
