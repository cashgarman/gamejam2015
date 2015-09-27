using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
	[Serializable] public class Sound
	{
		public string name;
		public AudioClip clip;
	}

	public class Sounds : MonoBehaviour
	{
		public static Sounds instance;

		public List<Sound> sounds;
		public AudioSource source;

		public void Awake()
		{
			instance = this;
		}

		public static void PlayOneShot(string name)
		{
			var sound = instance.sounds.FirstOrDefault(s => s.name == name);
			if(sound != null)
				instance.source.PlayOneShot(sound.clip);
		}
	}
}