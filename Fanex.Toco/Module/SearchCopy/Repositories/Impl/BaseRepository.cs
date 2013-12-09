// --------------------------------------------------------------------------
// <copyright file="BaseRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kos.Nguyen" date="2012.8.15">Created</change>
//   <change who="Thai.Tran" date="2013.03.04">Add "ReturnICommandResult", "ReturnDataReader", "ReturnDataCollection"</change>
// </history>
namespace Fanex.CustomerSetting.Module.SearchCopy.Repository.Impl
{
    using System;
    using System.Collections;
    using System.Data;    
    using Fanex.CustomerSetting.Module.SearchCopy.Criteria;
    using Fanex.CustomerSetting.Module.SearchCopy.Repository.Interface;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Class RepositoryBase.
    /// </summary>    
    public class BaseRepository : IBaseRepository
    {
        /// <summary>
        /// Gets the command result.
        /// </summary>
        /// <param name="hash">The hash table.</param>
        /// <returns>Command Result.</returns>
        public CommandResult GetCommandResult(Hashtable hash)
        {
            var errCode = hash["errcode"];
            var errMsg = hash["errmsg"];

            return new CommandResult
            {
                Code = (errCode is DBNull) ? 0 : Convert.ToInt32(errCode),
                Message = (errMsg is DBNull) ? string.Empty : Convert.ToString(errMsg)
            };
        }

        /// <summary>
        /// Returns the single value.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>System Object.</returns>
        public object ReturnSingleValueByColumnName(ICriteria criteria, string columnName)
        {
            // prepare SP parameters
            Hashtable param = criteria.GetSpParams();
            param = (new ResultCriteria()).AddSpParams(param);

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                objDb.ExecuteNonQuery(param);
                return param[columnName];
            }
        }

        /// <summary>
        /// Returns the single value.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>System Object.</returns>
        public object ReturnSingleValue(ICriteria criteria)
        {
            try
            {
                // prepare SP parameters
                Hashtable hash = criteria.GetSpParams();                
                hash = (new ResultCriteria()).AddSpParams(hash);

                var db = new ObjectDb(criteria.GetSettingKey());
                db.AssignParamValues(hash);
                return db.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Returns the i command result.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="logCriteria">The log criteria.</param>
        /// <returns>ICommand Result.</returns>
        public ICommandResult ReturnICommandResult(ICriteria criteria, ICriteria logCriteria)
        {
            try
            {
                // prepare SP parameters
                Hashtable hash = criteria.GetSpParams();

                hash = logCriteria.AddSpParams(hash);
                hash = (new ResultCriteria()).AddSpParams(hash);

                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    db.ExecuteNonQuery(hash);

                    return GetCommandResult(hash);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Returns the data reader.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="logCriteria">The log criteria.</param>
        /// <returns>IData Reader.</returns>
        public IDataReader ReturnDataReader(ICriteria criteria, ICriteria logCriteria)
        {
            try
            {
                // prepare SP parameters
                Hashtable hash = criteria.GetSpParams();

                hash = logCriteria.AddSpParams(hash);
                hash = (new ResultCriteria()).AddSpParams(hash);

                var db = new ObjectDb(criteria.GetSettingKey());                
                db.AssignParamValues(hash);
                return db.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Returns the data set.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="logCriteria">The log criteria.</param>
        /// <returns>The DataSet.</returns>
        public DataSet ReturnDataSet(ICriteria criteria, ICriteria logCriteria)
        {
            try
            {
                // prepare SP parameters
                Hashtable hash = criteria.GetSpParams();

                hash = logCriteria.AddSpParams(hash);
                hash = (new ResultCriteria()).AddSpParams(hash);

                var db = new ObjectDb(criteria.GetSettingKey());
                db.AssignParamValues(hash);
                return db.ExecuteDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }    
    }
}