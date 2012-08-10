using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.caipiao.signstatus.check
    /// </summary>
    public class CaipiaoSignstatusCheckRequest : ITopRequest<CaipiaoSignstatusCheckResponse>
    {
        /// <summary>
        /// 用户的淘宝数字ID
        /// </summary>
        public Nullable<long> UserNumId { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.caipiao.signstatus.check";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("user_num_id", this.UserNumId);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("user_num_id", this.UserNumId);
        }

        #endregion
    }
}
