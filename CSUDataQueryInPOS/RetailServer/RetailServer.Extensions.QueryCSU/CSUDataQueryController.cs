namespace RetailServer.Extensions.QueryCSU
{
    using System.Threading.Tasks;
    using Microsoft.Dynamics.Commerce.Runtime;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using Microsoft.Dynamics.Commerce.Runtime.Hosting.Contracts;
    using Microsoft.Dynamics.Commerce.Runtime.Messages;
    using Microsoft.Dynamics.Commerce.Runtime.Messages.Orders;
    using Microsoft.Dynamics.Commerce.Runtime.Services.Messages;
    using System.Collections.Generic;

    using Microsoft.Dynamics.Commerce.Runtime.Data.Types;
    using Microsoft.Dynamics.Commerce.Runtime.DataServices.Messages;
    using Commerce.Runtime.QueryCSU;
    using System.Text;
    using System.IO;
    using System;

    public class CSUDataQueryController : IController
    {
        [HttpPost]
        [Authorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
    
        public async Task<string> CSUQueryForCSVAsync(IEndpointContext context, string sqlText, QueryResultSettings settings)
        {
            ThrowIf.NullOrWhiteSpace(sqlText, nameof(sqlText));
            
            var request = new CSUQueryDataSetDataRequest(sqlText, settings);
            var response = await context.ExecuteAsync<SingleEntityDataServiceResponse<DataSet>>(request).ConfigureAwait(false);

            string outputFilePath = RetailServerUtils.saveDataTableRoCSV(response.Entity.Tables[0]);

            return outputFilePath;
        }

        [HttpPost]
        [Authorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        public async Task<string> CSUQueryForHtmlAsync(IEndpointContext context, string sqlText, QueryResultSettings settings)
        {
            ThrowIf.NullOrWhiteSpace(sqlText, nameof(sqlText));

            var request = new CSUQueryDataSetDataRequest(sqlText, settings);
            var response = await context.ExecuteAsync<SingleEntityDataServiceResponse<DataSet>>(request).ConfigureAwait(false);

            string htmlString = RetailServerUtils.convertDataTableToHtml(response.Entity.Tables[0]);

            return htmlString;
        }
    }
}

