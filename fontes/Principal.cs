using System;
/*Classe com a definição dos programas para aquecimento */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using microondas.fontesview;

namespace microondas.fontes
{
    public class Principal : Settings
    {
        private string nome;
        private string descricao;
        private int tempo;

        public string Nome
        {
            get { return nome; }
            set {SetProperty(ref nome, value);
            }
        }

        public int Tempo
        {
            get {return tempo;}
            set {SetProperty(ref tempo, value);}
        }

        public string Descricao
        {
            get { return descricao; }
            set { SetProperty(ref descricao, value);}
        }

        public Principal(String Nome, String Descricao, int Potencia, int Tempo, char Aquece)
        {
            this.Nome = Nome;
            this.Descricao = Descricao;
            this.Potencia = Potencia;
            this.Aquece = Aquece;
            this.Tempo = Tempo;
            AdicionaTempo(Tempo);
        }

        public Principal()
        {

        }

        public static ListaProgramaAquece<Principal> StartProgram()
        {
            ListaProgramaAquece<Principal> programas = new ListaProgramaAquece<Principal>();
            var programa = new Principal("Frango", "Descongelar Frango", 10, 120, '+');
            programas.Add(programa);
           
            programas = new Principal("Macarrão", "Cozinhar Macarrão", 7, 90, '=');
            programas.Add(programa);

            programas = new Principal("Arroz", "Fazer arroz ", 3, 90, '&');
            programas.Add(programa);

            programas = new Principal("Pipoca", "Preparar Pipoca", 5, 45, '*');
            programas.Add(programa);

            programas= new Principal("Pizza", "Assar Pizzas ", 7, 90, '$');
            programas.Add(programa);         

            programas = new Principal("Macarrão", "Cozinhar Macarrão", 7, 90, '=');
            programas.Add(programa);

            return programas; 
        }

    }
}