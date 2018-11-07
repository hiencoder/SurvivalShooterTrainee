using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour {
	private AudioMixer masterMixer;

	public void SetSfxLevels(float sfxLvl){
		masterMixer.SetFloat("sfxVol",sfxLvl);
	}

	public void SetSfxLevels(){

	}
	public void SetMusicLevels(float musicLvl){
		masterMixer.SetFloat("musicVol",musicLvl);
	}

	public void SetMusicLevels(){

	}
}
