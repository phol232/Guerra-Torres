using UnityEngine;
using UnityEngine.UI;
using System.Collections; 
using TMPro;
using GameCore;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("HUD")]
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private Slider _baseHealthBar;
    [SerializeField] private Slider _heroHealthBar;
    [SerializeField] private Button _flecheraBtn;
    [SerializeField] private Button _explosivaBtn;
    [SerializeField] private Image _specialCooldownFill;
    [Header("Estados")]
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private GameObject _defeatPanel;
    [SerializeField] private TextMeshProUGUI _tutorialText;
    [SerializeField] private GameObject _heroDeadOverlay;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        _flecheraBtn.onClick.AddListener(() => TowerManager.Instance.SelectTower("Flechera"));
        _explosivaBtn.onClick.AddListener(() => TowerManager.Instance.SelectTower("Explosiva"));
        ResourceManager.Instance.OnGoldChanged += UpdateGold;
    }

    public void UpdateGold(int gold)
    {
        _goldText.text = $"Oro: {gold}";
    }

    public void UpdateHeroHealth(int current, int max)
    {
        _heroHealthBar.value = (float)current / max;
    }

    public void UpdateBaseHealth(int current, int max)
    {
        _baseHealthBar.value = (float)current / max;
    }

    public void ShowSpecialCooldown(float duration)
    {
        StartCoroutine(CooldownBar(duration));
    }

    private IEnumerator CooldownBar(float duration)
    {
        float t = duration;
        while (t > 0)
        {
            _specialCooldownFill.fillAmount = t / duration;
            yield return null;
            t -= Time.deltaTime;
        }
        _specialCooldownFill.fillAmount = 0;
    }

    public void ShowVictoryScreen()
    {
        _victoryPanel.SetActive(true);
    }

    public void ShowDefeatScreen()
    {
        _defeatPanel.SetActive(true);
    }

    public void ShowHeroDead(bool isDead)
    {
        _heroDeadOverlay.SetActive(isDead);
    }

    public void ShowTutorial(string msg)
    {
        _tutorialText.text = msg;
        _tutorialText.gameObject.SetActive(!string.IsNullOrEmpty(msg));
    }
}