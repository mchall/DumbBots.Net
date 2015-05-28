using System;
using IrrKlang;

namespace Game
{
    /// <summary>
    /// Handles the playing of sounds
    /// </summary>
    public static class SoundManager
    {
        private static ISoundEngine _engine;

        private static bool _playSound;
        internal static bool PlaySound
        {
            get { return _playSound; }
            set
            {
                _playSound = value;
                if (_playSound == true)
                {
                    try
                    {
                        _engine = new ISoundEngine();
                        _engine.SoundVolume = 0.5f;
                    }
                    catch (Exception)
                    {
                        _playSound = false;
                    }
                }
            }
        }

        static SoundManager()
        {
            try
            {
                _engine = new ISoundEngine();
                _engine.SoundVolume = 0.5f;
                _playSound = true;
            }
            catch (Exception)
            {
                _playSound = false;
            }
        }

        internal static void PlayRocketFire()
        {
            if (_playSound)
            {
                _engine.Play2D("Sounds\\CANNON_F.WAV");
            }
        }

        internal static void PlayGunFire()
        {
            if (_playSound)
            {
                Random rand = new Random();
                int choice = rand.Next(2);
                if (choice == 1)
                {
                    _engine.Play2D("Sounds\\pistol-02.wav");
                }
                else
                {
                    _engine.Play2D("Sounds\\pistol-01.wav");
                }
            }
        }

        internal static void PlayDeath()
        {
            if (_playSound)
            {
                _engine.Play2D("Sounds\\death1.wav");
            }
        }

        internal static void PlayGetHealth()
        {
            if (_playSound)
            {
                _engine.Play2D("Sounds\\m_health.wav");
            }
        }

        internal static void PlayGetBazooka()
        {
            if (_playSound)
            {
                _engine.Play2D("Sounds\\w_pkup.wav");
            }
        }

        internal static void PlayUpdate()
        {
            if (_playSound)
            {
                _engine.Play2D("Sounds\\pc_up.wav");
            }
        }

        internal static void PlayPainSmall()
        {
            if (_playSound)
            {
                Random rand = new Random();
                int choice = rand.Next(5);
                if (choice == 0)
                {
                    _engine.Play2D("Sounds\\fall2.wav");
                }
            }
        }

        internal static void PlayPain()
        {
            if (_playSound)
            {
                Random rand = new Random();
                int choice = rand.Next(5);
                switch (choice)
                {
                    case (0):
                        _engine.Play2D("Sounds\\pain100_1.wav");
                        break;
                    case (1):
                        _engine.Play2D("Sounds\\pain100_2.wav");
                        break;
                    case (2):
                        _engine.Play2D("Sounds\\pain25_1.wav");
                        break;
                    case (3):
                        _engine.Play2D("Sounds\\pain25_2.wav");
                        break;
                    case (4):
                        _engine.Play2D("Sounds\\pain50_1.wav");
                        break;
                }
            }
        }

        internal static void PlayCustom(string filename)
        {
            if (_playSound)
            {
                try
                {
                    _engine.Play2D(filename);
                }
                catch (Exception)
                { 
                    //Do nothing
                }
            }
        }
    }
}