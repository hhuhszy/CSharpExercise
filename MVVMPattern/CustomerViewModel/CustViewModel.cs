using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerModel;

namespace CustomerViewModel
{
    public class CustViewModel
    {
        private Customer obj = new Customer();

        public string TxtCustomerName { get => obj.CustomerName; set => obj.CustomerName = value; }

        public string TxtAmount { get => obj.Amount.ToString(); set => obj.Amount = int.Parse(value); }

        public string LblAmountColor
        {
            get
            {
                if (obj.Amount > 2000)
                {
                    return "Blue";
                }
                else if (obj.Amount > 1500)
                {
                    return "Red";
                }
                return "Yellow";
            }
        }

        public bool IsMarried
        {
            get
            {
                if (obj.Married == "Married")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    obj.Married = "Married";
                }
                else
                {
                    obj.Married = "NotMarried";
                }
            }
        }
    }
}
