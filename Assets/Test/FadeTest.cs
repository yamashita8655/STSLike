using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTest : MonoBehaviour
{
    [SerializeField]
    private FadeObject fadeObj = null;
    
    float time = 0f;
    void Start() {
		SoundManager.Instance.Initialize();
	}

    void Update()
    {
        time += Time.deltaTime / 2f;
        if (time >= 1f) {
            time = 0f;
        }
        fadeObj.UpdateMaskCutout(time);
    }

	public void OnClickBgm1Play() {
		//SoundManager.Instance.PlayBgm(SoundManager.Bgm.Title);
		//SoundManager.Instance.PlaySe(Enum.Se.ButtonOk);
	}

	public void OnClickBgm2Play() {
		//SoundManager.Instance.PlayBgm(SoundManager.Bgm.Home);
		//SoundManager.Instance.PlaySe(Enum.Se.ButtonCancel);
	}
	
	public void OnClickChangeSnapShot() {
	}

	public void OnClickBgmStop() {
		//SoundManager.Instance.StopBgm();
	}
	public void OnClickBgmPauseResume() {
		//SoundManager.Instance.StopBgm();
	}
	
	public void OnClickSePlay() {
		//SoundManager.Instance.PlaySe();
	}
}
