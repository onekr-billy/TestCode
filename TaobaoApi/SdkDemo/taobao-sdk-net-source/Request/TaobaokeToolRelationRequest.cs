using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.taobaoke.tool.relation
    /// </summary>
    public class TaobaokeToolRelationRequest : ITopRequest<TaobaokeToolRelationResponse>
    {
        /// <summary>
        /// 用户的pubid 用来判断这个pubid是否是appkey关联的开发者的注册用户
        /// </summary>
        public Nullable<long> Pubid { get; set; }

        private IDictionary<string, string> otherParameters;

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.taobaoke.tool.relation";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("pubid", this.Pubid);
            parameters.AddAll(this.otherParameters);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("pubid", this.Pubid);
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
