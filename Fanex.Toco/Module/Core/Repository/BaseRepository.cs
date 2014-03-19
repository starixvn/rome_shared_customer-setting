// --------------------------------------------------------------------------
// <copyright file="BaseRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Repository
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// The base repository.
    /// </summary>
    public class BaseRepository : DynamicRepository
    {
        /// <summary>
        /// Fetch Data from Criteria.
        /// </summary>
        /// <typeparam name="T">The class type of returned list.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="mapObjectFunction">Read data reader and pass object.</param>
        /// <returns>
        /// Collection of object.
        /// </returns>
        public virtual IList<T> Fetch<T>(ICriteria criteria, Func<IDataReader, T> mapObjectFunction) where T : class
        {
            IList<T> listObject = new List<T>();
            if (criteria.IsValid())
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                    {
                        while (reader.Read())
                        {
                            listObject.Add(mapObjectFunction(reader));
                        }
                    }
                }
            }

            return listObject;
        }

        /// <summary>
        /// Execute command to DB.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The HashTable object.</returns>
        public virtual Hashtable ExecuteCommand(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());
                return db.GetOuputParams();
            }
        }

        /// <summary>
        /// Get DataTable and Output parameters from Criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="outputParams">The output parameters.</param>
        /// <returns>The DataTable object.</returns>
        public virtual DataTable Fetch(ICriteria criteria, ref Hashtable outputParams)
        {
            if (criteria.IsValid())
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    DataSet dataSet = db.ExecuteDataSet(criteria.GetSpParams());
                    if (dataSet != null
                        && dataSet.Tables.Count > 0)
                    {
                        outputParams = db.GetOuputParams();
                        return dataSet.Tables[0];
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Get DataTable from Criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The DataTable object.</returns>
        public virtual DataTable Fetch(ICriteria criteria)
        {
            if (criteria.IsValid())
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    DataSet dataSet = db.ExecuteDataSet(criteria.GetSpParams());
                    if (dataSet != null
                        && dataSet.Tables.Count > 0)
                    {
                        return dataSet.Tables[0];
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Get DataTable and Output parameters from Criteria.
        /// </summary>
        /// <typeparam name="T">The class type of returned list.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="mapObjectFunction">The map object function.</param>
        /// <returns>The data list.</returns>
        public virtual IList<T> Fetch<T>(ICriteria criteria, Func<DataRow, T> mapObjectFunction) where T : class
        {
            IList<T> listData = new List<T>();
            if (criteria.IsValid())
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    DataSet dataSet = db.ExecuteDataSet(criteria.GetSpParams());
                    if (dataSet != null
                        && dataSet.Tables.Count > 0)
                    {
                        DataTable currentTable = dataSet.Tables[0];
                        foreach (DataRow row in currentTable.Rows)
                        {
                            listData.Add(mapObjectFunction(row));
                        }
                    }
                }
            }

            return listData;
        }

        /// <summary>
        /// Get DataTable and Output parameters from Criteria.
        /// </summary>
        /// <typeparam name="T">The class type of returned list.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="mapObjectFunction">The map object function.</param>
        /// <param name="outputParams">The output parameters.</param>
        /// <returns>The data list.</returns>
        public virtual IList<T> Fetch<T>(ICriteria criteria, Func<DataRow, T> mapObjectFunction, ref Hashtable outputParams) where T : class
        {
            IList<T> listData = new List<T>();
            if (criteria.IsValid())
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    DataSet dataSet = db.ExecuteDataSet(criteria.GetSpParams());
                    outputParams = db.GetOuputParams();
                    if (dataSet != null
                        && dataSet.Tables.Count > 0)
                    {
                        DataTable currentTable = dataSet.Tables[0];
                        foreach (DataRow row in currentTable.Rows)
                        {
                            listData.Add(mapObjectFunction(row));
                        }
                    }
                }
            }

            return listData;
        }
    }
}
