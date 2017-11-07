
namespace CTP_STrader.Base
{
    public interface IOrderMgr
    {
        bool OrderInsert(CustomOrder order, out string errMsg);
        bool OrderCancel(CustomOrder order, out string errMsg);
    }
}
