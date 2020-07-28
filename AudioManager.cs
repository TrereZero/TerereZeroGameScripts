using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Static Instance
  private static AudioManager instance;
  public static AudioManager Instance {
      
    get{
        if (instance == null){
            instance = FindObjectOfType<AudioManager>();
            if(instance == null){
                instance = new GameObject("Spawned AudioManager",typeof(AudioManager)).GetComponent<AudioManager>();
            }
        }
        return instance;
    }

    private set{
        instance = value;
    }
  
  }
  #endregion

  #region Fields
    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource sfxSource;

    private AudioSource loopSfxSource;

    private AudioMixer audioMixer;

    private float musicVolume;

    private bool firstMusicSourceIsPlaying;

  #endregion


    private void Awake()
    {
        audioMixer = Resources.Load<AudioMixer>("MainAudioMixer");
        //garantir que a instancia do AudioManager Não seja destruída
        DontDestroyOnLoad(this.gameObject);

        //Cria os AudioSources programaticamente e os adiciona como referências
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();
        loopSfxSource = this.gameObject.AddComponent<AudioSource>();


        //faz o loop das musicas
        musicSource.loop = true;
        musicSource2.loop = true;
        loopSfxSource.loop = true;

        musicSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
        musicSource2.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];

        musicVolume = musicSource.volume;

    }

public void PlayMusic(AudioClip musicClip){

    //determina qual music source está ativo no momento
    AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;

    activeSource.clip = musicClip;
    activeSource.volume = 1;
    activeSource.Play();
}

public void PlayMusicWithFade(AudioClip newCLip, float transitionTIme = 1f){
    AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
    StartCoroutine(UpdateMusicWithFade(activeSource, newCLip, transitionTIme));
}

public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTIme = 1f){
    //DeterMina Qual source está ativo
    AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
    AudioSource newSource = (firstMusicSourceIsPlaying) ? musicSource2 : musicSource;


    //troca o audioSource
    firstMusicSourceIsPlaying = !firstMusicSourceIsPlaying;

    //determina os campos do audiosource e chama a corotina do crossfade
    newSource.clip = musicClip;
    newSource.Play();
    StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTIme));
}


private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newCLip, float transitionTIme){
    //Garantir que a fonte de áudio está ativa e tocando
    if(!activeSource.isPlaying){
        activeSource.Play();
    }

    float t = 0f;
    //Fade out
    for (t = 0; t < transitionTIme; t+= Time.deltaTime)
    {
        activeSource.volume = (1- (t/transitionTIme));
        yield return null;
    }

    activeSource.Stop();
    activeSource.clip = newCLip;
    activeSource.Play();    

    //Fade in
    for (t = 0; t < transitionTIme; t+= Time.deltaTime)
    {
        activeSource.volume = (t/transitionTIme);
        yield return null;
    }



}

private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTIme){
    float t = 0f;

    //Fade out
    for (t = 0; t <= transitionTIme; t+= Time.deltaTime)
    {
        original.volume = (musicVolume- (t/transitionTIme) * musicVolume);
        newSource.volume = (t/transitionTIme) * musicVolume;
        yield return null;
    }

    original.Stop(); 
}


public void PlaySfx(AudioClip clip){
    sfxSource.PlayOneShot(clip);
}

public void PlaySfx(AudioClip clip, float volume){
    sfxSource.PlayOneShot(clip, volume);

}

public void PlayLoopSfx(AudioClip clip){
    
    loopSfxSource.clip = clip;
    loopSfxSource.Play();
}

public void PlayLoopSfx(AudioClip clip, float volume){
    
    loopSfxSource.clip = clip;
    loopSfxSource.volume = volume;
    loopSfxSource.Play();
}

public void StopLoopSfx(){
    loopSfxSource.Stop();
}



public void setMusicVolume(float volume){
    musicSource.volume = volume;
    musicSource2.volume = volume;
}

public void setSfxVolume(float volume){
    sfxSource.volume = volume;
}

}