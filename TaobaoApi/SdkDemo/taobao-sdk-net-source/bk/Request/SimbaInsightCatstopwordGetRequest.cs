using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.simba.insight.catstopword.get
    /// </summary>
    public class SimbaInsightCatstopwordGetRequest : ITopRequest<SimbaInsightCatstopwordGetResponse>
    {
        /// <summary>
        /// 类目id数组，最大长度200
        /// </summary>
        public string CategoryIds { get; set; }

        /// <summary>
        /// 主人昵称
        /// </summary>
        public string Nick { get; set; }

        /// <summary>
        /// 最大返回数量(1-100)
        /// </summary>
        public Nullable<long> ResultNum { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.simba.insight.catstopword.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("category_ids", this.CategoryIds);
            parameters.Add("nick", this.Nick);
            parameters.Add("result_num", this.ResultNum);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("category_ids", this.CategoryIds);
            RequestValidator.ValidateMaxListSize("category_ids", this.CategoryIds, 200);
            RequestValidator.ValidateRequired("result_num", this.ResultNum);
            RequestValidator.ValidateMaxValue("result_num", this.ResultNum, 100);
            RequestValidator.ValidateMinValue("result_num", this.ResultNum, 1);
        }

        #endregion
    }
}
