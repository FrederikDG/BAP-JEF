using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class PictureSwapper : MonoBehaviour
{
    [System.Serializable]
    public class PictureStats
    {
        public Texture picture;
        public float speed;
        public float resistance;
        public float power;
        public float weight;
    }

    public RawImage noseArrayImg;
    public RawImage bodyArrayImg;
    public RawImage tailArrayImg;

    public PictureStats[] noseArray;
    public PictureStats[] bodyArray;
    public PictureStats[] tailArray;

    public TextMeshProUGUI speedText;
    public TextMeshProUGUI resistanceText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI weightText;
    public TextMeshProUGUI timerText; 
    private float countdownTime = 120f; 
    private bool timerRunning = false;
    public RectTransform speedBar;
    public RectTransform resistanceBar;
    private const float MaxBarWidth = 230f;
    private const float AnimationDuration = 1f;

    private int currentIndex1 = 0;
    private int currentIndex2 = 0;
    private int currentIndex3 = 0;

    public float moveSpeed = 1f;  
    public float moveHeight = 50f;  

    private Vector3 startPosition;

    void Start()
    {
        if (noseArray.Length > 0 && bodyArray.Length > 0 && tailArray.Length > 0)
        {
            UpdatePicture();
        }
        else
        {
            Debug.LogWarning("One or more picture arrays are empty!");
        }

        startPosition = transform.position;

        Debug.Log("Start method running");

        StartCoroutine(MoveUpAndDown());

        UpdateCombinedStats();
        StartCountdown();
        StartCoroutine(UpdateTextCount(speedText, GetTotalSpeed()));
        StartCoroutine(UpdateTextCount(resistanceText, GetTotalResistance()));
        StartCoroutine(UpdateTextCount(powerText, GetTotalPower()));
        StartCoroutine(UpdateTextCount(weightText, GetTotalWeight()));
    }

    private IEnumerator MoveUpAndDown()
    {
        float time = 0f;

        while (true)
        {
            time += Time.deltaTime * moveSpeed;
            float offsetY = Mathf.Sin(time) * moveHeight;
            transform.position = startPosition + new Vector3(0f, offsetY, 0f);

            yield return null;
        }
    }

    public void NextPicturenoseArray()
    {
        if (noseArray.Length > 0)
        {
            currentIndex1 = (currentIndex1 + 1) % noseArray.Length;
            UpdatePicture();
        }
    }

    public void PreviousPicturenoseArray()
    {
        if (noseArray.Length > 0)
        {
            currentIndex1 = (currentIndex1 - 1 + noseArray.Length) % noseArray.Length;
            UpdatePicture();
        }
    }

    public void NextPicturebodyArray()
    {
        if (bodyArray.Length > 0)
        {
            currentIndex2 = (currentIndex2 + 1) % bodyArray.Length;
            UpdatePicture();
        }
    }

    public void PreviousPicturebodyArray()
    {
        if (bodyArray.Length > 0)
        {
            currentIndex2 = (currentIndex2 - 1 + bodyArray.Length) % bodyArray.Length;
            UpdatePicture();
        }
    }

    public void NextPicturetailArray()
    {
        if (tailArray.Length > 0)
        {
            currentIndex3 = (currentIndex3 + 1) % tailArray.Length;
            UpdatePicture();
        }
    }

    public void PreviousPicturetailArray()
    {
        if (tailArray.Length > 0)
        {
            currentIndex3 = (currentIndex3 - 1 + tailArray.Length) % tailArray.Length;
            UpdatePicture();
        }
    }

    private float GetTotalSpeed()
    {
        float totalSpeed = 0;

        if (noseArray.Length > 0)
            totalSpeed += noseArray[currentIndex1].speed;
        if (bodyArray.Length > 0)
            totalSpeed += bodyArray[currentIndex2].speed;
        if (tailArray.Length > 0)
            totalSpeed += tailArray[currentIndex3].speed;

        return totalSpeed;
    }

    private float GetTotalResistance()
    {
        float totalResistance = 0;

        if (noseArray.Length > 0)
            totalResistance += noseArray[currentIndex1].resistance;
        if (bodyArray.Length > 0)
            totalResistance += bodyArray[currentIndex2].resistance;
        if (tailArray.Length > 0)
            totalResistance += tailArray[currentIndex3].resistance;

        return totalResistance;
    }

    private float GetTotalPower()
    {
        float totalPower = 0;

        if (noseArray.Length > 0)
            totalPower += noseArray[currentIndex1].power;
        if (bodyArray.Length > 0)
            totalPower += bodyArray[currentIndex2].power;
        if (tailArray.Length > 0)
            totalPower += tailArray[currentIndex3].power;

        return totalPower;
    }

    private float GetTotalWeight()
    {
        float totalWeight = 0;

        if (noseArray.Length > 0)
            totalWeight += noseArray[currentIndex1].weight;
        if (bodyArray.Length > 0)
            totalWeight += bodyArray[currentIndex2].weight;
        if (tailArray.Length > 0)
            totalWeight += tailArray[currentIndex3].weight;

        return totalWeight;
    }

    private void UpdatePicture()
    {
        if (noseArray.Length > 0)
            noseArrayImg.texture = noseArray[currentIndex1].picture;

        if (bodyArray.Length > 0)
            bodyArrayImg.texture = bodyArray[currentIndex2].picture;

        if (tailArray.Length > 0)
            tailArrayImg.texture = tailArray[currentIndex3].picture;

        UpdateCombinedStats();
    }

    private void UpdateCombinedStats()
    {
        float totalSpeed = GetTotalSpeed();
        float totalResistance = GetTotalResistance();
        float totalPower = GetTotalPower();
        float totalWeight = GetTotalWeight();

        StartCoroutine(UpdateTextCount(speedText, totalSpeed));
        StartCoroutine(UpdateTextCount(resistanceText, totalResistance));
        StartCoroutine(UpdateTextCount(powerText, totalPower));
        StartCoroutine(UpdateTextCount(weightText, totalWeight));

        float maxSpeed = 100f;
        float maxResistance = 100f;

        float speedPercentage = Mathf.Clamp01(totalSpeed / maxSpeed);
        float resistancePercentage = Mathf.Clamp01(totalResistance / maxResistance);

        StartCoroutine(AnimateBarGrowth(speedBar, speedPercentage));
        StartCoroutine(AnimateBarGrowth(resistanceBar, resistancePercentage));
    }

    private IEnumerator UpdateTextCount(TextMeshProUGUI textMesh, float targetValue)
    {
        float startValue = float.Parse(textMesh.text);
        float timeElapsed = 0f;

        while (timeElapsed < AnimationDuration)
        {
            timeElapsed += Time.deltaTime;
            float currentValue = Mathf.Lerp(startValue, targetValue, timeElapsed / AnimationDuration);
            textMesh.text = currentValue.ToString("F0");
            yield return null;
        }

        textMesh.text = targetValue.ToString("F0");
    }

    private IEnumerator AnimateBarGrowth(RectTransform bar, float targetPercentage)
    {
        float startWidth = bar.sizeDelta.x;
        float targetWidth = MaxBarWidth * targetPercentage;
        float elapsedTime = 0f;

        while (elapsedTime < AnimationDuration)
        {
            bar.sizeDelta = new Vector2(Mathf.Lerp(startWidth, targetWidth, elapsedTime / AnimationDuration), bar.sizeDelta.y);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bar.sizeDelta = new Vector2(targetWidth, bar.sizeDelta.y);
    }
    public void StartCountdown()
    {
        if (!timerRunning)
        {
            timerRunning = true;
            StartCoroutine(CountdownTimer());
        }
    }

    private IEnumerator CountdownTimer()
    {
        float remainingTime = countdownTime;

        while (remainingTime > 1)
        {
            remainingTime -= Time.deltaTime;

            
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);

            
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            
            if (remainingTime <= 5f)
            {
                StartCoroutine(BlinkTimerText());
            }

            yield return null;
        }

        
        StopAllCoroutines(); 
        timerText.text = "00:01";

        
        yield return new WaitForSeconds(1f);
        PlayGame();
    }

    private IEnumerator BlinkTimerText()
    {
        while (true)
        {
            timerText.enabled = !timerText.enabled; 
            yield return new WaitForSeconds(0.5f); 
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
