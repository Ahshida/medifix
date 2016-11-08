using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using DBO.Data.Interfaces;

namespace DBO.Data.Managers
{
    public class DataToDBManager
    {
        public void DataCopyToDBObject(object item, ITable table)
        {
            var tableProperties = table.GetType().GetProperties();
            foreach (var propertyInfo in item.GetType().GetProperties())
            {
                var column = propertyInfo.GetValue(item, null);
                if (!propertyInfo.PropertyType.IsClass || typeof(string).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    var tableProperty = tableProperties.Where(p => string.Equals(p.Name, propertyInfo.Name)).FirstOrDefault();
                    if (tableProperty != null)
                        tableProperty.SetValue(table, column, null);
                }
                else
                    DataCopyToDBObject(column, table);
            }
        }

        public string DataToDBFormat(object item, string tableName)
        {
            var type = item.GetType();

            StringBuilder txt = new StringBuilder();
            txt.AppendLine("USE [ACE]");
            txt.AppendLine("GO");

            txt.AppendLine("SET ANSI_NULLS ON");
            txt.AppendLine("GO");

            txt.AppendLine("SET QUOTED_IDENTIFIER ON");
            txt.AppendLine("GO");

            txt.AppendLine("SET ANSI_PADDING ON");
            txt.AppendLine("GO");

            txt.AppendLine(string.Format("CREATE TABLE [dbo].[{0}] (", tableName));
            List<string> columns = new List<string>();
            columns.Add("\t[ID] [int] NOT NULL");
            foreach (var propertyInfo in type.GetProperties())
            {
                columns.AddRange(GetColumn(propertyInfo));
            }
            columns.Add(string.Format("\t[CreatedBy] {0}", GetColumnType(typeof(int))));
            columns.Add(string.Format("\t[CreatedDate] {0}", GetColumnType(typeof(DateTime?))));
            columns.Add(string.Format("\t[ModifiedBy] {0}", GetColumnType(typeof(int))));
            columns.Add(string.Format("\t[ModifiedDate] {0}", GetColumnType(typeof(DateTime?))));
            columns.Add(string.Format("\tCONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]", tableName));
            txt.Append(string.Join(",\n", columns));
            txt.AppendLine(") ON [PRIMARY]");

            txt.AppendLine("");
            txt.AppendLine("GO");
            txt.AppendLine("SET ANSI_PADDING OFF");
            txt.AppendLine("GO");
            txt.AppendLine(string.Format("ALTER TABLE [dbo].[{0}]  WITH CHECK ADD  CONSTRAINT [FK_{0}_CreatedBy] FOREIGN KEY([CreatedBy])", tableName));
            txt.AppendLine("REFERENCES [dbo].[T_User] ([ID])");
            txt.AppendLine("GO");
            txt.AppendLine(string.Format("ALTER TABLE [dbo].[{0}] CHECK CONSTRAINT [FK_{0}_CreatedBy]", tableName));
            txt.AppendLine("GO");
            txt.AppendLine(string.Format("ALTER TABLE [dbo].[{0}]  WITH CHECK ADD  CONSTRAINT [FK_{0}_ModifiedBy] FOREIGN KEY([ModifiedBy])", tableName));
            txt.AppendLine("REFERENCES [dbo].[T_User] ([ID])");
            txt.AppendLine("GO");
            txt.AppendLine(string.Format("ALTER TABLE [dbo].[{0}] CHECK CONSTRAINT [FK_{0}_ModifiedBy]", tableName));
            txt.AppendLine("GO");

            return txt.ToString();
        }

        private List<string> GetColumn(PropertyInfo propertyInfo)
        {
            List<string> list = new List<string>();
            if (!propertyInfo.PropertyType.IsClass || typeof(string).IsAssignableFrom(propertyInfo.PropertyType))
                list.Add(string.Format("\t[{0}] {1}", propertyInfo.Name, GetColumnType(propertyInfo.PropertyType)));
            else
            {
                foreach (var p in propertyInfo.PropertyType.GetProperties())
                    list.AddRange(GetColumn(p));
            }

            return list;
        }

        private string GetColumnType(Type propertyType)
        {
            if (typeof(int).IsAssignableFrom(propertyType))
                return "[int] NOT NULL";
            else if (typeof(int?).IsAssignableFrom(propertyType))
                return "[int] NULL";
            else if (typeof(long).IsAssignableFrom(propertyType))
                return "[bigint] NOT NULL";
            else if (typeof(long?).IsAssignableFrom(propertyType))
                return "[bigint] NULL";
            else if (typeof(bool).IsAssignableFrom(propertyType))
                return "[bit] NOT NULL";
            else if (typeof(bool?).IsAssignableFrom(propertyType))
                return "[bit] NULL";
            else if (typeof(DateTime).IsAssignableFrom(propertyType))
                return "[datetime] NOT NULL";
            else if (typeof(DateTime?).IsAssignableFrom(propertyType))
                return "[datetime] NULL";
            else if (typeof(string).IsAssignableFrom(propertyType))
                return "[nvarchar](250) NULL";
            else
                return "[nvarchar](250)";
        }
    }
}
