using System;
using System.Collections.Generic;
using System.Text;

namespace microondas.fontes
{
    interface IEMicroondas
    {
        void DefinirTempo(DateTime tempo);
        void DefinirPotencia(int potencia);

        
    }
}