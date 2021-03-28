using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : BestPracticeSingleton<SoundManager> {
	
	[SerializeField]
	private AudioSource[] BgmAudioSource = null;

	[SerializeField]
	private AudioSource[] SeAudioSource = null;
	
	[SerializeField]
	private AudioClip[] BgmAudioClipList = null;
	
	[SerializeField]
	private AudioClip[] SeAudioClipList = null;

	[SerializeField]
	private AudioMixerSnapshot[] AudioMixerSnapshot_BgmCrossFade = null;
	
	// とりあえず、一個しか使わないのであれば、これ使っておけば良いと思われ
	[SerializeField]
	private AudioMixer SoundAudioMixer = null;

	private Enum.Bgm CurrentBgm = Enum.Bgm.None;
	private int BgmAudioUseIndex = 0;

	private int SeAudioUseIndex = 0;
	
	
	public void Initialize() {
		// ここのチェックは、開発中だけでいい
#if UNITY_EDITOR
		if (BgmAudioSource == null) {
			Debug.Log("SerializeFieldResourceManager:BgmAudioSource error");
		}
		
		if (SeAudioSource == null) {
			Debug.Log("SerializeFieldResourceManager:SeAudioSource error");
		}
#endif
		CurrentBgm = Enum.Bgm.None;
		BgmAudioUseIndex = 0;
		SeAudioUseIndex = 0;
	}

	public void PlayBgm(Enum.Bgm bgm, float fadeTime = 0f) {
		if (bgm == CurrentBgm) {
			return;
		}

		int currentIndex = BgmAudioUseIndex;
		BgmAudioUseIndex++;
		if (BgmAudioUseIndex >= BgmAudioSource.Length) {
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
		SoundAudioMixer.TransitionToSnapshots(AudioMixerSnapshot_BgmCrossFade, weights, fadeTime);
	}
	
	public void StopBgm(float fadeTime = 0f) {
		BgmAudioUseIndex++;
		if (BgmAudioUseIndex >= BgmAudioSource.Length) {
			BgmAudioUseIndex = 0;
		}
		int nextIndex = BgmAudioUseIndex;

		AudioSource source = GetBgmAudioSource(nextIndex);
		source.Stop();
		CurrentBgm = Enum.Bgm.None;
		
		float[] weights;
		if (BgmAudioUseIndex == 0) {
			weights = new float[2] { 1.0f, 0.0f };
		} else {
			weights = new float[2] { 0.0f, 1.0f };
		}
		SoundAudioMixer.TransitionToSnapshots(AudioMixerSnapshot_BgmCrossFade, weights, fadeTime);
	}

	private AudioSource GetBgmAudioSource(int sourceIndex) {
		AudioSource source = BgmAudioSource[sourceIndex];
		return source;
	}
	
	private AudioSource GetSeAudioSource(int sourceIndex) {
		AudioSource source = SeAudioSource[sourceIndex];
		return source;
	}

	public void PlaySe(Enum.Se se) {
		int currentIndex = SeAudioUseIndex;
		SeAudioUseIndex++;
		if (SeAudioUseIndex >= SeAudioSource.Length) {
			SeAudioUseIndex = 0;
		}
		int nextIndex = SeAudioUseIndex;

		AudioSource source = GetSeAudioSource(nextIndex);
		source.clip = SeAudioClipList[(int)se];
		source.Play();
	}
	
	//public void StopBgm() {
	//	BgmAudioSource.Stop();
	//}
	//
	//public void MuteBgm() {
	//	BgmAudioSource.mute = true;
	//}

	//public void UnmuteBgm()
	//{
	//	BgmAudioSource.mute = false;
	//}

	//public void PlaySe() {
	//	SeAudioSource.Play();
	//}
	//
}
