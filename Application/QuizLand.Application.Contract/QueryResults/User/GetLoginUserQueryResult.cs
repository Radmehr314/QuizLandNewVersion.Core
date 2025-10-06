using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLand.Application.Contract.QueryResults.User
{
    public class GetLoginUserQueryResult
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
    }
}
