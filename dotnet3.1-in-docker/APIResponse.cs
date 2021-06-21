using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace dotnet3._1_in_docker
{
    public class APIResponse
    {
        public string status { get; set; }
    }

    public class APIResponse_with_reason : APIResponse
    {
        public string reason { get; set; }
    }

    public class APIResponse_with_comms : APIResponse
    {
        public string communication { get; set; }
    }
}
