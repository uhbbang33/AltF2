using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private AudioClip _audioClip;
    public AudioSource BgSound;
    public AudioMixer Mixer;
    private string _bgFilename;

    public Slider _bgmSlider;
    public Slider _sfxSlider;
    public Slider _masterSlider;


    private void Awake()
    {
        _audioClip = GetComponent<AudioClip>();
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
        
        BgSoundPlay("BG1", 0.05f);
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode arg1)
    {
        if (scene.name == "KDH_Obstacle")
        {
            _bgFilename = "BG1";
        }
        else if (scene.name == "99.BJH")
        {
            _bgFilename = "BG3";
        }
        BgSoundPlay(_bgFilename, 0.05f);
    }

    public void SFXPlay(string sfxName, Vector3 audioPosition, float audioVolume)
    {
        GameObject AudioGo = new GameObject(sfxName + "Sound");
        AudioSource audiosource = AudioGo.AddComponent<AudioSource>();

        audiosource.outputAudioMixerGroup = Mixer.FindMatchingGroups("SFX")[0];
        _audioClip = Resources.Load<AudioClip>("Audios/SFX/"+sfxName);
        if (_audioClip!=null) 
        {
            audiosource.clip = _audioClip;
            audiosource.volume = audioVolume;
            audiosource.Play();

            Destroy(audiosource.gameObject, audiosource.clip.length);
        }
        
    }

    public void BgSoundPlay(string BgName, float audioVolume)
    {
        _audioClip = Resources.Load<AudioClip>("Audios/BGM/"+ BgName);
        BgSound.clip = _audioClip;
        BgSound.outputAudioMixerGroup = Mixer.FindMatchingGroups("BGSound")[0];
        BgSound.loop = true;
        BgSound.volume = audioVolume;
        BgSound.Play();
    }

    //º¼·ýÁ¶Àý
    public void BGSoundVolume() 
    {
        float bgmsound = _bgmSlider.value;
        Mixer.SetFloat("BGVolume", bgmsound);
    }
    public void SFXSoundVolume()
    {
        float sfxsound = _sfxSlider.value;
        Mixer.SetFloat("SFXVolume", sfxsound);
    }
    public void MasterVolume()
    {
        float mastersound = _masterSlider.value;
        Mixer.SetFloat("Master", mastersound);
    }
}