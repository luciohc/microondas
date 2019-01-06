/*Classe com Funções e Procedimentos parar exibir alertas e configurar parâmetros*/

using microondas.fontes;
using microondas.Utils;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace microondas.fontesview

	public class CriacaoPrograma: BindableBase
	{
        
        private string _nomePrograma;
        private string _descricaoPrograma;
        private int _tempoPrograma;
        private int _potenciaPrograma;
        private char _ProgramaEspecial;
        private INavigationService _navigationService;
        Principal _programa = new Principal();
        public DelegateCommand SaveCommand { get; private set; }


        public string NomePrograma
        {
            get { return _nomePrograma; }
            set { SetProperty(ref _nomePrograma, value); }
        }

        public string DescricaoPrograma
        {
            get {return _descricaoPrograma;}
            set { if (_descricaoPrograma != value) {
                    _descricaoPrograma = value;
                }
            }
        }

        public int TempoPrograma
        {
            get { return _tempoPrograma; }
            set { if (_tempoPrograma != value) {
                    _tempoPrograma = value;
                }
            }
        }

        public int PotenciaPrograma
        {
            get { return _potenciaPrograma; }
            set { if (_potenciaPrograma != value)  {
                  _potenciaPrograma = value;  }
            }
        }

        public char CharEspecial
        {
            get { return _ProgramaEspecial;}
            set { if (_ProgramaEspecial != value) {
                    _ProgramaEspecial = value;
                }
            }
        }


        public CriacaoPrograma: (INavigationService navigationService)
        {
            _navigationService = navigationService;
            SaveCommand = new DelegateCommand(ExecuteSave);
        }

        private void ExecuteSave()
        {
            try
            {
                ValidaTempo();
            }
            catch
            {
                App.Current.MainPage.DisplayAlert("ALERTA", "Parametro do Tempo fora do limite", "OK");
                return;
            }
            try
            {
                ValidaPotencia();
            }
            catch
            {
                App.Current.MainPage.DisplayAlert("ALERTA", "Parametro da Potência fora do limite", "OK");
                return;
            }
            try
            {
                ValidaPrograma();
            }
            catch
            {
                App.Current.MainPage.DisplayAlert("ALERTA", "Programa com nome já existente", "OK");
                return;
            }
          

            DataBase.getInstance().Programas.Add(new Principal(NomePrograma, DescricaoPrograma, TempoPrograma, PotenciaPrograma, CharEspecial));
            _navigationService.GoBackToRootAsync();
        }

        private void ValidaTempo()
        {          
            if TempoPrograma, > 120)
                    throw new Exception();
            if TempoPrograma, < 1)
                throw new Exception();
        }

        private void ValidaPotencia()
        {
            if (PotenciaPrograma > 10)
                throw new Exception();
            if (PotenciaPrograma < 1)
                throw new Exception();
        }

        private void ValidaPrograma()
        {
            var searchResults = (from a in DataBase.getInstance().Programas
                                 where a.Name == NameProgram
                                 select a).FirstOrDefault();
            if (searchResults != null)
                throw new Exception();
        }
    }
}