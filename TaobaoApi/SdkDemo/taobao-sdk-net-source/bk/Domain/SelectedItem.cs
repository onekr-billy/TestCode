using System;
using System.Xml.Serialization;

namespace Top.Api.Domain
{
    /// <summary>
    /// SelectedItem Data Structure.
    /// </summary>
    [Serializable]
    public class SelectedItem : TopObject
    {
        /// <summary>
        /// 商品对应的后台类目id
        /// </summary>
        [XmlElement("cid")]
        public long Cid { get; set; }

        /// <summary>
        /// 商品综合得分，根据商家运营能力、商家服务能力、商品质量多方面表现综合得到的分数。得分越高越好。
        /// </summary>
        [XmlElement("item_score")]
        public string ItemScore { get; set; }

        /// <summary>
        /// 商品id
        /// </summary>
        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        /// <summary>
        /// 店铺id
        /// </summary>
        [XmlElement("shop_id")]
        public long ShopId { get; set; }
    }
}
