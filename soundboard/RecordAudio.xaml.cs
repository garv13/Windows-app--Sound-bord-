using Coding4Fun.Toolkit.Audio;
using Coding4Fun.Toolkit.Audio.Helpers;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using SoundBoard.Resources;
using SoundBoard.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SoundBoard
{

    public partial class RecordAudio : PhoneApplicationPage
    {
        private MicrophoneRecorder _recorder = new MicrophoneRecorder();
        private IsolatedStorageFileStream _audioStream;
        private string _tempFileName = "tempWav.wav";

        public RecordAudio()
        {
            InitializeComponent();

            BuildLocalizedApplicationBar();

        }

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBarIconButton recordAudioAppBar = new ApplicationBarIconButton();

            recordAudioAppBar.IconUri = new Uri("/Assets/AppBar/save.png", UriKind.Relative);
            recordAudioAppBar.Text = AppResources.AppBarSave;

            recordAudioAppBar.Click +=recordAudioAppBar_Click;
            ApplicationBar.Buttons.Add(recordAudioAppBar);
            ApplicationBar.IsVisible = false;
        }

        private void recordAudioAppBar_Click(object sender, EventArgs e)
        {

            InputPrompt filename = new InputPrompt();

            filename.Title = "Sound Name";
            filename.Message = "What should We call the sound?";

            filename.Completed += FileNameCompleted;

            filename.Show();
        }

        private void FileNameCompleted(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            
            if(e.PopUpResult == PopUpResult.Ok)
            {
                //Create a sound data object

                SoundData soundData = new SoundData();
                soundData.FilePath = string.Format("/customAudio/{0}.wav",DateTime.Now.ToFileTime());
                soundData.Title = e.Result;
                    
                // save the wav file in directory/customaudio
                using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication()){

                    if(!isoStore.DirectoryExists("/customAudio/"))
                        isoStore.CreateDirectory("/customAudio/");

                    isoStore.MoveFile(_tempFileName, soundData.FilePath);

                }

                // add sound data object to app.viewmodel.customsounds

                App.ViewModel.CustomSounds.Items.Add(soundData);

                // save the list of custom sounds to IsolatedStorage.ApplicationSettings

                var data = JsonConvert.SerializeObject(App.ViewModel.CustomSounds);

                IsolatedStorageSettings.ApplicationSettings[SoundModel.CustomSoundsKey] = data;
                IsolatedStorageSettings.ApplicationSettings.Save();

                // We need to modify our sound model to retrive custom sounds from IsolatedStorage.ApplicationSettings

                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));

            }
        }

        private void RecordAudioChecked(object sender, RoutedEventArgs e)
        {
            PlayAudio.IsEnabled = false;
            ApplicationBar.IsVisible = false;
            RotateCircle.Begin();
            _recorder.Start();
        }

        private void RecordAudioUnchecked(object sender, RoutedEventArgs e)
        {
            _recorder.Stop();
            SaveTempAudio(_recorder.Buffer);
            PlayAudio.IsEnabled = true;
            ApplicationBar.IsVisible = true;
            RotateCircle.Stop();
        }
        private void SaveTempAudio(MemoryStream buffer)
        {
            //be Defensive ...trust no one
            if (buffer == null)
                throw new ArgumentException("Atttempting to save an empty sound buffer");

            //Clean up the audio player and hold on our audio stream
            if(_audioStream!=null)
            {
                AudioPlayer.Stop();
                AudioPlayer.Source = null;

                _audioStream.Dispose();
            }

            var bytes = buffer.GetWavAsByteArray(_recorder.SampleRate);
            
            using(IsolatedStorageFile isostore = IsolatedStorageFile.GetUserStoreForApplication())
            {

                if (isostore.FileExists(_tempFileName))
                    isostore.DeleteFile(_tempFileName);

                _tempFileName = string.Format("{0}.wav", DateTime.Now.ToFileTime());
                
                _audioStream = isostore.CreateFile(_tempFileName);
                _audioStream.Write(bytes, 0, bytes.Length);
                //Play ... Set Source of Media Element
                AudioPlayer.SetSource(_audioStream);
            }
        }

        private void PlayAudioClick(object sender, RoutedEventArgs e)
        {

            AudioPlayer.Play();
        }
    }
}