// -----------------------------------------------------------------------
// <copyright file="Result.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.OpusCasino
{
    using System;

    /// <summary>
    /// Result Class.
    /// </summary>
    [Serializable]
    public class Result
    {
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}
