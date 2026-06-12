using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GemObjectiveManager : MonoBehaviour
{
    [Header("UI Progress Bar")]
    public Slider gemProgressBar;

    [Header("Red Gem UI")]
    public TextMeshProUGUI txtStatusRed;
    public GameObject imgCheckRed;

    [Header("Green Gem UI")]
    public TextMeshProUGUI txtStatusGreen;
    public GameObject imgCheckGreen;

    [Header("Yellow Gem UI")]
    public TextMeshProUGUI txtStatusYellow;
    public GameObject imgCheckYellow;

    [Header("Global Header Status")]
    public Text txtQuestName; // Menggunakan UI Text biasa

    private bool isRedPlaced = false;
    private bool isGreenPlaced = false;
    private bool isYellowPlaced = false;

    private int totalGemsPlaced = 0;
    private readonly int maxGems = 3;

    void Start()
    {
        // 1. Setup awal Slider
        if (gemProgressBar != null)
        {
            gemProgressBar.minValue = 0;
            gemProgressBar.maxValue = maxGems;
            gemProgressBar.value = 0;
        }

        // 2. Set teks status awal otomatis saat play
        if (txtStatusRed != null) txtStatusRed.text = "Gems Collected: 0 / 1";
        if (txtStatusGreen != null) txtStatusGreen.text = "Gems Collected: 0 / 1";
        if (txtStatusYellow != null) txtStatusYellow.text = "Gems Collected: 0 / 1";

        // 3. Sembunyikan tanda centang di awal
        if (imgCheckRed != null) imgCheckRed.SetActive(false);
        if (imgCheckGreen != null) imgCheckGreen.SetActive(false);
        if (imgCheckYellow != null) imgCheckYellow.SetActive(false);
    }

    public void PlaceGem(string gemColor)
    {
        string colorKey = gemColor.ToLower().Trim();

        if (colorKey == "red" && !isRedPlaced)
        {
            isRedPlaced = true;
            if (txtStatusRed != null) txtStatusRed.text = "Gems Collected: 1 / 1";
            if (imgCheckRed != null) imgCheckRed.SetActive(true);
            UpdateProgress();
        }
        else if (colorKey == "green" && !isGreenPlaced)
        {
            isGreenPlaced = true;
            if (txtStatusGreen != null) txtStatusGreen.text = "Gems Collected: 1 / 1";
            if (imgCheckGreen != null) imgCheckGreen.SetActive(true);
            UpdateProgress();
        }
        else if (colorKey == "yellow" && !isYellowPlaced)
        {
            isYellowPlaced = true;
            if (txtStatusYellow != null) txtStatusYellow.text = "Gems Collected: 1 / 1";
            if (imgCheckYellow != null) imgCheckYellow.SetActive(true);
            UpdateProgress();
        }
    }

    void UpdateProgress()
    {
        totalGemsPlaced++;

        if (gemProgressBar != null)
        {
            gemProgressBar.value = totalGemsPlaced;
        }

        if (totalGemsPlaced >= maxGems)
        {
            CompleteMission();
        }
    }

    void CompleteMission()
    {
        Debug.Log("Misi Selesai! Semua Cursed Gems terkumpul.");
        if (txtQuestName != null)
        {
            txtQuestName.text = "Objective: Complete!";
            txtQuestName.color = Color.green;
        }
    }
}