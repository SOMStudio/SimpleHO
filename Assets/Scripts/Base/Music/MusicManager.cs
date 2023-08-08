using System.Collections.Generic;
using UnityEngine;

namespace Base.Music
{
	[AddComponentMenu("Base/Music Manager")]
	public class MusicManager : MonoBehaviour
	{
		[SerializeField] protected List<MusicClipManager> musicList;

		[System.NonSerialized] public static MusicManager Instance;

		private void Awake()
		{
			Init();
		}

		private void Start()
		{
			DontDestroyOnLoad(gameObject);
		}

		private void Init()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else if (Instance != this)
			{
				Destroy(gameObject);
			}
		}

		public void UpdateVolume()
		{
			foreach (MusicClipManager item in musicList)
			{
				item.UpdateVolume();
			}
		}

		public void StopMusic(int value)
		{
			MusicClipManager temp = musicList[value];

			if (temp)
			{
				temp.StopMusic();
			}
		}

		public void PlayMusic(int value)
		{
			MusicClipManager temp = musicList[value];

			if (temp)
			{
				temp.PlayMusic();
			}
		}

		public void PlayMusicStopAnother(int value)
		{
			for (int i = 0; i < musicList.Count; i++)
			{
				if (i != value)
				{
					if (musicList[i].IsPlaying())
					{
						StopMusic(i);
					}
				}
				else
				{
					PlayMusic(i);
				}
			}
		}

		public void PlayMenuMusic()
		{
			PlayMusicStopAnother(0);
		}

		public void PlayLevelMusic()
		{
			PlayMusicStopAnother(1);
		}
	}
}
