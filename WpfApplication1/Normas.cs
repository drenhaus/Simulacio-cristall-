﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Normas
    {
        //ATRIBUTOS
        double m;
        double delta;
        double dt;
        double betta;
        double epsilon;
        double dx;
        double dy;

        // SETS
        public void SetM(double m2)
        { this.m = m2; }
        public void SetDelta(double D2)
        { this.delta = D2; }
        public void SetDT(double dt2)
        { this.dt = dt2; }
        public void SetBetta(double B)
        { this.betta = B; }
        public void SetEpsilon(double e)
        { this.epsilon = e; }
        public void SetDxDy(double d)
        {
            this.dx = d;
            this.dy = d;
        }

        //GETS
        public double GetM()
        { return m; }
        public double GetDelta()
        { return delta; }
        public double GetDT()
        { return dt; }
        public double GetBetta()
        { return betta; }
        public double GetEpsilon()
        { return epsilon; }
        public double GetDxDy()
        {
            return dx;
        }

    }
}
