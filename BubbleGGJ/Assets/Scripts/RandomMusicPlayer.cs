using SimpleAudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusicPlayer : MonoBehaviour
{
    [SerializeField] private List<int> previousPlayedTracks;

    private void Start()
    {
        previousPlayedTracks.Add(0);
        StartCoroutine(PlayRandomSong());
    }

    private IEnumerator PlayRandomSong()
    {
        yield return new WaitForSeconds(Random.Range(90, 320));

        while (true)
        {
            if (Manager.instance.IsPlaying) 
            {
                yield return new WaitForSeconds(Random.Range(90, 320));
                Manager.instance.PlaySong(RandomSongNotPlayedRecently());
            }
            else { yield return null; }
        }
        
    }

    private int RandomSongNotPlayedRecently()
    {
        int randomNumber = Random.Range(1, 5);
        if (!previousPlayedTracks.Contains(randomNumber))
        {
            if(previousPlayedTracks.Count >= 3) { previousPlayedTracks.RemoveAt(0); }
            previousPlayedTracks.Add(randomNumber);
            return randomNumber;
        }
        else { return RandomSongNotPlayedRecently(); }
    }
}
