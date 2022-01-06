using System;

namespace RetailServer.Extensions.CWQtyLimitation
{
    using System.Threading.Tasks;
    using Microsoft.Dynamics.Commerce.Runtime;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using Microsoft.Dynamics.Commerce.Runtime.Hosting.Contracts;
    using Microsoft.Dynamics.Commerce.Runtime.Messages;
    using Microsoft.Dynamics.Commerce.Runtime.Messages.Orders;
    using Microsoft.Dynamics.Commerce.Runtime.Services.Messages;
    using System.Collections.Generic;
    using Commerce.Runtime.CWQtyLimitation;
    using Commerce.Runtime.CWQtyLimitation.Messages;

    public class ValidateSalesLinesQuantityLimitsController : IController
    {
        /// <summary>
        /// Gets the store hours for a given store.
        /// </summary>
        /// <param name="parameters">The parameters to this action.</param>
        /// <returns>The list of store hours.</returns>
        [HttpPost]
        [Authorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        public async Task<PagedResult<DataValidationFailure>> GetQtyValidationResultByCart(IEndpointContext context, string cartId, long cartVersion, QueryResultSettings queryResultSettings)
        {
            ThrowIf.Null(context, nameof(context));

            ValidateSalesLinesQuantityRequest request = new ValidateSalesLinesQuantityRequest(cartId, cartVersion, queryResultSettings);

            var validateSalesLinesQuantityResponse = await context.ExecuteAsync<ValidateSalesLinesQuantityResponse>(request).ConfigureAwait(false);

            return validateSalesLinesQuantityResponse.Failures;

        }
    }
}
