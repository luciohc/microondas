/* Classe com Funções e Procedimentos para Adicionar Tempo, Subtrair Tempo, Retornar Potencia,
Retornar Programa conforme string*/

using System;

namespace microondas.fontes
{
    public class Settings : BasePropertyChanged
    {
       private int potencia;        
       private string programacao; 
       private DateTime tempo;
       private char aquece;

        public Settings()
        {
            aquece = '.';
        }

        public DateTime Tempo
        {
            get
            {
                return tempo;
            }           
        }

        public void AdicionaTempo(int numero)
        {

            var tempopassado = tempo;
            var minutos = tempo.Minute;
            var segundos = tempo.Second * 10 + number;

            if (segundos > 60)
            {
                minutos = segundos / 60;
                segundos = segundos % 60;
            }

            tempo = new DateTime(0001, 1, 1, 0, minutos, segundos);
            SetProperty(ref tempopassado, tempo);
        }

        public void SubtraiTempo()
        {

            var tempopassado = tempo;
            var minutos = tempo.Minute;
            var segundos = tempo.Second;

            if (minutos > 0 && segundos == 0)
            {
                minutos--;
                segundos = 59;
            }
            else
            {
                segundos--;
            }

            tempo = new DateTime(0001, 1, 1, 0, minutos, segundos);
            SetProperty(ref tempopassado, tempo);
        }

        public void ZerarTempo()
        {
            var tempopassado = tempo;
            tempo = new DateTime();
            SetProperty(ref tempo, tempo);
        }

        public int Potencia
        {
            get
            {
                return potencia;
            }
            set
            {
                SetProperty(ref potencia, value);
            }
        }

        public string Programa
        {
            get
            {
                return programacao;
            }
            set
            {
                SetProperty(ref programacao, value);
            }
        }

        public char Aquece
        {
            get { return aquece; }
            set
            {
                SetProperty(ref aquece, value);
            }
        }
    }
}