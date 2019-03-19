using System;
using Amazon.EC2;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerlessApp.DAL
{
    public class clsCreateEC2:IDisposable
    {
        public string CreateInstance()
        {

            AmazonEC2Client amazonEC2Client = new AmazonEC2Client();
            return "Success";

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
