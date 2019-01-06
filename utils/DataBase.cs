using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using microondas.fontes;

namespace microondas.utils
{
    public class DataBase
    {
        private static DataBase _dataBase;

        public ObservableCollection<Principal> Programs { get; set; }

        public DataBase()
        {
            Programs = Principal.StartProgram();
        } 

        public static DataBase getInstance()
        {
            if (_dataBase == null)
                _dataBase = new DataBase();
            return _dataBase;
        }
    }
}