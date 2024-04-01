using System.Transactions;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;

namespace Core.Aspects.Autofac.Transaction;

/// <summary>
/// TransactionScopeAspect
/// </summary>
public class TransactionScopeAspect : MethodInterception
{
    //public override void Intercept(IInvocation invocation)
    //{
    //    using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
    //    try
    //    {
    //        invocation.Proceed();
    //        transactionScope.Complete();
    //    }
    //    catch (Exception)
    //    {
    //        //ex.ToString();
    //        transactionScope.Dispose();
    //        throw;
    //    }
    //}

    public override void Intercept(IInvocation invocation)
    {
        using (TransactionScope transactionScope = new TransactionScope())
        {
            try
            {
                invocation.Proceed();
                transactionScope.Complete();
            }
            catch (Exception e)
            {
                transactionScope.Dispose();
                throw;
            }
        }
    }

}
