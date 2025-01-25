using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ScrollingText
{
    public TextMeshProUGUI text;
    public float scrollSpeed = 10.0f;

    private TextMeshProUGUI cloneText;
    private RectTransform textRectTransform;
    private string sourceText;
    private string tempText;

    private List<string> _generalReviews = new List<string> {
        "Popperton - ⭐⭐⭐ - 'I say, there is not a finer Bubble Farm on this planet!'", 
        "Blubbley - ⭐⭐⭐ - 'The bubblecorn was a delight, but the bubble tractor almost knocked me over! Wear sturdy shoes.'", 
        "Bibbley - ⭐ - 'Something seems off about this farm...'",
        "Globby - ⭐⭐⭐⭐ - 'The shimmering bubble trees are breathtaking! Plus, they gave me a free bubble squash for my first visit.'",
        "Foambert - ⭐ - 'Not practical. My bubble tomatoes popped on the way home. They need to come up with bubble-proof bags.'",
        "Popperton - ⭐⭐⭐⭐ - 'My word! Bubble-Berry Acres produces only the finest bubbles on the market!'",
        "Popolina - '⭐⭐⭐⭐⭐ - The bubble carrots are literally the best! They popped right into my stew—no chopping needed. Magical experience!'",
        "Bloopwell - ⭐⭐⭐⭐⭐ - 'Bubble lettuce is wild! It popped on my sandwich and released a fresh mist. Never eating soap lettuce again.'",
        "Bubblina - ⭐⭐⭐ - 'It’s a cool concept, but the bubble pumpkins were tricky to carry without popping. Maybe they should give out instructions?'",
        "Hubbley - ⭐⭐ - 'I don't know, I just work here...'",
        "Poppaloo - ⭐⭐⭐ - 'The staff were nice, but the bubble goat I tried to pet floated into a tree and popped. Sad but kind of funny.'",
        "Drizzlepop - ⭐⭐⭐⭐ - 'The Bubble Farm was like, totally radical! Never seen so many crops in person before.'",
        "Puffette - ⭐⭐⭐⭐⭐ - 'Thanks to Bubble-Berry Acres, I no longer have to starve my mother inlaw!'", 
        "Glubble - ⭐⭐ - 'I was excited about bubble potatoes, but they disappeared in my stew! Cool experience, but not practical for cooking.'", 
        "Hubblyboo - ⭐ - 'Get out while you still can.'",
        "Popperton - ⭐⭐⭐⭐⭐ - 'I can't stop eating the bubbles even if I tried!'",
        "Hubbley - ⭐⭐⭐ - 'The pay is fine I guess.'"};
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

    void Awake () {
      textRectTransform = text.GetComponent<RectTransform>();
        
      cloneText = Instantiate(text) as TextMeshProUGUI;
      RectTransform cloneRectTransform = cloneText.GetComponent<RectTransform>();
      cloneRectTransform.SetParent(textRectTransform);
      cloneRectTransform.anchorMin = new Vector2(1, 0.5f);
      cloneRectTransform.localPosition = new Vector3(text.preferredWidth, 0, cloneRectTransform.position.z);
      cloneRectTransform.localScale = new Vector3(1, 1, 1);
      cloneText.text = text.text;  

     }

      private IEnumerator Start(){
  
          float width = text.preferredWidth;      
          Vector3 startPosition = textRectTransform.localPosition;

          float scrollPosition = 0;
                while (true) {
           textRectTransform.localPosition = new Vector3(-scrollPosition % width, startPosition.y, startPosition.z);
           scrollPosition += scrollSpeed * 20 * Time.deltaTime;         
           yield return null;
          }      
     }

}
