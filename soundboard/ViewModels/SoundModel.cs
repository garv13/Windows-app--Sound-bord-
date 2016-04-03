using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoard.ViewModels
{
  public class SoundModel
    {
        public SoundGroup CustomSounds { get; set; }
        public SoundGroup Animals { get; set; }
        public SoundGroup Taunts { get; set; }
        public SoundGroup Warnings { get; set; }

        public bool IsDataLoaded { get; set; }

        public const string CustomSoundsKey = "CustomSound";


      public void LoadData()
        {
          //Load Data Into the model    

            Animals = CreateAnimalsGroup();
            Taunts = CreateTauntsGroup();
            Warnings = CreateWarningsGroup();

            CustomSounds = LoadCustomSounds();

          IsDataLoaded = true;
        }

      private SoundGroup LoadCustomSounds()
      {

          SoundGroup data;
          string dataFromAppSettings;
          if (IsolatedStorageSettings.ApplicationSettings.TryGetValue(CustomSoundsKey, out dataFromAppSettings))
          {
              data = JsonConvert.DeserializeObject<SoundGroup>(dataFromAppSettings);
          }
          else
          {
              data = new SoundGroup();
              data.Title = "mine";
          }

          return data;
      }

      private SoundGroup CreateAnimalsGroup()
      {
          SoundGroup data = new SoundGroup();

          data.Title = "Warfields";
            string basePath = "assets/audio/warfield/";

            data.Items.Add(new SoundData
            { 
                Title = "All Right Let's Do This", 
                FilePath = basePath + "alright_lets_do_this.mp3" 
            });

            data.Items.Add(new SoundData 
            {
                Title = "Bomb Has Been Defused", 
                FilePath = basePath + "bombdef.mp3" 
            });

            data.Items.Add(new SoundData 
            {
                Title = "bomb Has Been Planted", 
                FilePath = basePath + "bombpl.mp3" 
            });

            data.Items.Add(new SoundData
            {
                Title = "Bomb Explode", 
                FilePath = basePath + "c4_explode1.mp3" 
            });

            data.Items.Add(new SoundData 
            {
                Title = "Affirmative", 
                FilePath = basePath + "ct_affirm.mp3" 
            });

            data.Items.Add(new SoundData
            {
                Title = "Fire In The Hole", 
                FilePath = basePath + "ct_fireinhole.mp3" 
            });

            data.Items.Add(new SoundData 
            { 
                Title = "Headshot", 
                FilePath = basePath + "headshot1.mp3" 
            });

            data.Items.Add(new SoundData 
            { 
                Title = "Gun Fire", 
                FilePath = basePath + "m3-1.mp3" 
            });

            data.Items.Add(new SoundData
            { 
                Title = "Man Down", 
                FilePath = basePath + "pl_die1.mp3" 
            });

            data.Items.Add(new SoundData 
            { 
                Title = "Roger That", 
                FilePath = basePath + "roger_that.mp3"
            });

            data.Items.Add(new SoundData 
            { 
                Title = "Sound Like A Plan", 
                FilePath = basePath + "soundslikeaplan-2.mp3"
            });
                    
            return data;
        }

      private SoundGroup CreateTauntsGroup()
        {
            SoundGroup data = new SoundGroup();
            data.Title = "taunts";
            string basePath = "assets/audio/taunts/";

            data.Items.Add(new SoundData { Title = "Come to Papa", 
                FilePath = basePath + "come_to_papa.mp3" });

            data.Items.Add(new SoundData { Title = "Game Begins", 
                FilePath = basePath + "game_begins-3.mp3" });

            data.Items.Add(new SoundData { Title = "MayBe..", 
                FilePath = basePath + "maybe-2.mp3" });

            data.Items.Add(new SoundData { Title = "Negative -", 
                FilePath = basePath + "negative.mp3" });

            data.Items.Add(new SoundData { Title = "OH. Man", 
                FilePath = basePath + "oh_man.mp3" });

            data.Items.Add(new SoundData { Title = "I Am Ahead of You", 
                FilePath = basePath + "slow_ahead-3.mp3" });

            data.Items.Add(new SoundData { Title = "Stayout", 
                FilePath = basePath + "stayout-1.mp3" });

            data.Items.Add(new SoundData { Title = "Is it Supposed To Be Hurt", 
                FilePath = basePath + "supposed_hurt-1.mp3" });

            data.Items.Add(new SoundData { Title = "That's the Way It Is Done", 
                FilePath = basePath + "thats_the_way_this_is_done.mp3" });

            data.Items.Add(new SoundData { Title = "They Never Knew What Hit Them", 
                FilePath = basePath + "they_never_knew_what_hit_them.mp3" });

            data.Items.Add(new SoundData { Title = "Uhhh", 
                FilePath = basePath + "uhhh-2.mp3" });

            data.Items.Add(new SoundData { Title = "Yesss", 
                FilePath = basePath + "yes-3.mp3" });

            return data;
        }

      private SoundGroup CreateWarningsGroup()
        {
            SoundGroup data = new SoundGroup();
            data.Title = "Warnings";
            string basePath = "assets/audio/warnings/";

            data.Items.Add(new SoundData { Title = "Attack", 
                FilePath = basePath + "attack-3.mp3" });

            data.Items.Add(new SoundData { Title = "Bwahahaha", 
                FilePath = basePath + "bwahaha-1.mp3" });

            data.Items.Add(new SoundData { Title = "City Is Going Down", 
                FilePath = basePath + "city_down-1.mp3" });

            data.Items.Add(new SoundData { Title = "Have You Fixed Your Problem",
                FilePath = basePath + "fixed_problem-1.mp3" });

            data.Items.Add(new SoundData { Title = "Get Em", 
                FilePath = basePath + "get_em-2.mp3" });

            data.Items.Add(new SoundData { Title = "Gonna Boooom", 
                FilePath = basePath + "gonna_boom-2.mp3" });

            data.Items.Add(new SoundData { Title = "It Means War", 
                FilePath = basePath + "means_war-3.mp3" });

            data.Items.Add(new SoundData { Title = "You Need That", 
                FilePath = basePath + "need_that-3.mp3" });

            data.Items.Add(new SoundData { Title = "Is It Over??", 
                FilePath = basePath + "over_yet-4.mp3" });

            data.Items.Add(new SoundData { Title = "Prepared To Be Crushed", 
                FilePath = basePath + "prepare_crushed-1.mp3" });

            data.Items.Add(new SoundData { Title = "Stayout", 
                FilePath = basePath + "stayout-1.mp3" });

            data.Items.Add(new SoundData { Title = "This Is My House", 
                FilePath = basePath + "this_is_my_house.mp3" });

            data.Items.Add(new SoundData { Title = "Victory Is Mine",
                FilePath = basePath + "victoryismine-3.mp3" });

            data.Items.Add(new SoundData { Title = "Wanna Bet??",
                FilePath = basePath + "wanna_bet-2.mp3" });
            data.Items.Add(new SoundData { Title = "Wanna Give Up??", 
                FilePath = basePath + "wanngiveup-2.mp3" });
            return data;
        }
 
    }
}
