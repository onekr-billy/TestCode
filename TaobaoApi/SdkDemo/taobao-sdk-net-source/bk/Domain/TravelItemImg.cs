using System;
using System.Xml.Serialization;

namespace Top.Api.Domain
{
    /// <summary>
    /// TravelItemImg Data Structure.
    /// </summary>
    [Serializable]
    public class TravelItemImg : TopObject
    {
        /// <summary>
        /// 图片创建时间 时间格式：yyyy-MM-dd HH:mm:ss。
        /// </summary>
        [XmlElement("created")]
        public string Created { get; set; }

        /// <summary>
        /// 商品图片的ID。
        /// </summary>
        [XmlElement("id")]
        public long Id { get; set; }

        /// <summary>
        /// 图片放在第几张（多图时可设置）
        /// </summary>
        [XmlElement("position")]
        public long Position { get; set; }

        /// <summary>
        /// 图片链接地址。
        /// </summary>
        [XmlElement("url")]
        public string Url { get; set; }
    }
}
