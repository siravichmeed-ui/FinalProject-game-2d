using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HPBarandManabar : MonoBehaviour
{
    public static HPBarandManabar Instance { get; private set; }

    [Header("Target")]
    [SerializeField] private Health targetHealth;

    [Header("Health UI")]
    public Image currentHealthBar;
    public Image currentHealthGlobe;
    public Text healthText;

    [Header("Mana UI")]
    public Image currentManaBar;
    public Image currentManaGlobe;
    public Text manaText;
    public float manaPoint = 100f;
    public float maxManaPoint = 100f;

    [Header("Regen")]
    public bool Regenerate = true;
    public float regen = 0.1f;
    private float timeleft = 0.0f;
    public float regenUpdateInterval = 1f;

    public bool GodMode;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (targetHealth != null)
        {
            targetHealth.OnHealthChanged += OnHealthChanged;
            OnHealthChanged(targetHealth.CurrentHealth, targetHealth.MaxHealth);
        }

        UpdateManaUI();
        timeleft = regenUpdateInterval;
    }

    void OnDestroy()
    {
        if (targetHealth != null)
            targetHealth.OnHealthChanged -= OnHealthChanged;
    }

    void Update()
    {
        if (Regenerate && targetHealth != null)
            Regen();
    }

    private void Regen()
    {
        timeleft -= Time.deltaTime;

        if (timeleft <= 0.0f)
        {
            if (GodMode)
            {
                targetHealth.Heal(targetHealth.MaxHealth);
                RestoreMana(maxManaPoint);
            }
            else
            {
                targetHealth.Heal(regen);
                RestoreMana(regen);
            }

            timeleft = regenUpdateInterval;
        }
    }

    private void OnHealthChanged(float current, float max)
    {
        UpdateHealthBar(current, max);
        UpdateHealthGlobe(current, max);
    }

    private void UpdateHealthBar(float hp, float maxHp)
    {
        float ratio = hp / maxHp;
        currentHealthBar.rectTransform.localPosition =
            new Vector3(currentHealthBar.rectTransform.rect.width * ratio - currentHealthBar.rectTransform.rect.width, 0, 0);

        healthText.text = hp.ToString("0") + "/" + maxHp.ToString("0");
    }

    private void UpdateHealthGlobe(float hp, float maxHp)
    {
        float ratio = hp / maxHp;
        currentHealthGlobe.rectTransform.localPosition =
            new Vector3(0, currentHealthGlobe.rectTransform.rect.height * ratio - currentHealthGlobe.rectTransform.rect.height, 0);

        healthText.text = hp.ToString("0") + "/" + maxHp.ToString("0");
    }

    private void UpdateManaBar()
    {
        float ratio = manaPoint / maxManaPoint;
        currentManaBar.rectTransform.localPosition =
            new Vector3(currentManaBar.rectTransform.rect.width * ratio - currentManaBar.rectTransform.rect.width, 0, 0);
        manaText.text = manaPoint.ToString("0") + "/" + maxManaPoint.ToString("0");
    }

    private void UpdateManaUI()
    {
        UpdateManaBar();
    }

    public void UseMana(float Mana)
    {
        manaPoint -= Mana;
        if (manaPoint < 0) manaPoint = 0;
        UpdateManaUI();
    }

    public void RestoreMana(float Mana)
    {
        manaPoint += Mana;
        if (manaPoint > maxManaPoint) manaPoint = maxManaPoint;
        UpdateManaUI();
    }
}
