using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.taobaoke.items.convert
    /// </summary>
    public class TaobaokeItemsConvertRequest : ITopRequest<TaobaokeItemsConvertResponse>
    {
        /// <summary>
        /// 需返回的字段列表.可选值:num_iid,title,nick,pic_url,price,click_url,commission,commission_rate,commission_num,commission_volume,shop_click_url,seller_credit_score,item_location,volume ;字段之间用","分隔.
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 标识一个应用是否来在无线或者手机应用,如果是true则会使用其他规则加密点击串.如果不穿值,则默认是false.
        /// </summary>
        public Nullable<bool> IsMobile { get; set; }

        /// <summary>
        /// 推广者的淘宝会员昵称.注：指的是淘宝的会员登录名
        /// </summary>
        public string Nick { get; set; }

        /// <summary>
        /// 淘宝客商品数字id串.最大输入40个.格式如:"value1,value2,value3" 用" , "号分隔商品数字id
        /// </summary>
        public string NumIids { get; set; }

        /// <summary>
        /// 自定义输入串.格式:英文和数字组成;长度不能大于12个字符,区分不同的推广渠道,如:bbs,表示bbs为推广渠道;blog,表示blog为推广渠道.
        /// </summary>
        public string OuterCode { get; set; }

        /// <summary>
        /// 用户的pid,必须是mm_xxxx_0_0这种格式中间的"xxxx". 注意nick和pid至少需要传递一个,如果2个都传了,将以pid为准,且pid的最大长度是20
        /// </summary>
        public Nullable<long> Pid { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.taobaoke.items.convert";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("is_mobile", this.IsMobile);
            parameters.Add("nick", this.Nick);
            parameters.Add("num_iids", this.NumIids);
            parameters.Add("outer_code", this.OuterCode);
            parameters.Add("pid", this.Pid);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateRequired("num_iids", this.NumIids);
            RequestValidator.ValidateMaxListSize("num_iids", this.NumIids, 50);
        }

        #endregion
    }
}
