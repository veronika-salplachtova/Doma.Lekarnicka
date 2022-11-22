using Doma.Lekarnicka.Logic;
using System.Drawing;

namespace Doma.Lekarnicka
{
    internal class DomaciLekarnickaProgram
    {
        static void Main(string[] args)
        {
            DomaciLekarnickaConsole domaciLekarnicka = new();

            domaciLekarnicka.Run();
           
        }
    }
}