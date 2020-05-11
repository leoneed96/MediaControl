using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MediaControl
{
    public class AudioWorker: IObserver<DeviceChangedArgs>
    {
        public const int KEYEVENTF_EXTENTEDKEY = 1;
        public const int KEYEVENTF_KEYUP = 0;
        public const int VK_MEDIA_NEXT_TRACK = 0xB0;
        public const int VK_MEDIA_PLAY_PAUSE = 0xB3;
        public const int VK_MEDIA_PREV_TRACK = 0xB1;

        private CoreAudioDevice AudioDevice;
        private readonly CoreAudioController _audioController;
        public string AudioDeviceName => AudioDevice.FullName;
        public double Volume => AudioDevice.Volume;
        public void SetVolume(int volume) => AudioDevice.Volume = volume;
        public void IncreaseVolume(int step = 5) => AudioDevice.Volume += 5;
        public void DecreaseVolume(int step = 5) => AudioDevice.Volume -= 5;
        public void PlayPause() => keybd_event(VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
        public void Next() => keybd_event(VK_MEDIA_NEXT_TRACK, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
        public void Prev() => keybd_event(VK_MEDIA_PREV_TRACK, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte virtualKey, byte scanCode, uint flags, IntPtr extraInfo);
        public AudioWorker()
        {
            _audioController = new CoreAudioController();
            AudioDevice = _audioController.DefaultPlaybackDevice;

            _audioController.AudioDeviceChanged.Subscribe(this);
            
        }

        public void OnCompleted()
        {
            return;
        }

        public void OnError(Exception error)
        {
            return;
        }

        public void OnNext(DeviceChangedArgs value)
        {
            AudioDevice = (CoreAudioDevice)value.Device;
        }
    }

}
