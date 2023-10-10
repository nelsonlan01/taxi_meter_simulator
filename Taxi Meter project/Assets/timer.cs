using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public Text timerText;
    public Text unitCostText;
    public Text totalCostText;
    public Text intervalText;
    public Text timeSinceLastUpdateText;
    public float unitCost;
    private float elapsedTime;
    private float totalCost;
    private float updateInterval = 60.0f; // Update interval in seconds
    private float timeSinceLastUpdate = 0.0f;
    private bool timerStarted = false;
    public AudioSource audioSource;
    public AudioClip soundEffect;

    public Button startButton;
    public Button soundButton;
    public Button increaseByOneButton;
    public Button increaseByTenButton;
    public Button decreaseByOneButton;
    public Button decreaseByTenButton;

    void Start()
    {
        startButton.onClick.AddListener(StartTimer);
        soundButton.onClick.AddListener(PlaySoundEffect);
        increaseByOneButton.onClick.AddListener(IncreaseUnitCostByOne);
        increaseByTenButton.onClick.AddListener(IncreaseUnitCostByTen);
        decreaseByOneButton.onClick.AddListener(DecreaseUnitCostByOne);
        decreaseByTenButton.onClick.AddListener(DecreaseUnitCostByTen);
    }

    void Update()
    {
        if (timerStarted)
        {
            elapsedTime += Time.deltaTime;
            int hours = (int)(elapsedTime / 3600);
            int minutes = (int)((elapsedTime / 60) % 60);
            int seconds = (int)(elapsedTime % 60);

            string timeString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

            if (timerText != null)
            {
                timerText.text = timeString;
            }

            totalCost = (elapsedTime / 60) * (unitCost / 60);
            timeSinceLastUpdate += Time.deltaTime;

            if (timeSinceLastUpdate >= updateInterval)
            {
                timeSinceLastUpdate = 0.0f;

                if (totalCostText != null)
                {
                    totalCostText.text =  totalCost.ToString("F1");
                }

                PlaySoundEffect();
            }

            if (unitCostText != null)
            {
                unitCostText.text =  unitCost.ToString();
            }

            if (intervalText != null)
            {
                intervalText.text = updateInterval.ToString("F1");
            }

            if (timeSinceLastUpdateText != null)
            {
                timeSinceLastUpdateText.text =  timeSinceLastUpdate.ToString("F1");
            }
        }
    }

    public void StartTimer()
    {
        timerStarted = true;
    }

    public void PlaySoundEffect()
    {
        if (audioSource != null && soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
    }

    public void IncreaseUnitCostByOne()
    {
        unitCost += 1;
    }

    public void IncreaseUnitCostByTen()
    {
        unitCost += 10;
    }

    public void DecreaseUnitCostByOne()
    {
        unitCost -= 1;
        if (unitCost < 0)
        {
            unitCost = 0;
        }
    }

    public void DecreaseUnitCostByTen()
    {
        unitCost -= 10;
        if (unitCost < 0)
        {
            unitCost = 0;
        }
    }
}