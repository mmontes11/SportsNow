using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.SportsNow.Model
{
    public partial class UserGroup
    {
        protected bool Equals(UserGroup other)
        {
            return _id == other._id && string.Equals(_name, other._name) && string.Equals(_descrip, other._descrip);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UserGroup) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = _id.GetHashCode();
                hashCode = (hashCode*397) ^ (_name != null ? _name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_descrip != null ? _descrip.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(UserGroup left, UserGroup right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UserGroup left, UserGroup right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            string result;

            result =
                "id = " + this.id + " | " +
                "name = " + this.name + " | " +
                "description = " + this.descrip;
            return result;
        }
    }
}