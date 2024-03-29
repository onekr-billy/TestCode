using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.simba.creatives.record.get
    /// </summary>
    public class SimbaCreativesRecordGetRequest : ITopRequest<SimbaCreativesRecordGetResponse>
    {
        /// <summary>
        /// 创意Id数组，最多200个
        /// </summary>
        public string CreativeIds { get; set; }

        /// <summary>
        /// 主人昵称
        /// </summary>
        public string Nick { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.simba.creatives.record.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("creative_ids", this.CreativeIds);
            parameters.Add("nick", this.Nick);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("creative_ids", this.CreativeIds);
            RequestValidator.ValidateMaxListSize("creative_ids", this.CreativeIds, 200);
        }

        #endregion
    }
}
