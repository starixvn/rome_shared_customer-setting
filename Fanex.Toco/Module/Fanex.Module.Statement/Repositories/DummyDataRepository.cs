// --------------------------------------------------------------------------
// <copyright file="DummyDataRepository.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Repositories
{
    using System;

    /// <summary>
    /// Dummy Data Repository.
    /// </summary>
    public class DummyDataRepository
    {
        public static DateTime BalanceUpDay
        {
            get
            {
                return new DateTime(2014, 11, 29);
            }
        }

        public static DateTime MoveBetsDay
        {
            get
            {
                return new DateTime(2014, 12, 1);
            }
        }
    }
}
