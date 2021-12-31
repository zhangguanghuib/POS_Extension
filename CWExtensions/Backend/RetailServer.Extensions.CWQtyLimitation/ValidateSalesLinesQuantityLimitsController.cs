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

            var getCartRequest = new GetCartRequest(
                new CartSearchCriteria(cartId, cartVersion),
                QueryResultSettings.SingleRecord,
                includeHistoricalTenderLines: false,
                ignoreProductDiscontinuedNotification: false);

            var getCartResponse = await context.ExecuteAsync<GetCartResponse>(getCartRequest).ConfigureAwait(false);

            SalesTransaction salesTransaction = getCartResponse.Transactions.SingleOrDefault();
            
            if (salesTransaction == null)
            {
                throw new CartValidationException(DataValidationErrors.Microsoft_Dynamics_Commerce_Runtime_CartNotFound, cartId);
            }

            var validateSalesLinesQuantityLimitsRequest = new ValidateSalesLinesQuantityLimitsRequest(salesTransaction);

            var validateSalesLinesQuantityLimitsResponse = await context.ExecuteAsync<ValidateSalesLinesQuantityLimitsResponse>(validateSalesLinesQuantityLimitsRequest).ConfigureAwait(false);

            if(validateSalesLinesQuantityLimitsResponse.CartLineValidationResults.HasErrors)
            {
                GetChannelIdServiceResponse getChannelIdServiceResponse = await context.ExecuteAsync<GetChannelIdServiceResponse>(new GetChannelIdServiceRequest()).ConfigureAwait(false);

                List<long> productIds = new List<long>();
                List<ProductBehavior> productBehaviors = new List<ProductBehavior>();

                foreach (var line in validateSalesLinesQuantityLimitsResponse.CartLineValidationResults.ValidationFailuresByCartLines)
                {
                    ProductBehavior productBehavior = validateSalesLinesQuantityLimitsResponse.LineIndexToProductBehaviorMap[line.LineIndex];
                    productBehaviors.Add(productBehavior);
                    productIds.Add(productBehavior.ProductId);  
                }

                GetProductsServiceRequest getProductsServiceRequest = new GetProductsServiceRequest(getChannelIdServiceResponse.ChannelId, productIds, new QueryResultSettings(PagingInfo.AllRecords))
                {
                    SearchLocation = SearchLocation.All
                };

                GetProductsServiceResponse getProductsServiceResponse = await context.ExecuteAsync<GetProductsServiceResponse>(getProductsServiceRequest);
                List<SimpleProduct> products = new List<SimpleProduct>(getProductsServiceResponse.Products);

                List<DataValidationFailure> failures = new List<DataValidationFailure>();

                foreach (var line in validateSalesLinesQuantityLimitsResponse.CartLineValidationResults.ValidationFailuresByCartLines)
                {
                    ProductBehavior productBehavior = validateSalesLinesQuantityLimitsResponse.LineIndexToProductBehaviorMap[line.LineIndex];
                    SimpleProduct product =  products.Find(p => p.RecordId == productBehavior.ProductId);
                    string failureMsg = line.DataValidationFailure.ErrorContext;

                    //string.Format("Line {0:d},product: {1}, validate failure: {2}", line.LineIndex, product.Name, failureMsg);

                    DataValidationFailure dataValidationFailure = new DataValidationFailure(DataValidationErrors.None,
                        string.Format("Line {0:d},product: {1}, validate failure: {2}", line.LineIndex, product.Name, failureMsg));

                    failures.Add(dataValidationFailure);

                }

                return new PagedResult<DataValidationFailure>(new System.Collections.ObjectModel.ReadOnlyCollection<DataValidationFailure>(failures));
            }

            CartLineValidationResults validationResult = validateSalesLinesQuantityLimitsResponse.CartLineValidationResults;

            return new PagedResult<DataValidationFailure>(validationResult.AggregateValidationFailures);

        }
    }
}
