using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;

public class Cinematics_DR : MonoBehaviour
{
    public bool playStartCinematic = true;

    private VideoPlayer videoPlayer;
    private PlayableDirector startCinematic;
    private PlayableDirector goodEndCinematic;
    private PlayableDirector badEndCinematic;

    private GameObject monitor;
    private bool monitorOn = false;

    private Interact_HR playerInteraction;

    public enum END_CINEMATICS
    {
        GOOD,
        BAD
    }

    private void Awake()
    {
        videoPlayer = GameObject.Find("LaptopScreen").GetComponent<VideoPlayer>();
        startCinematic = GameObject.Find("StartCinematic").GetComponent<PlayableDirector>();
        goodEndCinematic = GameObject.Find("GoodEndCinematic").GetComponent<PlayableDirector>();
        badEndCinematic = GameObject.Find("BadEndCinematic").GetComponent<PlayableDirector>();

        monitor = GameObject.Find("Monitor");
        for(int i = 0; i < monitor.transform.childCount; i++)
        {
            monitor.transform.GetChild(i).gameObject.SetActive(false);
        }

        playerInteraction = GameObject.Find("FirstPersonCharacter").GetComponent<Interact_HR>();
    }

    private void Start()
    {
        if(playStartCinematic)
        {
            playerInteraction.enabled = false;
            startCinematic.Play();
        }
    }

    public void PlayEndCinematic(END_CINEMATICS type)
    {
        switch (type)
        {
            case END_CINEMATICS.GOOD:
                playerInteraction.enabled = false;
                goodEndCinematic.Play();
                break;
            case END_CINEMATICS.BAD:
                playerInteraction.enabled = false;
                badEndCinematic.Play();
                break;
        }

        //TODO: Credits once cinematic is over
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
    }

    public void ToggleMonitor()
    {
        monitorOn = !monitorOn;
        for (int i = 0; i < monitor.transform.childCount; i++)
        {
            monitor.transform.GetChild(i).gameObject.SetActive(monitorOn);
        }
    }

    public void StartCinematicDone()
    {
        playStartCinematic = false;
        playerInteraction.enabled = true;
    }
}
