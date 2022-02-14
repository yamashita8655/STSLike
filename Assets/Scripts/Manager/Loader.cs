using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Loader
{
    public Loader()
    {
    }

    // 同じファイルを読み込むときは、必ず最初の読み込みが終わってから呼び出す事
    public IEnumerator LoadSprite(string pathAndName, Action<UnityEngine.Object> endCallback)
    {
        // リソースの非同期読込開始
        ResourceRequest resReq = Resources.LoadAsync<Sprite>(pathAndName);

        // 終わるまで待つ
        while (resReq.isDone == false) {
            yield return null;
        }

        if (resReq.asset == null) {
            LogManager.Instance.LogError("Loader:LoadSprite:resReq.asset == null : " + pathAndName);
        }

        endCallback(resReq.asset);
    }

    public IEnumerator LoadGameObject(string pathAndName, Action<UnityEngine.Object> endCallback)
    {
        // リソースの非同期読込開始
        ResourceRequest resReq = Resources.LoadAsync<GameObject>(pathAndName);
        
        // 終わるまで待つ
        while (resReq.isDone == false) {
            yield return null;
        }

        if (resReq.asset == null) {
            LogManager.Instance.LogError("Loader:LoadGameObject:resReq.asset == null : " + pathAndName);
        }

        endCallback(resReq.asset);
    }
    
	public IEnumerator LoadAudioClip(string pathAndName, Action<UnityEngine.Object> endCallback)
    {
        // リソースの非同期読込開始
        ResourceRequest resReq = Resources.LoadAsync<AudioClip>(pathAndName);
        
        // 終わるまで待つ
        while (resReq.isDone == false) {
            yield return null;
        }

        if (resReq.asset == null) {
            LogManager.Instance.LogError("Loader:LoadAudioClip:resReq.asset == null : " + pathAndName);
        }

        endCallback(resReq.asset);
    }
	
	public IEnumerator LoadAudioMixer(string pathAndName, Action<UnityEngine.Object> endCallback)
    {
        // リソースの非同期読込開始
        ResourceRequest resReq = Resources.LoadAsync<AudioMixer>(pathAndName);
        
        // 終わるまで待つ
        while (resReq.isDone == false) {
            yield return null;
        }

        if (resReq.asset == null) {
            LogManager.Instance.LogError("Loader:LoadAudioMixer:resReq.asset == null : " + pathAndName);
        }

        endCallback(resReq.asset);
    }
	
    public IEnumerator LoadTextAsset(string pathAndName, Action<UnityEngine.Object> endCallback)
    {
        // リソースの非同期読込開始
        ResourceRequest resReq = Resources.LoadAsync<TextAsset>(pathAndName);
        
        // 終わるまで待つ
        while (resReq.isDone == false) {
            yield return null;
        }

        if (resReq.asset == null) {
            LogManager.Instance.LogError("Loader:LoadTextAsset:resReq.asset == null : " + pathAndName);
        }

        endCallback(resReq.asset);
    }
	
	public IEnumerator Load(string pathAndName, Action<UnityEngine.Object> endCallback)
    {
        // リソースの非同期読込開始
        ResourceRequest resReq = Resources.LoadAsync(pathAndName);
        
        // 終わるまで待つ
        while (resReq.isDone == false) {
            yield return null;
        }

        if (resReq.asset == null) {
            LogManager.Instance.LogError("Loader:Load:resReq.asset == null : " + pathAndName);
        }

        endCallback(resReq.asset);
    }
}
