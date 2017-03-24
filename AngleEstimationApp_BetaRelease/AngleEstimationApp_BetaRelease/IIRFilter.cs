using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AngleEstimationApp_BetaRelease
{
    class IIRFilter
    {
        private List<double> a;
        private List<double> b;

        //default Filter
        public IIRFilter()
        {

            b = new List<double>();
            b.Add(0.002899695497431);
            b.Add(-0.006626465760968);
            b.Add(0.004033620976099);
            b.Add(0.004033620976099);
            b.Add(-0.006626465760968);
            b.Add(0.002899695497431);

            a = new List<double>();
            a.Add(1.000000000000000);
            a.Add(-4.229081817661462);
            a.Add(7.205853343227314);
            a.Add(-6.177477993982333);
            a.Add(2.662714482809827);
            a.Add(-0.461394312968222);

        }

        public IIRFilter(List<double> a, List<double> b)
        {
            this.a = a;
            this.b = b;
        }

        public void Applyfilter(List<double> x, out List<double> y)
        {
            int ord = a.Count - 1;
            int np = x.Count - 1;

            if (np < ord)
            {
                for (int k = 0; k < ord - np; k++)
                    x.Add(0.0);
                np = ord;
            }

            y = new List<double>();
            for (int k = 0; k < np + 1; k++)
            {
                y.Add(0.0);
            }
            int i, j;
            y[0] = b[0] * x[0];
            for (i = 1; i < ord + 1; i++)
            {
                y[i] = 0.0;
                for (j = 0; j < i + 1; j++)
                    y[i] = y[i] + b[j] * x[i - j];
                for (j = 0; j < i; j++)
                    y[i] = y[i] - a[j + 1] * y[i - j - 1];
            }
            /* end of initial part */
            for (i = ord + 1; i < np + 1; i++)
            {
                y[i] = 0.0;
                for (j = 0; j < ord + 1; j++)
                    y[i] = y[i] + b[j] * x[i - j];
                for (j = 0; j < ord; j++)
                    y[i] = y[i] - a[j + 1] * y[i - j - 1];
            }
        }
    }
}
