using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ScrollingText2 : MonoBehaviour
{
    
    public TextMeshProUGUI uiText; // Uncomment for TextMeshPro
    public float scrollSpeed = 100f; // Speed of the scrolling text
    public float intervalInSeconds = 60f; // Time interval between messages

    private RectTransform textRect;
    private float startPosX;
    private float endPosX;

    private List<string> _generalReviews = new List<string> {
        "Popperton - *** - 'I say, there is not a finer Bubble Farm on this planet!'", 
        "Blubbley - *** - 'The bubblecorn was a delight, but the bubble tractor almost knocked me over! Wear sturdy shoes.'", 
        "Bibbley - * - 'Something seems off about this farm...'",
        "Globby - **** - 'The shimmering bubble trees are breathtaking! Plus, they gave me a free bubble squash for my first visit.'",
        "Foambert - * - 'Not practical. My bubble tomatoes popped on the way home. They need to come up with bubble-proof bags.'",
        "Popperton - **** - 'My word! Bubble-Berry Acres produces only the finest bubbles on the market!'",
        "Popolina - ***** - 'The bubble carrots are literally the best! They popped right into my stew—no chopping needed. Magical experience!'",
        "Bloopwell - ***** - 'Bubble lettuce is wild! It popped on my sandwich and released a fresh mist. Never eating soap lettuce again.'",
        "Bubblina - *** - 'It’s a cool concept, but the bubble pumpkins were tricky to carry without popping. Maybe they should give out instructions?'",
        "Hubbley - ** - 'I don't know, I just work here...'",
        "Poppaloo - *** - 'The staff were nice, but the bubble goat I tried to pet floated into a tree and popped. Sad but kind of funny.'",
        "Drizzlepop - **** - 'The Bubble Farm was like, totally radical! Never seen so many crops in person before.'",
        "Puffette - ***** - 'Thanks to Bubble-Berry Acres, I no longer have to starve my mother inlaw!'", 
        "Glubble - ** - 'I was excited about bubble potatoes, but they disappeared in my stew! Cool experience, but not practical for cooking.'", 
        "Hubblyboo - * - 'Get out while you still can.'",
        "Popperton - ***** - 'I can't stop eating the bubbles even if I tried!'",
        "Hubbley - *** - 'The pay is fine I guess.'"};
    private List<string> _milstoneReviews = new List<string> {
        "BREAKING NEWS: New Bubble Farm Bubbles-Berry Acres producing high quality Bubble Crops. The public will be watching with a close lense.",
        "BREAKING NEWS: ", // 10% To Goal
        "BREAKING NEWS: ", // 20% To Goal
        "BREAKING NEWS: ", // 30% To Goal
        "BREAKING NEWS: ", // 40% To Goal
        "BREAKING NEWS: ", // 50% To Goal
        "BREAKING NEWS: ", // 60% To Goal
        "BREAKING NEWS: ", // 70% To Goal
        "BREAKING NEWS: ", // 80% To Goal
        "BREAKING NEWS: ", // 90% TO Goal
        "BREAKING NEWS: "}; // 95% To Goal

   
    void Start()
    {
        if (uiText == null)
        {
            Debug.LogError("UI Text is not assigned!");
            return;
        }

        textRect = uiText.GetComponent<RectTransform>();
        StartCoroutine(ShowRandomMessages());
    }

    IEnumerator ShowRandomMessages()
    {
        while (true)
        {
            DisplayRandomMessage();
            yield return new WaitForSeconds(intervalInSeconds);
        }
    }

    void DisplayRandomMessage()
    {
        // Pick a random message from the list
        string randomMessage = _generalReviews[Random.Range(0, _generalReviews.Count)];
        uiText.text = randomMessage;

        // Reset text position to start outside the screen
        startPosX = Screen.width; // Start off-screen to the right
        endPosX = -textRect.rect.width + 200; // End off-screen to the left
        textRect.anchoredPosition = new Vector2(startPosX, textRect.anchoredPosition.y);

        // Start scrolling
        StartCoroutine(ScrollText());
    }

    IEnumerator ScrollText()
    {
        while (textRect.anchoredPosition.x > endPosX)
        {
            textRect.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;
            yield return null;
        }
    }

}
