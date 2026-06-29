using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double price;
        public double Price
        {
            get { return price; }
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                price = value;
                Notify(nameof(Price));
                TotalCount();
            }
        }

        private int nightsCount;
        public int NightsCount
        {
            get { return nightsCount; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException();
                nightsCount = value;
                TotalCount();
                Notify(nameof(NightsCount));
            }
        }

        private double discount;
        public double Discount
        {
            get { return discount; }
            set
            {
                if (value > 100)
                    throw new ArgumentException();
                discount = value;
                TotalCount();
                Notify(nameof(Discount));
            }
        }

        private double total;
        public double Total
        {
            get { return price * nightsCount * (1 - discount / 100); }
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                total = value;
                if (price * nightsCount != 0)
                    discount = (1 - total / (price * nightsCount)) * 100;
                else discount = 0;
                Notify(nameof(Discount));
                Notify(nameof(Total));
            }
        }

        private void TotalCount()
        {
            total = price * nightsCount * (1 - discount / 100);
            Notify(nameof(Total));
        }
    }
}