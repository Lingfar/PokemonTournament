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

        public EntityObject()
        {

        }

        public EntityObject(int id)
        {
            ID = id;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == typeof(EntityObject))
                return ID == ((EntityObject)obj).ID;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "Id = " + ID.ToString();
        }
    }
}
