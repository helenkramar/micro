using System;
using System.Collections.Generic;

namespace integration.Models
{
    public class ItemsSame : EqualityComparer<Item>
    {
        public override bool Equals(Item x, Item y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;

            return (x.Id == y.Id &&
                    x.Name == y.Name);
        }

        public override int GetHashCode(Item obj)
        {
            throw new NotImplementedException();
        }
    }
}
