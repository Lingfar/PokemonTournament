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
            EntityObject entity = obj as EntityObject;
            if (entity != null)
                return ID == entity.ID;
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
