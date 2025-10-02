using System.Timers;
using UnityEngine;

public class UIMusic : MonoBehaviour
{
    [SerializeField] GameObject muteImg;
    MusicManager manager;
    bool mute;

    void Start()
    {
        manager = MusicManager.inst;
        muteImg.SetActive(manager.IsMuted);
    }
    public void SetMute()
    {
        if(manager.IsChanging) return;
        mute = !mute;
        muteImg.SetActive(mute);
        if(mute) manager.Stop();
        else manager.Play();
    }
}