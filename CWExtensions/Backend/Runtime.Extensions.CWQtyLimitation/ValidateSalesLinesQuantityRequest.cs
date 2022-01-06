namespace Commerce.Runtime.CWQtyLimitation
{
    using System.Runtime.Serialization;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using Microsoft.Dynamics.Commerce.Runtime.DataServices.Messages;

    /// <summary>
    /// The data service request to get the downloading data set.
    /// </summary>
    [DataContract]
    public class ValidateSalesLinesQuantityRequest : DataRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDownloadingDataSetDataRequest"/> class.
        /// </summary>
        /// <param name="dataGroupName">The data group name.</param>
        /// <param name="settings">The query result settings.</param>
        public ValidateSalesLinesQuantityRequest(string cartId, long cartVersion, QueryResultSettings settings)
        {
            this.CartId = cartId;
            this.CartVersion = cartVersion;
            this.QueryResultSettings = settings;
        }

        [DataMember]
        public string CartId { get; private set; }

        [DataMember]
        public long CartVersion { get; private set; }

    }
}

