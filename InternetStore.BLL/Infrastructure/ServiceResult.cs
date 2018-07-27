using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.BLL.Infrastructure
{
    public class ServiceResult<T>
    {
        public T Data { get; internal set; }
        public ValidationException Exception { get; internal set; }

        public ServiceResult(T Data, string ExProp, string ExMess)
        {
            this.Data = Data;
            Exception = new ValidationException(ExMess, ExProp);
        }
        public ServiceResult(T Data, ValidationException ex)
        {
            this.Data = Data;
            Exception = ex;
        }
    }
}
