// -----------------------------------------------------------------------
// <copyright file="IStatusRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <summary>
//  IStatusRepository
// </summary>

namespace Fanex.CustomerSetting.Module.KenoStatus.Repositories.Interface
{
    using System.Collections;
    using Fanex.CustomerSetting.Module.KenoStatus.Criteria;
    using Fanex.CustomerSetting.Module.KenoStatus.ViewModels;

    /// <summary>
    /// ProgressiveRepository Interface.
    /// </summary>
    public interface IStatusRepository
    {
        /// <summary>
        /// Get Enable Status.
        /// </summary>
        /// <param name="criteria">Parameter of stored procedure.</param>
        /// <returns>KenoEnableStatus object.</returns>
        KenoEnableStatus GetStatus(SelectStatusCriteria criteria);

        /// <summary>
        /// Update Enable Status for Keno.
        /// </summary>
        /// <param name="criteria">Parameter of stored procedure.</param>
        /// <returns>Result code of DB.</returns>
        Hashtable UpdateStatus(UpdateStatusCriteria criteria);
    }
}