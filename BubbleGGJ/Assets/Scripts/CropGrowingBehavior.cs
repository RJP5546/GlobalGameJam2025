using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class CropGrowingBehavior : MonoBehaviour
{
    public Crop Crop;

    public GameObject parentObject;
    // all of the crops in the planter box
    public GameObject[] CropsGrowing;
    // price will be replaced by Crop.GetUpgradeMultiplier and will be reset every single time a new upgrade is % 5
    public int price;
    private int i = 0;

    public float timer = 0f;
    public float growTime = 1f;
    public float maxSize = 200f;
    public float minSize = 1f;
    
    public bool isMaxSize = false;
    // plays glowing particles if the crop is upgraded

    // particle system
    public ParticleSystem upgradeParticle;

    public void CropUpgradeParticles()
    {
        ParticleSystem Correct = Instantiate(upgradeParticle, new Vector3(parentObject.transform.position.x - 1f, parentObject.transform.position.y, parentObject.transform.position.z - 2f), Quaternion.Euler(-90, 0, 0));
        Correct.Play();
        Destroy(Correct.gameObject, 3f);
    }

    public void CropAmount()
    {
        //if (Crop.GetUpgradeMultiplier() % 5 == 0)
        // {
        //    CropsGrowing[i].SetActive(true);
        //     i++;
        // }

        // checks if the amount of upgrades is divisible by 5 and if so 
        // increases the amount of crops in the planter box
        if (price % 5 == 0 && price != 0)
        {
            if (i < CropsGrowing.Length)
            {
                CropsGrowing[i].SetActive(true);
                i++;
                CropUpgradeParticles();
                price = 0;
            }
        }
    }



    private IEnumerator Grow(GameObject crop)
    {
        Vector3 startScale = new Vector3(minSize, minSize, minSize);
        Vector3 maxScale = crop.transform.localScale;

        if(crop.transform.localScale.x<1f)
        {
            Vector3 transfer = startScale;
            startScale = maxScale;
            maxScale = transfer;
        }

        while (true)  // Loop indefinitely
        {
            // Grow the crop from minSize to maxSize
            float timer = 0f;
            while (timer < growTime)
            {
                crop.transform.localScale = Vector3.Lerp(startScale, maxScale, timer / growTime);
                timer += Time.deltaTime;
                yield return null;
            }
            crop.transform.localScale = maxScale;  // Ensure it reaches max size
            // pause at max scale
            yield return new WaitForSeconds(2f);
            //play popping sound if there is a crop
            if (CropsGrowing[0].activeSelf) { SFXManager.Instance.PlayBubbleSound(); }
            // go back to starting scale
            crop.transform.localScale = startScale;
            // wait
            yield return new WaitForSeconds(.5f);

        }
    }


    void Start()
    {
        CropsGrowing = new GameObject[parentObject.transform.childCount];

        for (int i = 0; i < CropsGrowing.Length; i++)
        {
            CropsGrowing[i] = parentObject.transform.GetChild(i).gameObject;
        }

        // Start growing crops if they're not already at max size
        if (!isMaxSize)
        {
            foreach (var crop in CropsGrowing)
            {
                if (crop != null && !crop.activeSelf)
                {
                    StartCoroutine(Grow(crop)); // Start growing each crop
                }
            }
        }

    }

    public void Update()
    {
        CropAmount();
    }
}
