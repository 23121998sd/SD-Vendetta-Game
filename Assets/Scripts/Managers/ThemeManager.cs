using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager Instance;

    private const string ThemePrefKey = "ThemePreference";
    private Color darkColor = Color.black;
    private Color lightColor = Color.white;
    private bool isDarkTheme;
    private Image background;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadTheme();
    }

    private void Start()
    {
        background = GetComponent<Image>();
        ApplyTheme();
    }

    public void ToggleTheme()
    {
        isDarkTheme = !isDarkTheme;
        StartCoroutine(FadeToTheme(isDarkTheme ? darkColor : lightColor));
        SaveTheme();
    }

    private IEnumerator FadeToTheme(Color targetColor)
    {
        Color initialColor = background.color;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            background.color = Color.Lerp(initialColor, targetColor, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        background.color = targetColor;
    }

    private void LoadTheme()
    {
        isDarkTheme = PlayerPrefs.GetInt(ThemePrefKey, 0) == 1;
    }

    private void SaveTheme()
    {
        PlayerPrefs.SetInt(ThemePrefKey, isDarkTheme ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void ApplyTheme()
    {
        background.color = isDarkTheme ? darkColor : lightColor;
    }
}