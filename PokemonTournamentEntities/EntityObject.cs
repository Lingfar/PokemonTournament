using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentEntities
{
    public abstract class EntityObject
    {
        public int ID { get; set; }
        public EntityObject(int id)
        {
            ID = id;
        }

        public override bool Equals(object obj)
        {
            return ID == ((EntityObject)obj).ID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return ID.ToString();
        }
    }
}
