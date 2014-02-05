using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.SportsNow.Model
{
    public partial class Recommendation
    {
        protected bool Equals(Recommendation other)
        {
            return UserProfile.Equals(other.UserProfile) && _why.Equals(other._why) && SportEvent.Equals(other.SportEvent);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Recommendation) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_dateRecommendation.GetHashCode()*397) ^ _id.GetHashCode();
            }
        }

        public static bool operator ==(Recommendation left, Recommendation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Recommendation left, Recommendation right)
        {
            return !Equals(left, right);
        }
    }
}
