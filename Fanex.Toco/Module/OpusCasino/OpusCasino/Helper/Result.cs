// -----------------------------------------------------------------------
// <copyright file="Result.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Fanex.OpusCasino
{
    [Serializable]
    public class Result
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
