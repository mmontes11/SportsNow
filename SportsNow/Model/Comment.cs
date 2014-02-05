using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.SportsNow.Model
{
    public partial class Comment
    {
        protected bool Equals(Comment other)
        {
            return _dateComment.Equals(other._dateComment) && string.Equals(_value, other._value) && _id == other._id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Comment) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = _dateComment.GetHashCode();
                hashCode = (hashCode*397) ^ (_value != null ? _value.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ _id.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Comment left, Comment right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Comment left, Comment right)
        {
            return !Equals(left, right);
        }


    }
}
