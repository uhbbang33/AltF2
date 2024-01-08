using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager
{
    private AudioClip _audioClip;
    private AudioSource _bgmSource;
    private AudioMixer _audioMixer;
    private string _bgFilename;

    public float MasterVolume
    {
        get
        {
            _audioMixer.GetFloat("Master", out float volume);
            return volume;
        }
        set
        {
            _audioMixer.SetFloat("Master", value);
        }
    }
    public float BGMVolume
    {
        get
        {
            _audioMixer.GetFloat("BGM", out float volume);
            return volume;
        }
        set
        {
            _audioMixer.SetFloat("BGM", value);
        }
    }
    public float SFXVolume
    {
        get
        {
            _audioMixer.GetFloat("SFX", out float volume);
            return volume;
        }
        set
        {
            _audioMixer.SetFloat("SFX", value);
        }
    }

    public GameObject Root
    {
        get
        {
            var root = GameObject.Find("@Sound_Root");
            if (root == null)
            {
                root = new GameObject("@Sound_Root");
                Object.DontDestroyOnLoad(root);
            }
            else
            {
                Object.DontDestroyOnLoad(root);
            }

            return root;
        }
    }

    public void Init()
    {
        var go = new GameObject("@BGM");
        _bgmSource = go.AddComponent<AudioSource>();
        go.transform.parent = Root.transform;

        _audioMixer = Resources.Load<AudioMixer>("F2Mixwer");

        SceneManager.sceneLoaded += LoadedsceneEvent;
        BgSoundPlay("BG1", 0.05f);
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode arg1)
    {
        if (scene.buildIndex == 1)
        {
            _bgFilename = "BG1";
        }
        else if (scene.buildIndex == 2)
        {
            _bgFilename = "BG3";
        }
        BgSoundPlay(_bgFilename, 0.05f);
    }

    public void SFXPlay(string sfxName, Vector3 audioPosition, float audioVolume)
    {
        GameObject AudioGo = new GameObject(sfxName + "Sound");
        AudioSource audiosource = AudioGo.AddComponent<AudioSource>();

        audiosource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("SFX")[0];
        _audioClip = Resources.Load<AudioClip>("Audios/SFX/"+sfxName);
        if (_audioClip!=null) 
        {
            audiosource.clip = _audioClip;
            audiosource.Play();

            Object.Destroy(audiosource.gameObject, audiosource.clip.length);
        }        
    }

    public void BgSoundPlay(string BgName, float audioVolume)
    {
        _bgmSource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("BGM")[0];
        _audioClip = Resources.Load<AudioClip>("Audios/BGM/"+ BgName);
        _bgmSource.clip = _audioClip;        
        _bgmSource.loop = true;
        _bgmSource.Play();
    }
}