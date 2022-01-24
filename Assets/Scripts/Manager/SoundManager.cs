// このファイルは、Tools/CreateSoundManager/create_soundmanager.pyによって自動生成されるため、
// 直接の編集は禁止です。
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : SimpleMonoBehaviourSingleton<SoundManager> {
	
	private List<AudioSource> BgmAudioSources = null;
	private List<AudioSource> SeAudioSources = null;
	private List<AudioClip> BgmAudioClipList = null;
	private List<AudioClip> SeAudioClipList = null;

	private List<AudioMixerSnapshot> AudioMixerSnapshot_BgmCrossFade = null;
	
	// とりあえず、一個しか使わないのであれば、これ使っておけば良いと思われ
	private AudioMixer SoundAudioMixer = null;

	private EnumSelf.eBgm CurrentBgm = EnumSelf.eBgm.None;
	private int BgmAudioUseIndex = 0;

	private int SeAudioUseIndex = 0;

	private int LoadCount = 0; 
	private int LoadedCount = 0; 

	// TODO 現状は、最初に全てのBGM/SEをキャッシュしているが、
	// 処理が重くなったら、都度読み込みなど、読み込む量を調整する
	public IEnumerator CoInitialize() {
		BgmAudioSources = new List<AudioSource>();
		for (int i = 0; i < 2; i++) {
			BgmAudioSources.Add(null);
		}

		LoadCount = 0;
		LoadedCount = 0;

		LoadCount++;
		ResourceManager.Instance.RequestExecuteOrder(
			"Prefab/Sound/BgmAudioSource1",
			ExecuteOrder.Type.GameObject,
			this.gameObject,
			(audioSource) => {
				GameObject obj = GameObject.Instantiate(audioSource) as GameObject;
				obj.transform.SetParent(this.gameObject.transform);
			
				BgmAudioSources[0] = obj.GetComponent<AudioSource>();
				LoadedCount++;
			}
		);
		
		LoadCount++;
		ResourceManager.Instance.RequestExecuteOrder(
			"Prefab/Sound/BgmAudioSource2",
			ExecuteOrder.Type.GameObject,
			this.gameObject,
			(audioSource) => {
				GameObject obj = GameObject.Instantiate(audioSource) as GameObject;
				obj.transform.SetParent(this.gameObject.transform);
			
				BgmAudioSources[1] = obj.GetComponent<AudioSource>();
				LoadedCount++;
			}
		);
		
		SeAudioSources = new List<AudioSource>();
		for (int i = 0; i < 4; i++) {
			SeAudioSources.Add(null);
		}
		
		LoadCount++;
		ResourceManager.Instance.RequestExecuteOrder(
			"Prefab/Sound/SeAudioSource1",
			ExecuteOrder.Type.GameObject,
			this.gameObject,
			(audioSource) => {
				GameObject obj = GameObject.Instantiate(audioSource) as GameObject;
				obj.transform.SetParent(this.gameObject.transform);
			
				SeAudioSources[0] = obj.GetComponent<AudioSource>();
				LoadedCount++;
			}
		);
		
		LoadCount++;
		ResourceManager.Instance.RequestExecuteOrder(
			"Prefab/Sound/SeAudioSource2",
			ExecuteOrder.Type.GameObject,
			this.gameObject,
			(audioSource) => {
				GameObject obj = GameObject.Instantiate(audioSource) as GameObject;
				obj.transform.SetParent(this.gameObject.transform);
			
				SeAudioSources[1] = obj.GetComponent<AudioSource>();
				LoadedCount++;
			}
		);
		
		LoadCount++;
		ResourceManager.Instance.RequestExecuteOrder(
			"Prefab/Sound/SeAudioSource3",
			ExecuteOrder.Type.GameObject,
			this.gameObject,
			(audioSource) => {
				GameObject obj = GameObject.Instantiate(audioSource) as GameObject;
				obj.transform.SetParent(this.gameObject.transform);
			
				SeAudioSources[2] = obj.GetComponent<AudioSource>();
				LoadedCount++;
			}
		);
		
		LoadCount++;
		ResourceManager.Instance.RequestExecuteOrder(
			"Prefab/Sound/SeAudioSource4",
			ExecuteOrder.Type.GameObject,
			this.gameObject,
			(audioSource) => {
				GameObject obj = GameObject.Instantiate(audioSource) as GameObject;
				obj.transform.SetParent(this.gameObject.transform);
			
				SeAudioSources[3] = obj.GetComponent<AudioSource>();
				LoadedCount++;
			}
		);

        BgmAudioClipList = new List<AudioClip>();
        for (int i = 0; i < (int)EnumSelf.eBgm.Max; i++) {
			BgmAudioClipList.Add(null);
		}

		// これは、EnumSelf.Bgmの並びと揃える
		List<string> bgmList = new List<string>() {
			"Sound/BGM/Game/GameBgm1",
			"Sound/BGM/Home/HomeBgm"
		};
		
		for (int i = 0; i < bgmList.Count; i++) {
			LoadCount++;
			int index = i;
			ResourceManager.Instance.RequestExecuteOrder(
				bgmList[index],
				ExecuteOrder.Type.AudioClip,
				this.gameObject,
				(audioClip) => {
					BgmAudioClipList[index] = audioClip as AudioClip;
					LoadedCount++;
				}
			);
		}

        SeAudioClipList = new List<AudioClip>();
        for (int i = 0; i < (int)EnumSelf.eSe.Max; i++) {
			SeAudioClipList.Add(null);
		}
		
		// これは、EnumSelf.Seの並びと揃える
		List<string> seList = new List<string>() {
			"Sound/SE/SE/TapSe"
		};
		
		for (int i = 0; i < seList.Count; i++) {
			LoadCount++;
			int index = i;
			ResourceManager.Instance.RequestExecuteOrder(
				seList[index],
				ExecuteOrder.Type.AudioClip,
				this.gameObject,
				(audioClip) => {
					SeAudioClipList[index] = audioClip as AudioClip;
					LoadedCount++;
				}
			);
		}

        AudioMixerSnapshot_BgmCrossFade = new List<AudioMixerSnapshot>();
        for (int i = 0; i < 2; i++) {
            AudioMixerSnapshot_BgmCrossFade.Add(null);
        }

		LoadCount++;
        ResourceManager.Instance.RequestExecuteOrder(
			"Sound/AudioMixer",
			ExecuteOrder.Type.AudioMixer,
			this.gameObject,
			(audioMixer) => {
				SoundAudioMixer = audioMixer as AudioMixer;
                AudioMixerSnapshot_BgmCrossFade[0] = SoundAudioMixer.FindSnapshot("SnapshotBgm1");
                AudioMixerSnapshot_BgmCrossFade[1] = SoundAudioMixer.FindSnapshot("SnapshotBgm2");
				LoadedCount++;
            }
        );

		CurrentBgm = EnumSelf.eBgm.None;
		BgmAudioUseIndex = 0;
		SeAudioUseIndex = 0;

		while (LoadedCount < LoadCount) {
			yield return null;
		}
	}

	public void PlayBgm(EnumSelf.eBgm bgm, float fadeTime = 0f) {
		if (bgm == CurrentBgm) {
			return;
		}

		int currentIndex = BgmAudioUseIndex;
		BgmAudioUseIndex++;
		if (BgmAudioUseIndex >= BgmAudioSources.Count) {
			BgmAudioUseIndex = 0;
		}
		int nextIndex = BgmAudioUseIndex;

		AudioSource source = GetBgmAudioSource(nextIndex);
		source.clip = BgmAudioClipList[(int)bgm];
		source.Play();
		source.loop = true;
		CurrentBgm = bgm;
		
		float[] weights;
		if (BgmAudioUseIndex == 0) {
			weights = new float[2] { 1.0f, 0.0f };
		} else {
			weights = new float[2] { 0.0f, 1.0f };
		}
		SoundAudioMixer.TransitionToSnapshots(AudioMixerSnapshot_BgmCrossFade.ToArray(), weights, fadeTime);
	}
	
	public void StopBgm(float fadeTime = 0f) {
		BgmAudioUseIndex++;
		if (BgmAudioUseIndex >= BgmAudioSources.Count) {
			BgmAudioUseIndex = 0;
		}
		int nextIndex = BgmAudioUseIndex;

		AudioSource source = GetBgmAudioSource(nextIndex);
		source.Stop();
		CurrentBgm = EnumSelf.eBgm.None;
		
		float[] weights;
		if (BgmAudioUseIndex == 0) {
			weights = new float[2] { 1.0f, 0.0f };
		} else {
			weights = new float[2] { 0.0f, 1.0f };
		}
		SoundAudioMixer.TransitionToSnapshots(AudioMixerSnapshot_BgmCrossFade.ToArray(), weights, fadeTime);
	}

    public AudioSource GetBgmAudioSourceForCheck()
    {
        AudioSource source = BgmAudioSources[BgmAudioUseIndex];
        return source;
    }

    private AudioSource GetBgmAudioSource(int sourceIndex) {
		AudioSource source = BgmAudioSources[sourceIndex];
		return source;
	}
	
	private AudioSource GetSeAudioSource(int sourceIndex) {
		AudioSource source = SeAudioSources[sourceIndex];
		return source;
	}

	public void PlaySe(EnumSelf.eSe se) {
		int currentIndex = SeAudioUseIndex;
		SeAudioUseIndex++;
		if (SeAudioUseIndex >= SeAudioSources.Count) {
			SeAudioUseIndex = 0;
		}
		int nextIndex = SeAudioUseIndex;

		AudioSource source = GetSeAudioSource(nextIndex);
		source.clip = SeAudioClipList[(int)se];
		source.Play();
	}
	public void SetSeMuteFlag(bool isMute) {
		for (int i = 0; i < SeAudioSources.Count; i++) {
			SeAudioSources[i].mute = isMute;
		}
	}
	
	public void SetBgmMuteFlag(bool isMute) {
		for (int i = 0; i < BgmAudioSources.Count; i++) {
			BgmAudioSources[i].mute = isMute;
		}
	}
	
	public void SetSeVolume(float volume) {
		for (int i = 0; i < SeAudioSources.Count; i++) {
			SeAudioSources[i].volume = volume;
		}
	}
	
	public void SetBgmVolume(float volume) {
		for (int i = 0; i < BgmAudioSources.Count; i++) {
			BgmAudioSources[i].volume = volume;
		}
	}
}
