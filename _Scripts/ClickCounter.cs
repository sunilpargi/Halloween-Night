using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCounter : MonoBehaviour
{
    public int tapCount;
    public GameplayUI gameplayUI;
    public GameManager gameManager;

    public AudioClip[] ballonBlast;
    public AudioSource gameaudioSource, bgSound;
    public AudioClip pickUP;
    public GameObject[] particleEffct;
    public ParticleSystem candyFillParticle;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameManager.timerHasEnded)
        {


            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                tapCount++;
                bgSound.PlayOneShot(pickUP);
                candyFillParticle.Play();
                gameaudioSource.PlayOneShot(ballonBlast[Random.Range(0, ballonBlast.Length)]);
                Instantiate(particleEffct[Random.Range(0,particleEffct.Length)], hit.transform.position, Quaternion.identity);
                gameplayUI.UpdateTapCounterText(tapCount);
                gameplayUI.PlayBtnSound();
                hit.collider.gameObject.SetActive(false);
            }
        }
    }
}
