using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutscene : MonoBehaviour
{
    public Camera cutsceneCamera;
    public GameObject truck;
    public GameObject player;
    public GameManager gameManager;

    public Light truckLight1;
    public Light truckLight2;

    public AudioSource truckAudio;

    public InsideOutsideAudio audioFader;

    private bool hasPlayed = false;

    // Where objects should be after the cutscene. Not necessarily in the animation.
    private Vector3 truckEndPos;
    private Vector3 truckEndRot;

    // Start is called before the first frame update
    void Start()
    {
        truckEndPos = Vector3.zero;
        truckEndRot = Vector3.zero;
        player.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasPlayed)
        {
            hasPlayed = true;
            StartCoroutine("PlayCutscene");
        }
    }

    IEnumerator PlayCutscene()
    {
        gameManager.InCutscene = true;
        cutsceneCamera.gameObject.SetActive(true);

        // Play animations
        cutsceneCamera.GetComponent<Animator>().Play("IntroCam");
        truck.GetComponent<Animator>().Play("IntroTruck");

        // Wait for animation length in seconds
        yield return new WaitForSeconds(4f);

        cutsceneCamera.gameObject.SetActive(false);
        gameManager.InCutscene = false;

        // Set truck position
        truck.transform.position = truckEndPos;
        truck.transform.eulerAngles = truckEndRot;

        // Turn truck lights off
        truckLight1.intensity = 0f;
        truckLight2.intensity = 0f;

        StartCoroutine(audioFader.FadeOut(truckAudio));

        player.SetActive(true);
    }
}
