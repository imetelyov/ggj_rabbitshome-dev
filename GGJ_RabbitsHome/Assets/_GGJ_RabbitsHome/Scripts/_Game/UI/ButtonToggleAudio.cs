using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace QuasarGames
{

    public enum AudioParameter
    {
        MuteMusic,
        MuteSFX
    }

    public class ButtonToggleAudio : MonoBehaviour
    {

        public Image targetButtonImage;

        public AudioParameter audioParam;

        public Sprite activeSprite;
        public Color activeColor;

        [Space(20)]
        public Sprite mutedSprite;
        public Color mutedColor;

        private bool currentIsMuted;

        public void Refresh()
        {
            RefreshState();

            ApplyImage();
        }

        void RefreshState()
        {
            if (audioParam == AudioParameter.MuteMusic)
                currentIsMuted = AudioManager.Instance.muteMusic;
            else if (audioParam == AudioParameter.MuteSFX)
                currentIsMuted = AudioManager.Instance.muteSFX;
        }

        void ApplyImage()
        {
            if (currentIsMuted)
            {
                targetButtonImage.sprite = mutedSprite;
                targetButtonImage.color = mutedColor;
            }
            else
            {
                targetButtonImage.sprite = activeSprite;
                targetButtonImage.color = activeColor;
            }

        }

        public void Toggle()
        {
            if (audioParam == AudioParameter.MuteMusic)
                AudioManager.Instance.ToggleMusic();
            else if (audioParam == AudioParameter.MuteSFX)
                AudioManager.Instance.ToggleSFX();

            RefreshState();
            ApplyImage();
        }


    }
}
