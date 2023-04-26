using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luck.Walnut.Kube.Domain.Shared.Enums
{
    /// <summary>
    /// 在线状态枚举
    /// </summary>
    public enum OnlineStatusEnum
    {
        /// <summary>
        /// 上线
        /// </summary>
        [Description("上线")]
        Online =0,

        /// <summary>
        /// 下线
        /// </summary>
        [Description("下线")]
        Offline = 5,
    }
}
