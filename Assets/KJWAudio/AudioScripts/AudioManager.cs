using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class AudioManager : MonoBehaviour
{
    private AudioClip _audioClip;
    public AudioSource BgSound;
    public AudioSource[] SFXSound;
    public AudioMixer Mixer;
    private string _bgFilename;


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
        audiosource.clip = _audioClip;
        audiosource.volume = audioVolume;
        audiosource.Play();

        
        Destroy(audiosource.gameObject, audiosource.clip.length);
    }

    //GMTest.Instance.audioManager.SFXPlay("die", AudioClip);

    public void BgSoundPlay(string BgName, float audioVolume)
    {
        _audioClip = Resources.Load<AudioClip>("Audios/BGM/"+ BgName);
        BgSound.clip = _audioClip;
        BgSound.outputAudioMixerGroup = Mixer.FindMatchingGroups("BGSound")[0];
        BgSound.loop = true;
        BgSound.volume = audioVolume;
        BgSound.Play();
    }

    //GMTest.Instance.audioManager.BgSoundPlay(AudioClip);

    //mix º¼·ý Á¶Àý
    public void BGSoundVolume(float value) 
    {
        Mixer.SetFloat("BGSoundVolume", Mathf.Log10(value) * 20);
    }

}