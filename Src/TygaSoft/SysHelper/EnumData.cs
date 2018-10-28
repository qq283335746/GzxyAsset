using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.SysHelper
{
    public class EnumData
    {
        public enum ResCode { 成功 = 1000, 失败 = 1001,确认=1002, 未登录 = 1111};

        public enum EnumIsOk { 否, 是 };

        public enum Platform : byte { PC, Android, IOS };

        public enum EnumOperationAccess { 浏览 = 1, 新增 = 2, 编辑 = 3, 删除 = 4, 导入 = 5, 导出 = 6 };

        public enum EnumValidateAccess { IsView = 1, IsAdd = 2, IsEdit = 3, IsDelete = 4, IsImport = 5, IsExport = 6 };

        public enum EnumAssetStatus { 领用=1, 已退库=2 };

        public enum EnumUnitLevel { 件=1,内包装=2,箱=3,托盘=4, 其它=5};

        public enum EnumReceiptOrderStatus { 其它,新建 };

        public enum EnumReceiptOrderType { 其它,销售退回,采购入库 };

        public enum EnumOrderPrefix { 其它, 采购, 收货,盘盈};

        public enum EnumStockLocationType { 阁楼=1, 重力货架=2, 地堆=3 };

        public enum EnumStockLocationCtrType { 其它, 常规存储, 收货暂存,  拣货至or待运 };

        public enum EnumAbc { A=1,B=2,C=3 };

        public enum EnumStockLocationDeal { 叉车 = 1, 地牛 = 2 };

        public enum EnumStockLocationUseStatus { 正常 = 1, 破损 = 2 };

        public enum EnumWhetherFix { 是 = 1, 否 = 2, 原返=3 };

        public enum EnumStepType { 收货 = 1, 发货 = 2 };

        public enum EnumPandianStatus { 未完成,完成};

        public enum EnumPandianAssetStatus { 未盘点, 已盘点, 盘盈 };

        public enum EnumPdaDataStatus { 导入=1, 删除=2, 新增=3 };

        public enum EnumMenuName { 首页,禁止匿名访问 };

    }
}
