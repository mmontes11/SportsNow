using System;

namespace Es.Udc.DotNet.SportsNow.Model.UserService
{
    /// <summary>
    /// VO Class which contains the user details
    /// </summary>
    [Serializable]
    public class UserProfileDetails
    {
        #region Properties Region

        public String LoginName { get; private set; }

        public String FirstName { get; private set; }

        public String Lastname { get; private set; }

        public String Email { get; private set; }

        public string Language { get; private set; }

        public string Country { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileDetails"/>
        /// class.
        /// </summary>
        /// <param name="firstName">The user's first name.</param>
        /// <param name="lastName">The user's last name.</param>
        /// <param name="email">The user's email.</param>
        /// <param name="language">The language.</param>
        /// <param name="country">The country.</param>
        public UserProfileDetails(String firstName, String lastName,
            String email, String language, String country)
        {
            FirstName = firstName;
            Lastname = lastName;
            Email = email;
            Language = language;
            Country = country;
        }

        public override bool Equals(object obj)
        {

            UserProfileDetails target = (UserProfileDetails)obj;

            return (FirstName == target.FirstName)
                  && (Lastname == target.Lastname)
                  && (Email == target.Email)
                  && (Language == target.Language)
                  && (Country == target.Country);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the FirstName does not change.        
        public override int GetHashCode()
        {
            return FirstName.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the 
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </returns>
        public override String ToString()
        {
            String strUserProfileDetails;

            strUserProfileDetails =
                "[ firstName = " + FirstName + " | " +
                "lastName = " + Lastname + " | " +
                "email = " + Email + " | " +
                "language = " + Language + " | " +
                "country = " + Country + " ]";


            return strUserProfileDetails;
        }
    }
}
