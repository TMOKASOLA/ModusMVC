using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Church.Common
{
    public class Validations
    {
      
        public static bool contactValidation(String cont)
        {
            bool temp = false;
            string cellNum = cont.Substring(0, 3);

            if (cont.Length == 10)
            {
                if  (cellNum == "082" || cellNum == "072"
                   || cellNum == "076" || cellNum == "079"
                   || cellNum == "071" || cellNum == "060"
                   || cellNum == "083" || cellNum == "073"
                   || cellNum == "078" || cellNum == "061"
                   || cellNum == "084" || cellNum == "074"
                   || cellNum == "081" || cellNum == "063"
                   || cellNum == "060" || cellNum == "062"
                   || cellNum == "064")
                {
                    temp = true;
                }
                else
                {
                    temp = false;
                }
            }
            return temp;
        }
}
      