using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GROBS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 是或否
        /// </summary>
        public static Dictionary<int, string> YesOrNo;
        /// <summary>
        /// 性别
        /// </summary>
        public static Dictionary<int, string> Sex;
        /// <summary>
        /// 教育程度
        /// </summary>
        public static Dictionary<int, string> Education;
        /// <summary>
        /// 医疗器械管理类别
        /// </summary>
        public static Dictionary<int, string> ManageType;
        /// <summary>
        /// 首营状态
        /// </summary>
        public static Dictionary<int, string> ShouYingZhuangTai;
        /// <summary>
        /// 首营种类
        /// </summary>
        public static Dictionary<int, string> ShouYingType;
        /// <summary>
        /// 储运要求
        /// </summary>
        public static Dictionary<int, string> TranCondition;
        /// <summary>
        /// 入库类型
        /// </summary>
        public static Dictionary<int, string> EntryType;
        /// <summary>
        /// 出库类型
        /// </summary>
        public static Dictionary<int, string> OutgoingType;
        /// <summary>
        /// 入库计划状态
        /// </summary>
        public static Dictionary<int, string> EntryPlanState;
        /// <summary>
        /// 出库计划状态
        /// </summary>
        public static Dictionary<int, string> OutPlanState;
        /// <summary>
        /// 入库状态
        /// </summary>
        public static Dictionary<int, string> EntryState;
        /// <summary>
        /// 出库状态
        /// </summary>
        public static Dictionary<int, string> OutState;
        /// <summary>
        /// 验收状态
        /// </summary>
        public static Dictionary<int, string> CheckState;
        /// <summary>
        /// 验收结果
        /// </summary>
        public static Dictionary<int, string> CheckResult;
        /// <summary>
        /// 验收不符合项说明
        /// </summary>
        public static Dictionary<int, string> CheckMemo;
        /// <summary>
        /// 复核不符合项说明
        /// </summary>
        public static Dictionary<int, string> CheckMemo1;
        /// <summary>
        /// 验收标准
        /// </summary>
        public static Dictionary<int, string> CheckStandard;
        /// <summary>
        /// 存货状态
        /// </summary>
        public static Dictionary<int, string> CargoState;
        /// <summary>
        /// 仓库区域类型
        /// </summary>
        public static Dictionary<int, string> AreaType;
        /// <summary>
        /// 仓库区域功能类型
        /// </summary>
        public static Dictionary<int, string> AreaFunType;
        /// <summary>
        /// 快递公司
        /// </summary>
        public static Dictionary<int, string> ExpressCompany;
        /// <summary>
        /// 运输公司类型
        /// </summary>
        public static Dictionary<int, string> TransferType;
        /// <summary>
        /// 结算方式
        /// </summary>
        public static Dictionary<int, string> SettlingType;
        /// <summary>
        ///运送方式 
        /// </summary>
        public static Dictionary<int, string> DeliveryType;
        /// <summary>
        /// 提醒区间
        /// </summary>
        public static Dictionary<int, string> RemindPeriod;
        /// <summary>
        /// 提醒对象
        /// </summary>
        public static Dictionary<int, string> RemindObject;
        /// <summary>
        /// 纸箱规格
        /// </summary>
        public static Dictionary<int, string> BoxType;
        /// <summary>
        /// 包装箱状态
        /// </summary>
        public static Dictionary<int, string> BoxState;
        /// <summary>
        /// 移位类型
        /// </summary>
        public static Dictionary<int, string> MoveType;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //davis define
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
            InitCommon();
        }

        private void InitCommon()
        {

            //是或否
            YesOrNo = new Dictionary<int, string>();
            YesOrNo.Add(-1, "");
            YesOrNo.Add(1, "是");
            YesOrNo.Add(2, "否");
            //性别
            Sex = new Dictionary<int, string>();
            Sex.Add(0, "");
            Sex.Add(1, "男");
            Sex.Add(2, "女");
            Sex.Add(3, "无");
            //教育程度
            Education = new Dictionary<int, string>();
            Education.Add(0, "");
            Education.Add(1, "小学");
            Education.Add(2, "初中");
            Education.Add(3, "高中");
            Education.Add(4, "大学");
            Education.Add(5, "硕士");
            Education.Add(6, "博士");

            //医疗器械管理类别
            ManageType = new Dictionary<int, string>();
            ManageType.Add(0, "");
            ManageType.Add(1, "Ⅰ");
            ManageType.Add(2, "Ⅱ");
            ManageType.Add(3, "Ⅲ");

            //首营状态
            ShouYingZhuangTai = new Dictionary<int, string>();
            ShouYingZhuangTai.Add(0, "");
            ShouYingZhuangTai.Add(1, "新建");
            ShouYingZhuangTai.Add(2, "审核中");
            ShouYingZhuangTai.Add(3, "质量部通过");
            ShouYingZhuangTai.Add(4, "负责人通过");
            ShouYingZhuangTai.Add(5, "已通过");
            ShouYingZhuangTai.Add(-1, "未通过");

            //储运要求
            TranCondition = new Dictionary<int, string>();
            TranCondition.Add(0, "");
            TranCondition.Add(1, "常温");
            TranCondition.Add(2, "阴凉");
            TranCondition.Add(3, "冷藏");
            TranCondition.Add(4, "冷冻");

            AreaType = new Dictionary<int, string>();
            AreaType.Add(0, "");
            AreaType.Add(1, "常温");
            AreaType.Add(2, "阴凉");
            AreaType.Add(3, "冷藏");
            AreaType.Add(4, "冷冻");

            AreaFunType = new Dictionary<int, string>();
            AreaFunType.Add(0, "");
            AreaFunType.Add(1, "正常");
            AreaFunType.Add(2, "特殊");
            AreaFunType.Add(3, "不良");

            EntryType = new Dictionary<int, string>();
            EntryType.Add(0, "");
            EntryType.Add(1, "进货");
            EntryType.Add(2, "移库");
            EntryType.Add(3, "销退");
            EntryType.Add(4, "赠品");
            EntryType.Add(5, "换货");
            EntryType.Add(6, "其它");

            EntryPlanState = new Dictionary<int, string>();
            EntryPlanState.Add(0, "");
            EntryPlanState.Add(1, "新建");
            EntryPlanState.Add(2, "执行中");
            EntryPlanState.Add(3, "部分入库");
            EntryPlanState.Add(4, "入库完成");
            EntryPlanState.Add(5, "历史入库");
            EntryPlanState.Add(6, "人工关闭");

            OutgoingType = new Dictionary<int, string>();
            OutgoingType.Add(0, "");
            OutgoingType.Add(1, "销售");
            OutgoingType.Add(2, "移库");
            OutgoingType.Add(3, "采退");
            OutgoingType.Add(4, "赠品");
            OutgoingType.Add(5, "换货");
            OutgoingType.Add(6, "其它");

            OutPlanState = new Dictionary<int, string>();
            OutPlanState.Add(0, "");
            OutPlanState.Add(1, "新建");
            OutPlanState.Add(2, "执行中");
            OutPlanState.Add(3, "部分出库");
            OutPlanState.Add(4, "出库完成");
            OutPlanState.Add(5, "历史出库");
            OutPlanState.Add(6, "人工关闭");

            CheckState = new Dictionary<int, string>();
            CheckState.Add(0, "");
            CheckState.Add(1, "未检验");//没有检验
            CheckState.Add(2, "待检验");//正在检验
            CheckState.Add(3, "已检验");//完成检验

            CheckResult = new Dictionary<int, string>();
            CheckResult.Add(0, "");
            CheckResult.Add(1, "合格");
            CheckResult.Add(2, "部分合格");
            CheckResult.Add(3, "不合格");

            CheckMemo = new Dictionary<int, string>();
            CheckMemo.Add(0, "");
            CheckMemo.Add(1, "未见异常，检查验收合格");
            CheckMemo.Add(2, "近效期，包装外观未见异常");
            CheckMemo.Add(3, "退货经检查验收，包装外观符合要求，可入库");
            CheckMemo.Add(4, "包装损坏，货品经检验后合格，可入库");
            CheckMemo.Add(5, "包装破损，不合格");
            CheckMemo.Add(6, "货品经检验后判定不合格");

            CheckMemo1 = new Dictionary<int, string>();
            CheckMemo1.Add(0, "");
            CheckMemo1.Add(1, "未见异常，检查合格");
            CheckMemo1.Add(2, "近效期，包装外观未见异常");
            CheckMemo1.Add(3, "不合格：包装出现破损、污染、封口不牢、封条损坏等问题");
            CheckMemo1.Add(4, "不合格：标签脱落、字迹模糊不清或者标示内容与实物不符");
            CheckMemo1.Add(5, "不合格：医疗器械超过有效期");
            CheckMemo1.Add(6, "不合格：存在其他异常情况的医疗器械");

            CargoState = new Dictionary<int, string>();
            CargoState.Add(0, "");
            CargoState.Add(1, "正常");
            CargoState.Add(2, "破损");
            CargoState.Add(3, "污染");
            CargoState.Add(4, "渗漏");
            CargoState.Add(5, "其它");

            ShouYingType = new Dictionary<int, string>();
            ShouYingType.Add(0, "");
            ShouYingType.Add(1, "商品");
            ShouYingType.Add(2, "委托方");
            ShouYingType.Add(3, "供应商");
            ShouYingType.Add(4, "收货单位");
            ShouYingType.Add(5, "生产企业");
            ShouYingType.Add(6, "销售");
            ShouYingType.Add(7, "发货方");
            ShouYingType.Add(8, "运输单位");

            CheckStandard = new Dictionary<int, string>();
            CheckStandard.Add(0, "");
            CheckStandard.Add(1, "包装质量");
            CheckStandard.Add(2, "外观质量");
            CheckStandard.Add(3, "检验报告");
            CheckStandard.Add(4, "检验证标志");
            CheckStandard.Add(5, "合格证标志");
            CheckStandard.Add(6, "注册证");
            CheckStandard.Add(7, "存储质量");
            CheckStandard.Add(8, "运输温度");

            EntryState = new Dictionary<int, string>();
            EntryState.Add(0, "");
            EntryState.Add(1, "新建");
            EntryState.Add(2, "收货");
            EntryState.Add(3, "验收");
            EntryState.Add(4, "上架");
            EntryState.Add(5, "完成");
            EntryState.Add(6, "错误");
            EntryState.Add(7, "上传");

            OutState = new Dictionary<int, string>();
            OutState.Add(-12, "扫码");
            OutState.Add(0, "");
            OutState.Add(1, "新建");
            OutState.Add(2, "拣货");
            OutState.Add(3, "复核");
            OutState.Add(4, "装箱");
            OutState.Add(5, "完成");
            OutState.Add(6, "错误");
            OutState.Add(7, "上传");

            ExpressCompany = new Dictionary<int, string>();
            ExpressCompany.Add(0, "");
            ExpressCompany.Add(1, "顺丰");
            ExpressCompany.Add(2, "德邦");
            ExpressCompany.Add(3, "申通");
            ExpressCompany.Add(4, "圆通");
            ExpressCompany.Add(5, "韵达");
            ExpressCompany.Add(6, "中通");
            ExpressCompany.Add(7, "天天");
            ExpressCompany.Add(8, "联邦");
            ExpressCompany.Add(9, "宅急送");
            ExpressCompany.Add(10, "邮政");
            ExpressCompany.Add(11, "TNT");
            ExpressCompany.Add(12, "菜鸟");
            ExpressCompany.Add(13, "京东");
            ExpressCompany.Add(14, "DHL");
            ExpressCompany.Add(15, "UPS");

            TransferType = new Dictionary<int, string>();
            TransferType.Add(0, "");
            TransferType.Add(1, "快递");
            TransferType.Add(2, "车队");
            TransferType.Add(3, "货代");
            TransferType.Add(4, "报关");

            SettlingType = new Dictionary<int, string>();
            SettlingType.Add(0, "");
            SettlingType.Add(1, "月结");
            SettlingType.Add(2, "到付");
            SettlingType.Add(3, "包邮");
            SettlingType.Add(4, "30天");
            SettlingType.Add(5, "60天");
            SettlingType.Add(6, "90天");

            DeliveryType = new Dictionary<int, string>();
            DeliveryType.Add(0, "");
            DeliveryType.Add(1, "快递");
            DeliveryType.Add(2, "航空");
            DeliveryType.Add(3, "公路");
            DeliveryType.Add(4, "次晨");
            DeliveryType.Add(5, "铁路");
            DeliveryType.Add(6, "自提");
            DeliveryType.Add(7, "海运");

            RemindPeriod = new Dictionary<int, string>();
            RemindPeriod.Add(0, "");
            RemindPeriod.Add(-1, "超出有效期");
            RemindPeriod.Add(30, "近效期30天");
            RemindPeriod.Add(60, "近效期60天");
            RemindPeriod.Add(90, "近效期90天");
            RemindPeriod.Add(120, "近效期120天");
            RemindPeriod.Add(150, "近效期150天");
            RemindPeriod.Add(180, "近效期180天");
            RemindPeriod.Add(360, "近效期360天");

            BoxType = new Dictionary<int, string>();
            BoxType.Add(0, "");
            BoxType.Add(1, "一号箱");
            BoxType.Add(2, "二号箱");
            BoxType.Add(3, "三号箱");
            BoxType.Add(4, "四号箱");
            BoxType.Add(5, "五号箱");
            BoxType.Add(6, "六号箱");
            BoxType.Add(7, "七号箱");
            BoxType.Add(8, "八号箱");
            BoxType.Add(9, "九号箱");
            BoxType.Add(10, "十号箱");
            BoxType.Add(11, "十一号箱");
            BoxType.Add(12, "十二号箱");
            BoxType.Add(13, "十三号箱");
            BoxType.Add(14, "十四号箱");
            BoxType.Add(15, "十五号箱");

            BoxState = new Dictionary<int, string>();
            BoxState.Add(0, "");
            BoxState.Add(1, "新建");
            BoxState.Add(2, "启用");
            BoxState.Add(3, "装箱");
            BoxState.Add(4, "封箱");
            BoxState.Add(5, "出库");
            BoxState.Add(6, "损坏");

            RemindObject = new Dictionary<int, string>();
            RemindObject.Add(0, "");
            RemindObject.Add(1, "库存商品");
            RemindObject.Add(2, "注册证");
            RemindObject.Add(3, "货主授权");
            RemindObject.Add(4, "货主营业执照");
            RemindObject.Add(5, "货主经营许可");
            //RemindObject.Add(6, "供应商营业执照");
            //RemindObject.Add(7, "供应商经营许可");
            //RemindObject.Add(8, "客户营业执照");
            //RemindObject.Add(9, "客户经营许可");
            //RemindObject.Add(10, "工厂营业执照");
            //RemindObject.Add(11, "工厂生产许可");
            RemindObject.Add(12, "当前入库单");
            RemindObject.Add(13, "当前出库单");
            RemindObject.Add(14, "U8-无商品");

            MoveType = new Dictionary<int, string>();
            MoveType.Add(0, "");
            MoveType.Add(1, "普通移位");
            MoveType.Add(2, "特殊移位");
            MoveType.Add(3, "不良移位");
            MoveType.Add(4, "回复移动");
            MoveType.Add(5, "库位移动");
        }
    }
}
