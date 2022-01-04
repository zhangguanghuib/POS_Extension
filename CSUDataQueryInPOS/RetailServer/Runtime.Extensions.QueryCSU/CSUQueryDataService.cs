using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Dynamics.Commerce.Runtime;
using Microsoft.Dynamics.Commerce.Runtime.Data.Types;
using Microsoft.Dynamics.Commerce.Runtime.Messages;
using Microsoft.Dynamics.Commerce.Runtime.DataServices.Messages;
using System.Diagnostics;
using Microsoft.Dynamics.Commerce.Runtime.Data;

namespace Commerce.Runtime.QueryCSU
{
    public class CSUQueryDataService : IRequestHandlerAsync
    {
        public IEnumerable<Type> SupportedRequestTypes
        {
            get
            {
                return new[]
                {
                    typeof(CSUQueryDataSetDataRequest)
                };
            }
        }


        public async Task<Response> Execute(Request request)
        {
            ThrowIf.Null(request, nameof(request));

            switch (request)
            {
                case CSUQueryDataSetDataRequest csuQueryDataSetDataRequest:
                    return await this.GetCSUQueryDataSet(csuQueryDataSetDataRequest);
                default:
                    throw new NotSupportedException(string.Format("Request '{0}' is not supported.", request.GetType()));
            }
        }


        public async Task<Response> GetCSUQueryDataSet(CSUQueryDataSetDataRequest request)
        {
            ThrowIf.Null(request, nameof(request));
            ThrowIf.NullOrWhiteSpace(request.SqlText, "request.SqlText");

            Stopwatch processTimer = Stopwatch.StartNew();

            ParameterSet parameters = new ParameterSet();

            DataSet result = null;

            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                result = await databaseContext.ExecuteQueryDataSetAsync(request.SqlText, parameters).ConfigureAwait(false); ;
            }

            processTimer.Stop();

            return new SingleEntityDataServiceResponse<DataSet>(result);
        }
    }
}
