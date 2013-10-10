using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fanex.CustomerSetting.OpusCasino.ViewModels
{
    public class PositionTaking
    {
        public double S4 { get; set; }
        public double M4 { get; set; }
        public double A4 { get; set; }
        public double N4 { get; set; }
        public double L4 { get; set; }
        public double Comm4Max { get; set; }
        public double Comm4 { get; set; }

        public double S3 { get; set; }
        public double M3 { get; set; }
        public double US3 { get; set; }
        public bool IS3 { get; set; }
        public double N3 { get; set; }
        public double L3 { get; set; }
        public double Comm3 { get; set; }

        public double M2 { get; set; }
        public double A2 { get; set; }
        public double UM2 { get; set; }
        public bool IM2 { get; set; }
        public double N2 { get; set; }
        public double L2 { get; set; }
        public double Comm2 { get; set; }

        public double S1 { get; set; }
        public double M1 { get; set; }
        public double A1 { get; set; }
        public double US1 { get; set; }
        public bool IS1 { get; set; }
        public double UM1 { get; set; }
        public bool IM1 { get; set; }

        public PositionTaking()
        {
            this.S4 = 0.8;
            this.M4 = 0.6;
            this.A4 = 0.5;
            this.N4 = 0.3;
            this.L4 = 0.85;
            this.Comm4Max = 0.01;

            this.S3 = 0.2;
            this.M3 = 0.65;
            this.US3 = 0;
            this.IS3 = false;
            this.N3 = 0.8;
            this.L3 = 0.8;

            this.M2 = 0.25;
            this.A2 = 0.3;
            this.UM2 = 0;
            this.IM2 = false;
            this.N2 = 0.1;
            this.L2 = 0.65;

            this.S1 = 0.3;
            this.M1 = 0.25;
            this.A1 = 0.1;
            this.US1 = 0.6;
            this.IS1 = false;
            this.UM1 = 0.6;
            this.IM1 = false;
        }

    }
}