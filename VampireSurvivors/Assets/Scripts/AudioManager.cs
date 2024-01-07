using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	[Header("BGM")] 
	public AudioClip bgmClip;
	public float bgmVolume;
	private AudioSource bgmPlayer;
	
	[Header("SFX")] 
	public AudioClip[] sfxClips;
	public float sfxVolume;
	public int channels;
	private AudioSource[] sfxPlayers;
	private int channelIndex;

	private void Awake() {
		instance = this;
		Init();
	}

	private void Init() {
		GameObject bgmObject = new GameObject("BgmPlayer");
		bgmObject.transform.parent = transform;
		bgmPlayer = bgmObject.AddComponent<AudioSource>();
		bgmPlayer.playOnAwake = false;
		bgmPlayer.loop = true;
		bgmPlayer.volume = bgmVolume;
		bgmPlayer.clip = bgmClip;

		GameObject sfxObject = new GameObject("SfxPlayer");
		sfxObject.transform.parent = transform;
		sfxPlayers = new AudioSource[channels];

		for (int i = 0; i < sfxPlayers.Length; i++) {
			sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
			sfxPlayers[i].playOnAwake = false;
			sfxPlayers[i].volume = sfxVolume;
		}
	}
}
