namespace CCmall.Model.Entities
{
    ///<summary>
    ///
    ///</summary>
    public class Router
    {
        public Router()
        {
        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>

        public int id { get; set; }


        /// <summary>
        /// Desc:
        /// Default:1
        /// Nullable:True
        /// </summary>

        public int? status { get; set; }


        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:True
        /// </summary>

        public long? create_time { get; set; }


        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:True
        /// </summary>

        public long? edit_time { get; set; }


        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:True
        /// </summary>

        public int? creator { get; set; }


        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:True
        /// </summary>

        public int? editor { get; set; }


        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>

        public string name { get; set; }


        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>

        public string component { get; set; }


        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>

        public string icon { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>

        public int parent { get; set; }


    }
}
