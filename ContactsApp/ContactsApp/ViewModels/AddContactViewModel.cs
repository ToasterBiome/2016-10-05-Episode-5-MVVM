using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactsApp.ViewModels
{
    public class AddContactViewModel : INotifyPropertyChanged
    {

        public AddContactViewModel()
        {
            LaunchWebsiteCommand = new Command(LaunchWebsite,
                                                () => !IsBusy);
            SaveContactCommand = new Command(async ()=> await SaveContact(),
                                            () => !IsBusy);
        }

        string name = "James Montemagno";
        string website = "http://motz.codes";
        bool bestFriend;
        bool worstEnemy;
        bool isBusy = false;
        string companyName = "Companies R' Us";
        string companyRevenue;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public bool BestFriend
        {
            get { return bestFriend; }
            set
            {
                bestFriend = value;
                OnPropertyChanged();

                OnPropertyChanged(nameof(DisplayMessage));
            }
        }

        public bool WorstEnemy
        {
            get { return worstEnemy; }
            set
            {
                worstEnemy = value;
                OnPropertyChanged();

                OnPropertyChanged(nameof(DisplayMessage));
            }
        }

        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                companyName = value;
                OnPropertyChanged();

                OnPropertyChanged(nameof(DisplayCompanyMessage));
            }
        }

        public string CompanyRevenue
        {
            get
            {
                return companyRevenue;
            }
            set
            {
                companyRevenue = value;
                OnPropertyChanged();

                OnPropertyChanged(nameof(DisplayCompanyMessage));
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;

                if (name == "Miguel")
                    IsBusy = true;
                else
                    IsBusy = false;

                OnPropertyChanged();

                OnPropertyChanged(nameof(DisplayMessage));
            }
        }



        public string DisplayMessage
        {
            get { return $"Your new friend is named {Name} and " +
                         $"{(bestFriend ? "is" : "is not")} your best friend and "+
                         $"{(worstEnemy ? "is" : "is not")} your worst enemy."; }
        }

        public string DisplayCompanyMessage
        {
            get
            {
                return $"Your company is named {Name} and your revenue is " +
                       $"{CompanyRevenue}";
            }
        }

        public string Website
        {
            get
            {
                return website;
            }
            set
            {
                website = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;

                OnPropertyChanged();
                LaunchWebsiteCommand.ChangeCanExecute();
                SaveContactCommand.ChangeCanExecute();
            }
        }


        public Command LaunchWebsiteCommand { get; }
        public Command SaveContactCommand { get; }

        void LaunchWebsite()
        {
            try
            {
                Device.OpenUri(new Uri(website));
            }
            catch
            {

            }
        }

        async Task SaveContact()
        {
            IsBusy = true;
            await Task.Delay(4000);

            IsBusy = false;

            await Application.Current.MainPage.DisplayAlert("Save", "Contact has been saved", "OK");
        }

    }
}
