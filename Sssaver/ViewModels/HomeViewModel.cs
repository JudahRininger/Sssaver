using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Sssaver.Models;
using Xamarin.Forms;

namespace Sssaver.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region fields
        string goalsText;
        bool showChallenge;
        bool showConfetti;
        //string password;
        #endregion
        public SavingsPlan SavingsPlan { get; set; }

        public decimal TodaysSavingsAmount { get; set; }

        public int RandomInt { get; set; }

        private int confettiCounter = 0;

        private Random rnd = new Random();

        public ICommand ChangePlan {get; set;}

        public ICommand DismissPlan { get; set; }

        public bool ShowConfetti
        {
            get { return showConfetti; }
            set
            {
                showConfetti = value;
                OnPropertyChanged("ShowConfetti");
            }
        }

        public bool ShowChallenge
        {
            get { return showChallenge; }
            set
            {
                showChallenge = value;
                OnPropertyChanged("ShowChallenge");
            }
        }

        public string GoalsText
        {
            get { return goalsText; }
            set
            {
                goalsText = value;
                OnPropertyChanged("GoalsText");
            }
        }



        public ObservableCollection<SavingsChallenge> SavingsHistory { get; set; }

        public HomeViewModel(SavingsPlan savingsPlan = null)    
        {
            if (savingsPlan == null)
            {
                // if no savingsPlan is passed into the constructor,
                // then create one. This is for demo purposes.
                SavingsPlan = new SavingsPlan()
                {
                    Days = 30,
                    Name = "Viper",
                    CurrentSavingsAmount = 30,
                    TotalSavingsAmount = 100
                };

                RandomInt = rnd.Next(1, 31);

                GoalsText = "Today's Saving Challenge";

                ShowChallenge = false;

                ShowConfetti = false;

                ChangePlan = new Command(ChangeSavingsPlan);

                DismissPlan = new Command(DismissPlanClicked);

                // Today's Savings Amount should be extracted from
                // the SavingsChallenges list in the SavingsPlan.


                // The SavingsHistory should be loaded from the
                // SavingsChallenges list in the SavingsPlan.
            }

            void ChangeSavingsPlan()
            {
                if(!ShowChallenge && RandomInt != 0)
                {
                    GoalsText = "Save!";
                    ShowChallenge = true;
                }
                else
                {
                    SavingsPlan.CurrentSavingsAmount += RandomInt;
                    RandomInt = 0;
                    ShowChallenge = false;
                    GoalsText = "Awesome!";
                    //ShowConfetti = true;
                    Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                    {
                        // called every 1 second
                        // do stuff here
                        ShowConfetti = true;
                        confettiCounter++;
                        if (confettiCounter >= 4)
                        {
                            ShowConfetti = false;
                            return false;
                        }
                        else
                        {
                            return true; // return true to repeat counting, false to stop timer
                        }

                    });

                }
            }
            void DismissPlanClicked()
            {
                GoalsText = "Today's Saving Challenge";
                ShowChallenge = false;
            }
        }
    }
}
