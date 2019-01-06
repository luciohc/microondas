using Prism.Mvvm;
using Prism.Navigation;
using microondas.fontes;
using System.Collections.ObjectModel;
using Prism.Commands;


namespace microondas.fontesview
{
	public class MasterViewModel : BindableBase
	{        
        private readonly INavigationService _navigationService;
        public DelegateCommand TestCommand { get; set; }
        public ObservableCollection<Principal> ProgramsList { get; set; }
        

        public MasterViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
           

            TestCommand = new DelegateCommand(ExTest);
        }           


        private void ExTest()
        {
            var queryString = "ligar";
            var navigationParams = new NavigationParameters(queryString);
            _navigationService.NavigateAsync("Master/Navigation/MainPage", navigationParams);
            
        }



        private void Inicial()
        {
            
            _navigationService.NavigateAsync("Inicial/Navegacao/MainPage");
        }
    }
}