namespace CCmall.Model.Entities
{
    ///<summary>
    ///BaseUser
    ///</summary>
    public class BaseUser
    {
        public BaseUser()
        {
        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>

        public string nickname { get; set; }


        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>

        public string password { get; set; }


        /// <summary>
        /// Desc:
        /// Default:1
        /// Nullable:False
        /// </summary>

        public int status { get; set; }


        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>

        public string username { get; set; }


        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>

        public int id { get; set; }


        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>

        public string mobile { get; set; }


    }
}
