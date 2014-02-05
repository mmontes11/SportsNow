using System;

namespace Es.Udc.DotNet.SportsNow.Model
{
    public partial class UserProfile
    {
        protected bool Equals(UserProfile other)
        {
            return _id == other._id && string.Equals(_enPassword, other._enPassword) && string.Equals(_loginName, other._loginName) && string.Equals(_firstName, other._firstName) && string.Equals(_lastName, other._lastName) && string.Equals(_email, other._email) && string.Equals(_language, other._language) && string.Equals(_country, other._country);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UserProfile) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = _id.GetHashCode();
                hashCode = (hashCode*397) ^ (_enPassword != null ? _enPassword.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_loginName != null ? _loginName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_firstName != null ? _firstName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_lastName != null ? _lastName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_email != null ? _email.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_language != null ? _language.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_country != null ? _country.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(UserProfile left, UserProfile right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UserProfile left, UserProfile right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            string strUserProfile;

            strUserProfile =
                "id = " + this.id + " | " +
                "loginName = " + this.loginName + " | " +
                "firstName = " + this.firstName + " | " +
                "lastName = " + this.lastName + " | " +
                "email = " + this.email + " | " +
                "language = " + this.language + " | " +
                "country = " + this.country;

            return strUserProfile;
        }
    }
}
