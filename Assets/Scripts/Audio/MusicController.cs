using UnityEngine;

public class MusicController : MonoBehaviour
{

    public AudioSource musicSourcePast, musicSourcePresent, musicSourceFuture, musicSourceHub;

    public void UpdateMusic(Room room) {
        musicSourcePast.volume = 0;
        musicSourcePresent.volume = 0;
        musicSourceFuture.volume = 0;
        musicSourceHub.volume = 0;
        switch (room)
        {
            case Room.Past:
                musicSourcePast.volume = 1;
                break;
            case Room.Present:
                musicSourcePresent.volume = 1;
                break;
            case Room.Future:
                musicSourceFuture.volume = 1;
                break;
            case Room.Hub:
                musicSourceHub.volume = 1;
                break;
        }
    }
}
