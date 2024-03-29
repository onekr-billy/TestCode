using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.caipiao.present.win.items.get
    /// </summary>
    public class CaipiaoPresentWinItemsGetRequest : ITopRequest<CaipiaoPresentWinItemsGetResponse>
    {
        /// <summary>
        /// 查询个数，最大值为50.如果为空、0和负数，则取默认值50
        /// </summary>
        public Nullable<long> Num { get; set; }

        /// <summary>
        /// 淘宝数字ID，不可为空，0和负数。
        /// </summary>
        public Nullable<long> UserNumId { get; set; }

        private IDictionary<string, string> otherParameters;

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.caipiao.present.win.items.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("num", this.Num);
            parameters.Add("user_num_id", this.UserNumId);
            parameters.AddAll(this.otherParameters);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("user_num_id", this.UserNumId);
        }

        #endregion

        public void AddOtherParameter(string key, string value)
        {
            if (this.otherParameters == null)
            {
                this.otherParameters = new TopDictionary();
            }
            this.otherParameters.Add(key, value);
        }
    }
}
