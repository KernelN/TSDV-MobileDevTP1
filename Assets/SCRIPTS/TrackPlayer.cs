using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    [SerializeField] MusicManager.Track track;
    [SerializeField] bool play = true;
    void Start()
    {
        if (play)
            MusicManager.inst.PlayTrack(track);
        else
            MusicManager.inst.Stop();
    }
}
