namespace Commerce.Runtime.QueryCSU
{
    using System.Runtime.Serialization;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using Microsoft.Dynamics.Commerce.Runtime.DataServices.Messages;

    /// <summary>
    /// The data service request to get the downloading data set.
    /// </summary>
    [DataContract]
    public class CSUQueryDataSetDataRequest : DataRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDownloadingDataSetDataRequest"/> class.
        /// </summary>
        /// <param name="dataGroupName">The data group name.</param>
        /// <param name="settings">The query result settings.</param>
        public CSUQueryDataSetDataRequest(string sqlText, QueryResultSettings settings)
        {
            this.SqlText = sqlText;
            this.QueryResultSettings = settings;
        }

        /// <summary>
        /// Gets the data group name.
        /// </summary>
        [DataMember]
        public string SqlText { get; private set; }
    }
}
