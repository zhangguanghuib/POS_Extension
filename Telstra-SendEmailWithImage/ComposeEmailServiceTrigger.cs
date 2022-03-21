using Microsoft.Dynamics.Commerce.Runtime;
using Microsoft.Dynamics.Commerce.Runtime.Messages;
using Microsoft.Dynamics.Commerce.Runtime.Services.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Dynamics.Commerce.Runtime.DataModel;
using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Dynamics.Commerce.Runtime.DataServices.Messages;

namespace Runtime.Extensions.TelstraEmailImage
{
    public class ComposeEmailServiceTrigger : IRequestTriggerAsync
    {
        internal const string ImageBase64Data = "imagebase64data";

        /// <summary>
        /// Gets the supported requests for this trigger.
        /// </summary>
        public IEnumerable<Type> SupportedRequestTypes
        {
            get
            {
                return new[] { typeof(ComposeEmailServiceRequest) };
            }
        }

        /// <summary>
        /// Pre trigger code
        /// </summary>
        /// <param name="request">The request.</param>
        public async Task OnExecuting(Request request)
        {
            // Version 1:  <html><body><img src="data:image/png;base64,  %imagebase64data%" alt="" /><pre>%message%</pre></body></html>
            //if (request is ComposeEmailServiceRequest)
            //{
            //    ComposeEmailServiceRequest req = (ComposeEmailServiceRequest)request;
            //    IDictionary<string, string> placeholderReplacements = req.PlaceholderMappings;

            //    string imageUrl = @"https://usnconeboxax1ret.cloud.onebox.dynamics.com/MediaServer/Products/0002_000_001.png";
            //    placeholderReplacements.Add(ImageBase64Data, await this.ConvertImageURLToBase64Async(imageUrl));
            //}
			
			// select * from dbo.SYSEMAILMESSAGETABLE as T where T.EMAILID = 'EmailRecpt'
            //select * from ax.SYSEMAILMESSAGETABLE as T where T.EMAILID = 'EmailRecpt'

            // update ax.SYSEMAILMESSAGETABLE
            //set MAIL = '<html><body><img src="%https://usnconeboxax1ret.cloud.onebox.dynamics.com/MediaServer/Products/0003_000_001.png%" alt="" /><pre>%message%</pre></body></html>'
            // where RECID = 11290925407
            //Version 2:  <html><body><img src="%https://usnconeboxax1ret.cloud.onebox.dynamics.com/MediaServer/Products/0002_000_001.png%" alt="" /><pre>%message%</pre></body></html>
            if (request is ComposeEmailServiceRequest)
            {
                ComposeEmailServiceRequest req = (ComposeEmailServiceRequest)request;

                var getEmailTemplateRequest = new GetEmailTemplateDataRequest(req.TemplateId, req.LanguageId);
                var emailTemplate = (await req.RequestContext.ExecuteAsync<SingleEntityDataServiceResponse<EmailTemplate>>(getEmailTemplateRequest).ConfigureAwait(false)).Entity;

                string messageTemplate = emailTemplate.MessageTemplate;
                int index1 = -1, index2 = -1, index3 = -1;

                if ((index1 = messageTemplate.IndexOf("<img src=")) != -1 && (index2 = messageTemplate.IndexOf('%', index1)) != -1 && (index3 = messageTemplate.IndexOf('%', index2 + 1)) != -1)
                {
                    string imageUrl = messageTemplate.Substring(index2 + 1, index3 - index2 - 1);
                    IDictionary<string, string> placeholderReplacements = req.PlaceholderMappings;
                    placeholderReplacements.Add(imageUrl, await this.GetImageBase64DataURLAsync(imageUrl));
                }

            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Post trigger code.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        public async Task OnExecuted(Request request, Response response)
        {
            //if (request is ComposeEmailServiceRequest)
            //{
            //    ComposeEmailServiceRequest req = (ComposeEmailServiceRequest)request;
            //    ComposeEmailServiceResponse res = (ComposeEmailServiceResponse)response;

            //    EmailMessage emailMessage = res.ComposedEmail;

            //    // string imageUrl = "https://i.imgur.com/MNwsDvt.jpeg";
            //    string imageUrl = "https://usnconeboxax1ret.cloud.onebox.dynamics.com/MediaServer/Products/0002_000_001.png";
                
            //    // Version 1, sync mode
            //    //string imageBase64Data = this.ConvertImageURLToBase64(imageUrl);

            //    // Version 2, async mode
            //    string imageBase64Data = await this.ConvertImageURLToBase64Async(imageUrl);

            //    string imageBase64Url = "<img src = \"data:image/png;base64, " + imageBase64Data + "\" alt=\"Company Logo\" />";
            //    emailMessage.MessageBody += imageBase64Url;

            //    response = new ComposeEmailServiceResponse(emailMessage);
            //}
            await Task.CompletedTask;
        }

        public async Task<string> GetImageBase64DataURLAsync(string imageUrl)
        {
            string imageBase64Data = await this.ConvertImageURLToBase64Async(imageUrl);

            return ("data:image/png;base64, " + imageBase64Data);
        }

        public async Task<string> ConvertImageURLToBase64Async(string url)
        {
            StringBuilder _sb = new StringBuilder();

            Byte[] _byte = await GetImageAsyn(url);

            _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));

            return _sb.ToString();
        }

        private async Task<byte[]> GetImageAsyn(string url)
        {
            byte[] buf;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse httpResponse = (HttpWebResponse)(await req.GetResponseAsync());

                using (Stream responseStream = httpResponse.GetResponseStream())
                {
                    using (BinaryReader br = new BinaryReader(responseStream))
                    {
                        int len = (int)(httpResponse.ContentLength);
                        buf = br.ReadBytes(len);
                        br.Close();
                    }
                    responseStream.Close();
                }
                httpResponse.Close();
            }
            catch (Exception)
            {
                buf = null;
            }

            return buf;
        }

        // Sync Mode
        private  string ConvertImageURLToBase64(string url)
        {
            StringBuilder sb = new StringBuilder();

            byte[] bytes = GetImage(url);

            sb.Append(Convert.ToBase64String(bytes, 0, bytes.Length));

            return sb.ToString();
        }

        private  byte[] GetImage(string url)
        {
            Stream stream = null;
            byte[] buf;

            try
            {
                WebProxy myProxy = new WebProxy();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                stream = response.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    int len = (int)response.ContentLength;
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch (Exception)
            {
                buf = null;
            }

            return buf;
        }
    }
}
