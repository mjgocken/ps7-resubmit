using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Text;

namespace SWARM.Server.Data
{
    public static class DbContextCaseSensitive
    {

        public static void FinalAdjustments(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var entityProperty in entityType.GetProperties())
                {
                    //  Fix "Id" column so it's TABLE_NAME || Id
                    //if (entityProperty.Name.Equals("Id"))
                    //{
                    //    entityProperty.SetColumnName(entityType.GetTableName() + "_ID");
                    //}

                    //  Fix datatype for ASP_NET_ROLES.ScrtyLevelScrtyLevelId
                    if ((entityType.GetTableName() == "ASP_NET_ROLES") && (entityProperty.Name.Equals("ScrtyLevelScrtyLevelId")))
                    {
                        entityProperty.SetIsUnicode(false);
                    }
                }

            }
        }

        public static void AddFootprintColumns(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {

                entityType.AddProperty(entityType.GetTableName() + "_CRTD_ID", typeof(string)).SetMaxLength(40);
                entityType.AddProperty(entityType.GetTableName() + "_CRTD_DT", typeof(DateTime)).SetDefaultValueSql("SYSDATE");
                entityType.AddProperty(entityType.GetTableName() + "_UPDT_ID", typeof(string)).SetMaxLength(40);
                entityType.AddProperty(entityType.GetTableName() + "_UPDT_DT", typeof(DateTime)).SetDefaultValueSql("SYSDATE");
            }
        }

        public static String AddUnderscoreUppercase(String strWord)
        {
            StringBuilder strWordFix = new StringBuilder();
            strWordFix.Append(strWord.Substring(0, 1).ToUpper());
            for (int i = 1; i < strWord.ToString().Length; i++)
            {
                var c = strWord.Substring(i, 1).ToCharArray();
                if (Char.IsUpper(c[0]))
                {
                    strWordFix.Append("_");
                    strWordFix.Append(strWord.Substring(i, 1).ToUpper());
                }
                else
                {
                    strWordFix.Append(strWord.Substring(i, 1).ToUpper());
                }
            }
            return strWordFix.ToString();
        }


        /// <summary>
        /// Set table's name to Uppercase
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void ToUpperCaseTables(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(AddUnderscoreUppercase(entityType.GetTableName()));
            }
        }


        /// <summary>
        /// Set column's name to Uppercase 
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void ToUpperCaseColumns(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    property.SetColumnName(AddUnderscoreUppercase(property.GetColumnName()));
                }
            }
        }

        /// <summary>
        /// Set foreignkey's name to Uppercase
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void ToUpperCaseForeignKeys(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    foreach (var fk in entityType.FindForeignKeys(property))
                    {
                        fk.SetConstraintName(fk.GetConstraintName().ToUpper());
                    }
                }
            }
        }

        /// <summary>
        /// Set index's name to Uppercase
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void ToUpperCaseIndexes(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var index in entityType.GetIndexes())
                {
                    index.SetName(index.GetName().ToUpper());
                }
            }
        }
    }
}
