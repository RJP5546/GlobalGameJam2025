using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    [SerializeField] private AudioSource buttonsSource;
    [SerializeField] private AudioSource shopSource;
    [SerializeField] private AudioSource bubbleSource;
    [SerializeField] private AudioSource coinsSource;
    [SerializeField] private AudioSource scienceSource;

    [SerializeField] private List<AudioClip> buttons;
    [SerializeField] private List<AudioClip> shop;
    [SerializeField] private List<AudioClip> bubblePops;
    [SerializeField] private List<AudioClip> coins;
    [SerializeField] private List<AudioClip> science;

    public void PlayButtonSound()
    {
        buttonsSource.PlayOneShot( buttons[Random.Range(0, buttons.Count)] );
    }
    public void PlayShopSound()
    {
        shopSource.PlayOneShot(shop[Random.Range(0, shop.Count)]);
    }
    public void PlayBubbleSound()
    {
        //bubbleSource.PlayOneShot( bubblePops[Random.Range(0, bubblePops.Count - 1)] );
        bubbleSource.clip = bubblePops[Random.Range(0, bubblePops.Count)];
        bubbleSource.Play();
    }
    public void PlaycoinsSound()
    {
        coinsSource.PlayOneShot( coins[Random.Range(0, coins.Count)] );
    }
    public void PlayscienceSound()
    {
        scienceSource.PlayOneShot( science[Random.Range(0, science.Count)] );
    }

}
