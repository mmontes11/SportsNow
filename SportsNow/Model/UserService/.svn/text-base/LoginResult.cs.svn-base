using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.SportsNow.Model.UserService
{
    /// <summary>
    /// A Custom VO which keeps the results for a login action.
    /// </summary>
    [Serializable()]
    public class LoginResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginResult"/> class.
        /// </summary>
        /// <param name="userProfileId">The user profile id.</param>
        /// <param name="firstName">Users's first name.</param>
        /// <param name="encryptedPassword">The encrypted password.</param>
        /// <param name="language">The language.</param>
        /// <param name="country">The country.</param>
        public LoginResult(long userProfileId, String firstName,
            String encryptedPassword, String language, String country)
        {
            this.UserProfileId = userProfileId;
            this.FirstName = firstName;
            this.EncryptedPassword = encryptedPassword;
            this.Language = language;
            this.Country = country;
        }

        #region Properties Region

        /// <summary>
        /// Gets the user profile id.
        /// </summary>
        /// <value>The user profile id.</value>
        public long UserProfileId { get; private set; }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <value>The <c>firstName</c></value>
        public string FirstName { get; private set; }


        /// <summary>
        /// Gets the encrypted password.
        /// </summary>
        /// <value>The <c>encryptedPassword.</c></value>
        public string EncryptedPassword { get; private set; }

        /// <summary>
        /// Gets the language code.
        /// </summary>
        /// <value>The language code.</value>
        public string Language { get; private set; }

        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public string Country { get; private set; }


        #endregion

        public override bool Equals(object obj)
        {

            LoginResult target = (LoginResult)obj;

            return (this.UserProfileId == target.UserProfileId)
                   && (this.FirstName == target.FirstName)
                   && (this.EncryptedPassword == target.EncryptedPassword)
                   && (this.Language == target.Language)
                   && (this.Country == target.Country);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, it is based on a field that does not change.
        public override int GetHashCode()
        {
            return this.UserProfileId.GetHashCode();
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
            String strLoginResult;

            strLoginResult =
                "[ userProfileId = " + UserProfileId + " | " +
                "firstName = " + FirstName + " | " +
                "encryptedPassword = " + EncryptedPassword + " | " +
                "language = " + Language + " | " +
                "country = " + Country + " ]";

            return strLoginResult;
        }

    }
}
