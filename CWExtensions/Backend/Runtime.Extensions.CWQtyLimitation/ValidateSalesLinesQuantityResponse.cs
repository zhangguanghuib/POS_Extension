using System;
using System.Collections.Generic;
using System.Text;

namespace Commerce.Runtime.CWQtyLimitation.Messages
{
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;
    //using Commerce.Runtime.DataModel;
    using Microsoft.Dynamics.Commerce.Runtime;
    using Microsoft.Dynamics.Commerce.Runtime.Messages;

    public class ValidateSalesLinesQuantityResponse: Response
    {
        public ValidateSalesLinesQuantityResponse(List<DataValidationFailure> failures)
        {
            this.Failures = new PagedResult<DataValidationFailure>(new System.Collections.ObjectModel.ReadOnlyCollection<DataValidationFailure>(failures));
        }

        public ValidateSalesLinesQuantityResponse(ReadOnlyCollection<DataValidationFailure> failures)
        {
            this.Failures = new PagedResult<DataValidationFailure>(failures);
        }

        [DataMember]
        public PagedResult<DataValidationFailure> Failures { get; private set; }
    }
}
