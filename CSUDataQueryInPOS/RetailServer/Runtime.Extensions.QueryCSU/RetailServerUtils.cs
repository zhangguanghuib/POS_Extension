namespace Commerce.Runtime.QueryCSU
{
    using Microsoft.Dynamics.Commerce.Runtime.Data.Types;
    using System;
    using System.IO;
    using System.Text;

    public class RetailServerUtils
    {
        public static string convertDataTableToHtml(DataTable dt)
        {
            if (dt.Rows.Count == 0) return ""; // enter code here

            StringBuilder builder = new StringBuilder();
            builder.Append("<html>");
            builder.Append("<head>");
            builder.Append("<title>");
            builder.Append("Page-");
            builder.Append(Guid.NewGuid());
            builder.Append("</title>");
            builder.Append("</head>");
            builder.Append("<body>");
            builder.Append("<table border='1px' cellpadding='5' cellspacing='0' ");
            builder.Append("style='border: solid 1px Silver; font-size: x-small;'>");
            builder.Append("<tr align='left' valign='top'>");
            foreach (DataColumn c in dt.Columns)
            {
                builder.Append("<td align='left' valign='top'><b>");
                builder.Append(c.ColumnName);
                builder.Append("</b></td>");
            }
            builder.Append("</tr>");
            foreach (DataRow r in dt.Rows)
            {
                builder.Append("<tr align='left' valign='top'>");
                foreach (DataColumn c in dt.Columns)
                {
                    builder.Append("<td align='left' valign='top'>");
                    builder.Append(r[c.ColumnName]);
                    builder.Append("</td>");
                }
                builder.Append("</tr>");
            }
            builder.Append("</table>");
            builder.Append("</body>");
            builder.Append("</html>");

            return builder.ToString();
        }

        public static string saveDataTableRoCSV(DataTable dataTable)
        {
            string outputFilePath = "";

            if (String.IsNullOrWhiteSpace(outputFilePath))
            {
                outputFilePath = RetailServerUtils.getFileName();
            }
            StringBuilder sbCsvContent;
            try
            {
                sbCsvContent = new StringBuilder();
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    sbCsvContent.Append(dataTable.Columns[i].ColumnName);
                    sbCsvContent.Append(i == dataTable.Columns.Count - 1 ? "\r\n" : ",");
                }

                foreach (var row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        sbCsvContent.Append(((DbElementCollection<object>)row)[i]);
                        sbCsvContent.Append(i == dataTable.Columns.Count - 1 ? "\r\n" : ",");
                    }
                }

                File.WriteAllText(outputFilePath, sbCsvContent.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return outputFilePath;
        }

        private static string getFileName()
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
            String fileName =
                "" + dateTimeOffset.Year
                + '_' + dateTimeOffset.Month
                + '-' + dateTimeOffset.Day
                + '_' + dateTimeOffset.Hour
                + '-' + dateTimeOffset.Minute
                + '_' + dateTimeOffset.Minute
                + '-' + dateTimeOffset.Millisecond;
            return @"C:\Temp\" + fileName + '_' + ".csv";
        }
    }
}
