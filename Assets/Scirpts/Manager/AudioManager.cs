using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scirpts.Manager
{
    public enum SoundEffectName {
        Player_Attack,
        Player_Dead,
        Player_Down,
        Player_Fall,
        Player_Hit,
        Player_Jump,
        Player_Walk,
        Player_Rush,
    }

    public enum BgmName
    {
        mainTheme,
        backGround,
        bossIntro,
        bossLoop,
        bossOutro,
        fightTheme
    }
    
    [DisallowMultipleComponent]
    public class AudioManager : SingletonBase<AudioManager>
    {
        #region 字段

        [Serializable]
        public struct SoundEffectItem
        {
            public SoundEffectName audioName;
            public AudioClip audioClip;
        }
        
        [Serializable]
        public struct BgmItem
        {
            public BgmName audioName;
            public AudioClip audioClip;
        }
        
        // 存储游戏中用到的所有音效
        public SoundEffectItem[] soundEffectItems;
        // 存储游戏中用到的所有背景音
        public BgmItem[] bgmItems;
        
        private Dictionary<SoundEffectName, AudioClip> soundEffectDict;
        private Dictionary<BgmName, AudioClip> bgmDict;
        
        // 整体背景BGM
        public AudioSource currentBGM;
        public AudioSource currentEffectMusic;

        #endregion

        void Start()
        {
            if (currentBGM == null || currentEffectMusic == null)
            {
                Debug.LogError("AudioSource lost");
            }
            soundEffectDict = new Dictionary<SoundEffectName, AudioClip>();
            bgmDict = new Dictionary<BgmName, AudioClip>();
            for (int i = 0; i < soundEffectItems.Length; i++)
            {
                soundEffectDict.Add(soundEffectItems[i].audioName, soundEffectItems[i].audioClip);
            }
            for (int i = 0; i < bgmItems.Length; i++)
            {
                bgmDict.Add(bgmItems[i].audioName, bgmItems[i].audioClip);
            }
            AudioClip newClip = bgmDict[BgmName.mainTheme];
            currentBGM.clip = newClip;
            currentBGM.loop = true;
            currentBGM.Play();
            //Debug.Log(soundEffectDict);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                SwitchBgm(BgmName.fightTheme);
            }
        }

        #region Interface
        
        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="audioName"></param>
        /// <param name="isLoop"></param>
        public void PlaySoundEffect(SoundEffectName audioName, bool isLoop = false)
        {
            // 获取要播放的音源
            AudioClip audioClip = soundEffectDict[audioName];
            // 切换音效
            currentEffectMusic.clip = audioClip;
            // 每次播放前，设置音调为随机值，让声音每次听起来都稍微有些不同
            currentEffectMusic.pitch = Random.Range(0.9f, 1.1f);
            currentEffectMusic.loop = isLoop;
            currentEffectMusic.Play();
        }
        
        /// <summary>
        /// 停止播放音效
        /// </summary>
        public void StopSoundEffect() => currentEffectMusic.Stop();

        public void SwitchBgm(BgmName bgmName)
        {
            AudioClip newClip = bgmDict[bgmName];
            if (currentBGM.clip == null || currentBGM.clip != newClip)
            {
                //currentBGM.clip = newClip;
                StartCoroutine(SwitchBgmWithFade(newClip, 1.0f));
            }
        }

        IEnumerator SwitchBgmWithFade(AudioClip newClip, float fadeDuration)
        {
            float timer = 0.0f;
            float startVolume = currentBGM.volume;

            while (timer < fadeDuration)
            {
                currentBGM.volume = Mathf.Lerp(startVolume, 0, timer / fadeDuration); // 渐变音量从当前音量到0
                timer += Time.deltaTime;
                yield return null;
            }

            currentBGM.Stop();
            currentBGM.clip = newClip;
            currentBGM.Play();

            timer = 0.0f;
            while (timer < fadeDuration)
            {
                currentBGM.volume = Mathf.Lerp(0, startVolume, timer / fadeDuration); // 渐变音量从0到目标音量
                timer += Time.deltaTime;
                yield return null;
            }

            currentBGM.volume = startVolume; // 确保音量最终恢复到目标音量
        }
        
        public void PauseBGM()
        {
            currentBGM.Pause();
        }

        public void ResumeBGM()
        {
            currentBGM.Play();
        }
        
        #endregion
    }
}