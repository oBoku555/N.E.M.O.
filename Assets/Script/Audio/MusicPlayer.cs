using UnityEngine;
using UnityEngine.UI;

using System.Linq;
using TMPro;

public class MusicPlayer : MonoBehaviour
{
    public Sprite Pause_icon;
    public Sprite Start_icon;
    public Image pauseMusicButtonImage;
    public AudioSource audioSource;  // 指定 Audio Source
    public AudioClip[] musicTracks;  // 多首音樂
    public TMP_Text musicName;
    public GameObject musicNameButton;
    public GameObject musicList;
    public GameObject bigMusicList;
    public MusicPlayer MP;
    public Transform contentParent;
    public Transform bigContentParent;
    private int currentTrack = 0;
    private bool isPlaying;
    void Start()
    {
        SetTrack(currentTrack);
        
    }
    public void SetTrack(int index){
        if (index < 0 || index >= musicTracks.Length) return;

        audioSource.clip = musicTracks[index];
        musicName.text = musicTracks[index].name;
        pauseMusicButtonImage.sprite = Start_icon;
    }
    public void PlayTrack(int index)
    {
        if (index < 0 || index >= musicTracks.Length) return;

        audioSource.clip = musicTracks[index];
        audioSource.Play();
        musicName.text = musicTracks[index].name;
        pauseMusicButtonImage.sprite = Pause_icon;
    }

    public bool ToggleMusic()
    {
        if (audioSource.isPlaying)
            audioSource.Pause();
        else
            audioSource.Play();
        isPlaying = audioSource.isPlaying;
        return isPlaying;
    }
    public void PauseMusic()
    {
        pauseMusicButtonImage.sprite = Start_icon;
        audioSource.Pause();
    }

    public void NextTrack()
    {
        currentTrack = (currentTrack + 1) % musicTracks.Length;
        PlayTrack(currentTrack);
    }
    public void BackTrack()
    {
        currentTrack = (currentTrack - 1) % musicTracks.Length;
        if(currentTrack < 0){
            currentTrack = musicTracks.Length - 1;
        }
        PlayTrack(currentTrack);
    }
    void PopulateMusicList(GameObject targetPanel, Transform targetParent)
    {
        foreach (Transform child in targetParent)
            Destroy(child.gameObject);

        targetPanel.SetActive(true);

        int index = 0;
        foreach (var clip in musicTracks)
        {
            GameObject btnObj = Instantiate(musicNameButton, targetParent);
            btnObj.GetComponentInChildren<TMP_Text>().text = clip.name;

            MusicButton mb = btnObj.GetComponent<MusicButton>();
            mb.musicPlayer = MP;
            mb.musicList = targetPanel;
            mb.index = index;

            index++;
        }
    }
    public void TrackList() {
        if (musicList.activeSelf)
            musicList.SetActive(false);
        else
            PopulateMusicList(musicList, contentParent);

    }
    public void OnBigMusicButtonClick()
    {
        PopulateMusicList(bigMusicList, bigContentParent);
    }
}
