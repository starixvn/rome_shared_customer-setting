using Fanex.Data;
using Fanex.NAP.Framework.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory
{
    internal static class ObjectDbFactory
    {
        [ThreadStatic]
        private static Func<ICriteria, Func<IDataRecord, ProductStatus>, IEnumerable<ProductStatus>> executeReaderFunc;

        internal static Func<ICriteria, Func<IDataRecord, ProductStatus>, IEnumerable<ProductStatus>> ExecuteReaderFunc
        {
            get
            {
                return executeReaderFunc ?? ExecuteReader;
            }

            set
            {
                executeReaderFunc = value;
            }
        }

        [ThreadStatic]
        private static Func<ICriteria, Hashtable> executeNonQueryFunc;

        internal static Func<ICriteria, Hashtable> ExecuteNonQueryFunc
        {
            get
            {
                return executeNonQueryFunc ?? ExecuteNonQuery;
            }

            set
            {
                executeNonQueryFunc = value;
            }
        }

        private static IEnumerable<ProductStatus> ExecuteReader(ICriteria criteria, Func<IDataRecord, ProductStatus> mapDataRow)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.AssignParamValues(criteria.GetSpParams());
                var reader = db.ExecuteReader();
                while (reader.Read())
                {
                    yield return mapDataRow(reader);
                }
            }
        }

        private static Hashtable ExecuteNonQuery(ICriteria criteria)
        {
            Hashtable outputs;
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.AssignParamValues(criteria.GetSpParams());
                db.ExecuteNonQuery();
                outputs = db.GetOuputParams();
            }

            return outputs;
        }
    }
}
