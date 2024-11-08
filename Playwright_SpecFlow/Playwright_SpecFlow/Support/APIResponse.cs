using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Playwright_SpecFlow.Support
{
    public class APIResponse
    {
       /* APIRequest
            https://opentdb.com/api.php?amount=10&category=9&difficulty=easy&type=boolean
            https://opentdb.com/api.php?amount=50&category=21&difficulty=medium&type=multiple
       */
        public int response_code { get; set; }
        public List<Question> Results { get; set; }

    }

    public class Question
    {
        public string type { get; set; }
        public string difficulty { get; set; }
        public string category { get; set; }
        public string question { get; set; }
        public string correct_answer { get; set; }
        public string[] incorrect_answers { get; set; }
    }
}
