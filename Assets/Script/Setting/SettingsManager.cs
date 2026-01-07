using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Linq; // 用於查詢解析度陣列

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    [Header("音量滑塊")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    [Header("外部引用")]
    public AudioMixer mainMixer;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown fullScreenDropdown; // 新的全螢幕 Dropdown

    private FullScreenMode currentFullScreenMode;
    private Resolution[] availableResolutions;

    private const string PREFS_RES_INDEX = "ResolutionIndex";
    private const string PREFS_FULLSCREEN_KEY = "FullscreenMode"; // Dropdown 索引鍵

    private const int TARGET_DEFAULT_RES_INDEX = 19; 

    private const int TARGET_DEFAULT_MODE_INDEX = 0; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 1. 初始化所有設定 (解析度、全螢幕、音量)
        InitializeSettings();
        
        // 2. 應用初始設定 (確保遊戲以正確的解析度和模式啟動)
        // 注意: Dropdown.value 的設置會觸發各自的 Listener，但為了確保啟動時一定生效，我們手動觸發一次。
        ApplyCurrentSettings(); 
    }
    
    // SettingsManager.cs

    // ===================================================================
    // B. 音量控制功能
    // ===================================================================
    // 將這個方法綁定到所有音量滑塊 (Slider) 的 OnValueChanged 事件
    public void SetVolume(string exposedParamName, float volume)
    {
        // 將滑塊的線性值 (0.0001 到 1) 轉換為對數分貝 (-80dB 到 0dB)
        float db = Mathf.Log10(Mathf.Max(0.0001f, volume)) * 20;
    
        // 應用到 Audio Mixer
        mainMixer.SetFloat(exposedParamName, db);
    
        // 儲存設定
        PlayerPrefs.SetFloat(exposedParamName, volume);
        PlayerPrefs.Save();
    }

    // 輔助方法：將滑塊值轉換回 dB (用於初始化)
    private float GetVolumeDB(float linearVolume)
    {
        return Mathf.Log10(Mathf.Max(0.0001f, linearVolume)) * 20;
    }
    
    // ===================================================================
    // C. 螢幕與解析度控制功能
    // ===================================================================
    
    // 1. 初始化所有設定 (取代 InitializeResolution 和 LoadPersistedSettings 的部分功能)
    private void InitializeSettings()
    {
        // 1. 載入並填充解析度 Dropdown
        LoadResolutionOptions();

        // 2. 載入持久化設定 (音量)
        LoadVolumeSettings();
        
        // 3. 載入解析度索引和全螢幕模式，並設定 Dropdown 的值
        
        // 解析度索引：讀取儲存值，如果沒有則使用預設值或當前系統解析度
        int savedResIndex = PlayerPrefs.GetInt(PREFS_RES_INDEX, GetDefaultResolutionIndex());
        
        // 全螢幕模式：讀取儲存值，如果沒有則使用 FullScreenWindow (0)
        int savedModeIndex = PlayerPrefs.GetInt(PREFS_FULLSCREEN_KEY, TARGET_DEFAULT_MODE_INDEX);

        // 設定 Dropdown 的值 (這會觸發 Listener，但我們在 Start() 結尾手動呼叫 ApplyCurrentSettings)
        if (resolutionDropdown != null)
        {
            resolutionDropdown.value = savedResIndex;
            resolutionDropdown.RefreshShownValue();
        }
        if (fullScreenDropdown != null)
        {
            fullScreenDropdown.value = savedModeIndex;
            fullScreenDropdown.RefreshShownValue();
        }
        
        // 初始化 currentFullScreenMode 變數 (確保在 ApplyCurrentSettings 前有值)
        SetFullscreenMode(savedModeIndex, false); // 第二個參數設為 false，避免在初始化階段重複應用
    }

    // 輔助方法：獲取預設或當前系統解析度索引
    private int GetDefaultResolutionIndex()
    {
        if (availableResolutions == null) return TARGET_DEFAULT_RES_INDEX;
        
        // 優先尋找與當前螢幕匹配的解析度
        for (int i = 0; i < availableResolutions.Length; i++)
        {
            if (availableResolutions[i].width == Screen.currentResolution.width &&
                availableResolutions[i].height == Screen.currentResolution.height)
            {
                return i;
            }
        }
        // 如果找不到，則返回預設的 1920x1080 索引
        return TARGET_DEFAULT_RES_INDEX; 
    }

    // 輔助方法：載入解析度選項並填充 Dropdown
    private void LoadResolutionOptions()
    {
        if (resolutionDropdown == null) return;
        
        // 獲取所有可用的解析度 (去重)
        availableResolutions = Screen.resolutions
            .Select(res => new Resolution { width = res.width, height = res.height })
            .Distinct()
            .ToArray();

        resolutionDropdown.ClearOptions();
        var options = availableResolutions.Select(res => $"{res.width} x {res.height}").ToList();

        resolutionDropdown.AddOptions(options);
        
        // 綁定 Dropdown 的值改變事件 (使用新的統一應用方法)
        resolutionDropdown.onValueChanged.AddListener(SetResolutionIndex);
        if (fullScreenDropdown != null)
        {
            fullScreenDropdown.onValueChanged.AddListener(SetFullscreenMode);
        }   
    }

    // 2. 應用解析度索引 (綁定到 resolutionDropdown 的 OnValueChanged 事件)
    public void SetResolutionIndex(int resolutionIndex)
    {
        // 儲存索引
        PlayerPrefs.SetInt(PREFS_RES_INDEX, resolutionIndex);
        PlayerPrefs.Save();
        
        // 應用設定
        ApplyCurrentSettings();
    }

    // 3. 全螢幕模式切換 (綁定到 fullScreenDropdown 的 OnValueChanged 事件)
    public void SetFullscreenMode(int modeIndex)
    {
        SetFullscreenMode(modeIndex, true); // 設置並立即應用
    }

    // 輔助方法：設置 FullScreenMode 變數
    private void SetFullscreenMode(int modeIndex, bool applySettings)
    {
        // 1. 根據索引確定 FullScreenMode
        if (modeIndex == 0)
        {
            currentFullScreenMode = FullScreenMode.FullScreenWindow; // 無邊框全螢幕
        }
        else // modeIndex == 1
        {
            currentFullScreenMode = FullScreenMode.Windowed; // 標準視窗化
        }

        // 2. 儲存選擇
        PlayerPrefs.SetInt(PREFS_FULLSCREEN_KEY, modeIndex);
        PlayerPrefs.Save();

        // 3. 應用變更 (只在 Dropdown 被使用者操作時執行，避免初始化時重複)
        if (applySettings)
        {
            ApplyCurrentSettings();
        }
    }
    
    // 4. 統一應用所有螢幕設定的核心方法
    private void ApplyCurrentSettings()
    {
        // 確保引用和列表不為空
        if (availableResolutions == null || resolutionDropdown == null || fullScreenDropdown == null) return;

        // 獲取當前解析度
        int resIndex = resolutionDropdown.value;
        if (resIndex < 0 || resIndex >= availableResolutions.Length)
        {
            Debug.LogError("解析度索引超限！");
            return;
        }
        Resolution resolution = availableResolutions[resIndex];
        
        // 獲取當前模式文字 (用於 Log)
        string modeText = GetFullscreenModeText(fullScreenDropdown.value);

        // 應用解析度
        Screen.SetResolution(resolution.width, resolution.height, currentFullScreenMode);

        Debug.Log($"應用設定: {resolution.width} x {resolution.height}, 模式: {modeText}");
    }
    
    // 輔助方法：獲取 FullScreenMode 的文字名稱 (用於 Debug.Log)
    private string GetFullscreenModeText(int modeIndex)
    {
        if (fullScreenDropdown != null && modeIndex >= 0 && modeIndex < fullScreenDropdown.options.Count)
        {
            return fullScreenDropdown.options[modeIndex].text;
        }
        return currentFullScreenMode.ToString(); // 如果 Dropdown 不存在，則直接返回 enum 名稱
    }
    
    // ===================================================================
    // D. 載入初始設定 (音量)
    // ===================================================================
    private void LoadVolumeSettings()
    {
        // 這裡需要知道您所有暴露的音量參數名稱
        string[] volumeParams = { "MasterVolume", "MusicVolume", "SFXVolume" };
        Slider[] sliders = { masterSlider, musicSlider, sfxSlider }; // 與上面數組順序匹配

        for (int i = 0; i < volumeParams.Length; i++)
        {
            string paramName = volumeParams[i];
            Slider slider = sliders[i];
        
            // 預設音量值為 1 (0dB)
            float savedVolume = PlayerPrefs.GetFloat(paramName, 1f);
        
            // 應用到 Mixer
            mainMixer.SetFloat(paramName, GetVolumeDB(savedVolume));
        
            // **設定滑塊的初始值**
            if (slider != null) 
            { 
                slider.value = savedVolume; 
            }
        }
        if (masterSlider != null)
        {
            // 綁定到新的輔助函式
            masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        }
        if (musicSlider != null)
        {
            musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }
        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.AddListener(OnSfxVolumeChanged);
        }
    }
    public void OnMasterVolumeChanged(float volume)
    {
        // 調用核心邏輯，傳入正確的 Mixer 參數名稱
        SetVolume("MasterVolume", volume);
    }

    // 針對 Music Slider 的綁定
    public void OnMusicVolumeChanged(float volume)
    {
        SetVolume("MusicVolume", volume);
    }

    // 針對 SFX Slider 的綁定
    public void OnSfxVolumeChanged(float volume)
    {
        SetVolume("SFXVolume", volume);
    }
}