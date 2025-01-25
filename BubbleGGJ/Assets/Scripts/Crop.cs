using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private float goldGenerationPerSec;
    [SerializeField] private float foodGenerationPerSec;

    private void OnEnable()
    {
        GameManager.Instance.AddGoldPerSec(goldGenerationPerSec);
        GameManager.Instance.AddFoodPerSec(foodGenerationPerSec);
    }
    private void OnDisable()
    {
        GameManager.Instance.AddGoldPerSec(-goldGenerationPerSec);
        GameManager.Instance.AddFoodPerSec(-foodGenerationPerSec);
    }
}
