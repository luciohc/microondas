/* Classe com Funções e Procedimentos para inicializar os botões da interface com o usuário, 
verificação da string passada pelo usuário    

*/
using Prism.Commands;
using Prism.Navigation;
using microondas.fontes;
using System.Collections.ObjectModel;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using microondas.utils;

namespace microondas.fontesview
{
    public class PaginaPrincipal : ViewModelBase
    {
        private INavigationService _navigationService;
        public ObservableCollection<Principal> ProgramsList { get; set; } = new ObservableCollection<Program>();
        public ObservableCollection<Principal> SearchResults { get; }

        private string _foodString;
        private string _searchTerm;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                SetProperty(ref _searchTerm, value);
                SearchCommand.ChangeCanExecute();
                SearchResults.Clear();
            }
        }

        public Command SearchCommand { get; }

        private bool _startList = true;

        Settings _settings = new Settings();
        Microondas _microondas = new Microondas();


        /* Inicialização dos botões da interface*/

        public DelegateCommand BtnOneCommand { get; private set; }
        public DelegateCommand BtnTwoCommand { get; private set; }
        public DelegateCommand BtnThreeCommand { get; private set; }
        public DelegateCommand BtnFourCommand { get; private set; }
        public DelegateCommand BtnFiveCommand { get; private set; }
        public DelegateCommand BtnSixCommand { get; private set; }
        public DelegateCommand BtnSevenCommand { get; private set; }
        public DelegateCommand BtnEightCommand { get; private set; }
        public DelegateCommand BtnNineCommand { get; private set; }
        public DelegateCommand BtnZeroCommand { get; private set; }

        public DelegateCommand BtnClearTimeCommand { get; private set; }
        public DelegateCommand BtnClearPowerCommand { get; private set; }

        public DelegateCommand SubPowerCommand { get; private set; }
        public DelegateCommand AddPowerCommand { get; private set; }

        public DelegateCommand BtnStartCommand { get; private set; }

        public DelegateCommand NavigateToPageProgramsCommand { get; private set; }

        
        
        public string FoodString
        {
            get
            {
                return _foodString;
            }
            set
            {
                if (_foodString != value)
                {
                    SetProperty(ref _foodString, value);
                }
            }
        }

        private Program _selectedProgram { get; set; }
        public Program SelectedProgram
        {
            get
            {
                return _selectedProgram;
            }
            set
            {
                if (_selectedProgram != value)
                {
                    _selectedProgram = value;
                    try
                    {
                        HandleSelectedItem();
                    }
                    catch (Exception ex)
                    {
                        App.Current.MainPage.DisplayAlert("ALERTA", "Alimento incompatível com o programa selecionado", "OK");
                    }
                }
            }
        }
        private void HandleSelectedItem()
        {
            
            string name = _selectedProgram.Name;
            if (_foodString.Contains(name))
            {
                _settings = _selectedProgram;
                ExecuteBtnStartAsync();
            }
           
        }



    public PaginaPrincipal(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;

            SearchResults = new ObservableCollection<Program>();
            SearchCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);

            StartProgramList();
            BtnOneCommand = new DelegateCommand(ExecuteBtnOne);
            BtnTwoCommand = new DelegateCommand(ExecuteBtnTwo);
            BtnThreeCommand = new DelegateCommand(ExecuteBtnThree);
            BtnFourCommand = new DelegateCommand(ExecuteBtnFour);
            BtnFiveCommand = new DelegateCommand(ExecuteBtnFive);
            BtnSixCommand = new DelegateCommand(ExecuteBtnSix);
            BtnSevenCommand = new DelegateCommand(ExecuteBtnSeven);
            BtnEightCommand = new DelegateCommand(ExecuteBtnEight);
            BtnNineCommand = new DelegateCommand(ExecuteBtnNine);
            BtnZeroCommand = new DelegateCommand(ExecuteBtnZero);

            BtnClearTimeCommand = new DelegateCommand(ExecuteBtnClearTime);
            BtnClearPowerCommand = new DelegateCommand(ExecuteBtnClearPower);

            SubPowerCommand = new DelegateCommand(ExecuteSubPower);
            AddPowerCommand = new DelegateCommand(ExecuteAddPower);

            BtnStartCommand = new DelegateCommand(ExecuteBtnStartAsync);

            NavigateToPageProgramsCommand = new DelegateCommand(ExecuteNavigationToPageProgram);
        }

        private bool CanExecuteSearchCommand()
        {
            return string.IsNullOrWhiteSpace(SearchTerm) == false;
        }

        private async void ExecuteSearchCommand()
        {
            var searchResults = (from a in ProgramsList
                                 where a.Name.Contains(SearchTerm)
                                 select a).FirstOrDefault();

            if (searchResults == null)
            {
                await App.Current.MainPage.DisplayAlert("RESULTADO", "Nenhum resultado encontrado.", "OK");
                return;
            }

            SearchResults.Add(searchResults);

        }

        private void ExecuteBtnOne()
        {
            ExecuteSetTime(1);
        }

        private void ExecuteBtnTwo()
        {
            ExecuteSetTime(2);
        }

        private void ExecuteBtnThree()
        {
            ExecuteSetTime(3);
        }

        private void ExecuteBtnFour()
        {
            ExecuteSetTime(4);
        }

        private void ExecuteBtnFive()
        {
            ExecuteSetTime(5);
        }

        private void ExecuteBtnSix()
        {
            ExecuteSetTime(6);
        }

        private void ExecuteBtnSeven()
        {
            ExecuteSetTime(7);
        }

        private void ExecuteBtnEight()
        {
            ExecuteSetTime(8);
        }

        private void ExecuteBtnNine()
        {
            ExecuteSetTime(9);
        }

        private void ExecuteBtnZero()
        {
            ExecuteSetTime(0);
        }


        private void ExecuteBtnClearTime()
        {
            _settings.ZerarTime();
        }

        private void ExecuteBtnClearPower()
        {
            _settings.Power = 0;
        }


        private void ExecuteSetTime(int number)
        {
            if (!_microondas.Controller.Executando)
            {
                _settings.AddTime(number);
            }
        }

        private void ExecuteSubPower()
        {
            _settings.Power--;
        }

        private void ExecuteAddPower()
        {            
            _settings.Power++;
        }

        private void StartProgramList()
        {
            ProgramsList = DataBase.getInstance().Programs;
        }

        /* Procedimento para execução do tempo em modo ligado */
        private async void ExecuteBtnStartAsync()
        {
            try
            {
                _microondas.Execute(_settings);
                _microondas.Controller.Executando = true;
                string result = new String(_microondas.Settings.CharHeating, _settings.Power);
                var tempo = _settings.Time.Minute * 60 + _settings.Time.Second;
                while (tempo > 0)
                {
                    FoodString += result;
                    await Task.Delay(1000);
                    _settings.SubtraiTime();
                    tempo = _settings.Time.Minute * 60 + _settings.Time.Second;
                }
                FoodString += "Aquecida";                
                _microondas.Controller.Executando = false;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Parâmetros Incorretos", ex.Message, "OK");
            }
        }

        private void ExecuteNavigationToPageProgram()
        {
            _navigationService.NavigateAsync("CreateProgramPage");
        }
    }
}