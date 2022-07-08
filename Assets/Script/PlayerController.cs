using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// êßå‰Ç∑ÇÈVideo PlayerÇÃÉäÉXÉg
    /// </summary>
    [SerializeField]
    private List<VideoPlayer> playerList;
    /// <summary>
    /// çƒê∂
    /// </summary>
    public void Play()
    {
        foreach (VideoPlayer player in playerList)
        {
            if (!player.isPlaying)
            {
                player.Play();
            }
        }
    }
    /// <summary>
    /// àÍéûí‚é~
    /// </summary>
    public void Pause()
    {
        foreach (VideoPlayer player in playerList)
        {
            if (player.isPlaying)
            {
                player.Pause();
            }
        }
    }
    /// <summary>
    /// í‚é~
    /// </summary>
    public void Stop()
    {
        foreach (VideoPlayer player in playerList)
        {
            player.Stop();
        }
    }
}
