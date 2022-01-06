using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Dynamics.Commerce.Runtime;
using Microsoft.Dynamics.Commerce.Runtime.Data.Types;
using Microsoft.Dynamics.Commerce.Runtime.Messages;
using Microsoft.Dynamics.Commerce.Runtime.DataServices.Messages;
using System.Diagnostics;
using Microsoft.Dynamics.Commerce.Runtime.Data;
using Microsoft.Dynamics.Commerce.Runtime.DataModel;
using Microsoft.Dynamics.Commerce.Runtime.Messages.Orders;
using Microsoft.Dynamics.Commerce.Runtime.Services.Messages;
using Commerce.Runtime.CWQtyLimitation.Messages;
using System.Globalization;

namespace Commerce.Runtime.CWQtyLimitation
{
    public class ValidateSalesLinesQuantityLimitsService : IRequestHandlerAsync
    {
        public IEnumerable<Type> SupportedRequestTypes
        {
            get
            {
                return new[]
                {
                    typeof(ValidateSalesLinesQuantityRequest)
                };
            }
        }


        public async Task<Response> Execute(Request request)
        {
            ThrowIf.Null(request, nameof(request));

            switch (request)
            {
                case ValidateSalesLinesQuantityRequest validateSalesLinesQuantityRequest:
                    return await this.ValidateCartSalesLineQuantity(validateSalesLinesQuantityRequest);
                default:
                    throw new NotSupportedException(string.Format("Request '{0}' is not supported.", request.GetType()));
            }
        }


        public async Task<Response> ValidateCartSalesLineQuantity(ValidateSalesLinesQuantityRequest request)
        {
            ThrowIf.Null(request, nameof(request));
            ThrowIf.NullOrWhiteSpace(request.CartId, "request.CartId");

            //If do have, and then no validation failures
            List<DataValidationFailure> failures = new List<DataValidationFailure>();

            if (await this.CheckAccess(request))
            {
                return new ValidateSalesLinesQuantityResponse(failures);
            }

            var getCartRequest = new GetCartRequest(
                new CartSearchCriteria(request.CartId, request.CartVersion),
                QueryResultSettings.SingleRecord,
                includeHistoricalTenderLines: false,
                ignoreProductDiscontinuedNotification: false);

            var getCartResponse = await request.RequestContext.ExecuteAsync<GetCartResponse>(getCartRequest).ConfigureAwait(false);
            SalesTransaction salesTransaction = getCartResponse.Transactions.SingleOrDefault();
            if (salesTransaction == null)
            {
                throw new CartValidationException(DataValidationErrors.Microsoft_Dynamics_Commerce_Runtime_CartNotFound, request.CartId);
            }

            var validateSalesLinesQuantityLimitsRequest = new ValidateSalesLinesQuantityLimitsRequest(salesTransaction);
            var validateSalesLinesQuantityLimitsResponse = await request.RequestContext.ExecuteAsync<ValidateSalesLinesQuantityLimitsResponse>(validateSalesLinesQuantityLimitsRequest).ConfigureAwait(false);
            if (validateSalesLinesQuantityLimitsResponse.CartLineValidationResults.HasErrors)
            {
                List<long> productIds = new List<long>();
                List<ProductBehavior> productBehaviors = new List<ProductBehavior>();
                foreach (var line in validateSalesLinesQuantityLimitsResponse.CartLineValidationResults.ValidationFailuresByCartLines)
                {
                    ProductBehavior productBehavior = validateSalesLinesQuantityLimitsResponse.LineIndexToProductBehaviorMap[line.LineIndex];
                    productBehaviors.Add(productBehavior);
                    productIds.Add(productBehavior.ProductId);
                }

                GetChannelIdServiceResponse getChannelIdServiceResponse = await request.RequestContext.ExecuteAsync<GetChannelIdServiceResponse>(new GetChannelIdServiceRequest()).ConfigureAwait(false);

                GetProductsServiceRequest getProductsServiceRequest = new GetProductsServiceRequest(getChannelIdServiceResponse.ChannelId, productIds, new QueryResultSettings(PagingInfo.AllRecords))
                {
                    SearchLocation = SearchLocation.All
                };

                GetProductsServiceResponse getProductsServiceResponse = await request.RequestContext.ExecuteAsync<GetProductsServiceResponse>(getProductsServiceRequest);
                List<SimpleProduct> products = new List<SimpleProduct>(getProductsServiceResponse.Products);

                foreach (var line in validateSalesLinesQuantityLimitsResponse.CartLineValidationResults.ValidationFailuresByCartLines)
                {
                    ProductBehavior productBehavior = validateSalesLinesQuantityLimitsResponse.LineIndexToProductBehaviorMap[line.LineIndex];
                    SimpleProduct product = products.Find(p => p.RecordId == productBehavior.ProductId);
                    string failureMsg = line.DataValidationFailure.ErrorContext;

                    DataValidationFailure dataValidationFailure = new DataValidationFailure(DataValidationErrors.None,
                        string.Format("Line {0:d},product: {1}, validate failure: {2}", line.LineIndex, product.Name, failureMsg));

                    failures.Add(dataValidationFailure);

                }

                return new ValidateSalesLinesQuantityResponse(failures);
            }

            CartLineValidationResults validationResult = validateSalesLinesQuantityLimitsResponse.CartLineValidationResults;
            return new ValidateSalesLinesQuantityResponse(validationResult.AggregateValidationFailures);
        }

        private async Task<bool> CheckAccess(ValidateSalesLinesQuantityRequest request)
        {
            // Check if the current staff has permission to to Larger Quantity operation.
            string staffId = request.RequestContext.Runtime.CurrentPrincipal.UserId;
            var getEmployeePermissionsDataRequest = new GetEmployeePermissionsDataRequest(staffId);
            var getEmployeePermissionsDataResponse = await request.RequestContext.ExecuteAsync<SingleEntityDataServiceResponse<EmployeePermissions>>(getEmployeePermissionsDataRequest).ConfigureAwait(false);
            bool canSellLargeQuantity = getEmployeePermissionsDataResponse.Entity.AllowXReportPrinting;

            return await Task.FromResult(canSellLargeQuantity);
        }
    }
}


