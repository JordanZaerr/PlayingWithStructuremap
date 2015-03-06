using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using StructureMapExample.Reporting;
using StructureMapExample.Reporting.Reports;

namespace StructureMapExample.Tests
{
    [TestClass]
    public class DebugTests
    {
        private IContainer _container;

        [TestInitialize]
        public void Setup()
        {
            _container = new Container(new StructureMapRegistry());
        }

        [TestMethod]
        public void ReportModuleBuildPlan_1_Simple()
        {
            var buildPlan = _container.Model.For<IReportModule>().Default.DescribeBuildPlan();
            //Inspect 'buildPlan'
        }

        [TestMethod]
        public void ReportModuleBuildPlan_2_Detailed()
        {
            var buildPlan = _container.Model.For<IReportModule>().Default.DescribeBuildPlan(100);
            //Inspect 'buildPlan'
        }

        [TestMethod]
        public void ReportInstanceBuildPlan()
        {
//            This fails since there isn't a default instance for IReport
//            var buildPlan = _container.Model.For<IReport>().Default.DescribeBuildPlan(100);

            var buildPlan = String.Join(Environment.NewLine, _container.Model.For<IReport>()
                .Instances.Select(x => x.DescribeBuildPlan(100)));

            //Inspect 'buildPlan'
        }

        [TestMethod]
        public void WhatDoIHave()
        {
            var data = _container.WhatDoIHave();

            //Inspect 'data'
        }
    }
}