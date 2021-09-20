using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class QnA
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public QnA()
        {

        }
    }
}
