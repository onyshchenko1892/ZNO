using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIEE.WEB.UI.Models
{
    public class Test
    {
        public int TestId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int NumberQuestions { get; set; }
        public int NumberMemberAttempt  { get; set; }
        public int Time { get; set; }
    }
}