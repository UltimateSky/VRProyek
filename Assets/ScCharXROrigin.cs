using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GemObjectiveManager : MonoBehaviour
{
    public static GemObjectiveManager Instance;

    [Header("Gem Status")]
    public bool redCollected = false;
    public bool greenCollected = false;
    public bool yellowCollected = false;

    [Header("Gem Row GameObjects")]
    public GameObject rowRed;
    public GameObject rowGreen;
    public GameObject rowYellow;

    [Header("Checkmark Images (per gem)")]
    public Image checkRed;
    public Image checkGreen;
    public Image checkYellow;

    [Header("Gem Icon Images (per gem)")]
    public Image iconRed;
    public Image iconGreen;
    public Image iconYellow;

    [Header("Status Texts (per gem)")]
    public TextMeshProUGUI statusRed;
    public TextMeshProUGUI statusGreen;
    public TextMeshProUGUI statusYellow;

    [Header("Progress")]
    public Slider progressSlider;
    public TextMeshProUGUI progressCountText;

    [Header("Win Panel")]
    public GameObject winPanel;

    [Header("Colors")]
    public Color gemDimColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    public Color gemBrightColor = Color.white;
    public Color checkCollectedColor = new Color(0.36f, 0.73f, 0.25f, 1f);
    public Color checkEmptyColor = new Color(0.15f, 0.15f, 0.12f, 1f);

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        winPanel.SetActive(false);
        RefreshUI();
    }

    public void CollectGem(string color)
    {
        switch (color.ToLower())
        {
            case "red":
                if (redCollected) return;
                redCollected = true;
                break;
            case "green":
                if (greenCollected) return;
                greenCollected = true;
                break;
            case "yellow":
                if (yellowCollected) return;
                yellowCollected = true;
                break;
        }
        RefreshUI();
    }

    void RefreshUI()
    {
        UpdateGemRow(redCollected, iconRed, checkRed, statusRed, "Red Gem collected");
        UpdateGemRow(greenCollected, iconGreen, checkGreen, statusGreen, "Green Gem collected");
        UpdateGemRow(yellowCollected, iconYellow, checkYellow, statusYellow, "Yellow Gem collected");

        int count = (redCollected ? 1 : 0) + (greenCollected ? 1 : 0) + (yellowCollected ? 1 : 0);
        progressSlider.value = count / 3f;
        progressCountText.text = count + " / 3";

        if (count == 3)
            winPanel.SetActive(true);
    }

    void UpdateGemRow(bool collected, Image icon, Image check, TextMeshProUGUI status, string doneText)
    {
        icon.color = collected ? gemBrightColor : gemDimColor;
        check.color = collected ? checkCollectedColor : checkEmptyColor;
        status.text = collected ? doneText : "Not yet found...";
    }
}
