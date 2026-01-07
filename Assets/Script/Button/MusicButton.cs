// 將這段掛在 UI 按鈕上
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{   //暫停開始 開關
    public Sprite  Start_icon;
    public Sprite  Pause_icon;
    public Image buttonImage;
    public MusicPlayer musicPlayer;
    public GameObject musicList;
    public Transform contentParent;
    public int index = 0;
    void Start()
    {
        buttonImage = GetComponent<Image>();
        
    }
    public void OnPauseClick(){
        bool isRunning = musicPlayer.ToggleMusic();
        buttonImage.sprite = isRunning ? Pause_icon : Start_icon;
    }
    public void OnNextClick()=>musicPlayer.NextTrack();
    public void OnBackClick()=>musicPlayer.BackTrack();
    public void OnMusicNameClick() => musicPlayer.TrackList();

    public void OnMusicSelect() {
        musicPlayer.PlayTrack(index);
        musicList.SetActive(false);
    }
    
}
