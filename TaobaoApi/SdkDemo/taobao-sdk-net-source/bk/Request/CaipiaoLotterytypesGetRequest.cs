using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.caipiao.lotterytypes.get
    /// </summary>
    public class CaipiaoLotterytypesGetRequest : ITopRequest<CaipiaoLotterytypesGetResponse>
    {
        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.caipiao.lotterytypes.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            return parameters;
        }

        public void Validate()
        {
        }

        #endregion
    }
}
