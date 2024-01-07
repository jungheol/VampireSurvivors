using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

	public enum Sfx {
		Dead,
		Hit,
		LevelUp = 3,
		Lose,
		Melee,
		Range = 7,
		Select,
		Win
	}
	
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

	public void PlayBgm(bool isPlay) {
		if (isPlay) {
			bgmPlayer.Play();
		} else {
			bgmPlayer.Stop();
		}
	}

	public void PlaySfx(Sfx sfx) {
		for (int i = 0; i < sfxPlayers.Length; i++) {
			int loopIndex = (i + channelIndex) % sfxPlayers.Length;
			
			if (sfxPlayers[loopIndex].isPlaying) continue;

			int ranIndex = 0;
			if (sfx == Sfx.Hit || sfx == Sfx.Melee) {
				ranIndex = Random.Range(0, 2);
			}

			channelIndex = loopIndex;
			sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + ranIndex];
			sfxPlayers[loopIndex].Play();
			break;
		}
	}
}
