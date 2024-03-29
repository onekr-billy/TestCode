using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: tmall.selected.items.search
    /// </summary>
    public class TmallSelectedItemsSearchRequest : ITopRequest<TmallSelectedItemsSearchResponse>
    {
        /// <summary>
        /// 后台类目ID，支持父类目或叶子类目，可以通过taobao.itemcats.get 获取到后台类目ID列表
        /// </summary>
        public Nullable<long> Cid { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "tmall.selected.items.search";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("cid", this.Cid);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("cid", this.Cid);
        }

        #endregion
    }
}
