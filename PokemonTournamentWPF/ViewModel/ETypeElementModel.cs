using PokemonTournamentEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentWPF.ViewModel
{
    public class ETypeElementModel
    {
        public string TypeString { get; set; }
        public ETypeElement Type { get; set; }

        public ETypeElementModel(ETypeElement type)
        {
            Type = type;
            TypeString = type.ToString();
        }
    }
}
