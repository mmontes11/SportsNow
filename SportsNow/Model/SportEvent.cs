using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.SportsNow.Model
{
    public partial class SportEvent
    {
        protected bool Equals(SportEvent other)
        {
            return string.Equals(_review, other._review) && string.Equals(_place, other._place) && _dateEvent.Equals(other._dateEvent) && string.Equals(_name, other._name) && _id == other._id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SportEvent) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (_review != null ? _review.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_place != null ? _place.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ _dateEvent.GetHashCode();
                hashCode = (hashCode*397) ^ (_name != null ? _name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ _id.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(SportEvent left, SportEvent right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SportEvent left, SportEvent right)
        {
            return !Equals(left, right);
        }
    }
}
