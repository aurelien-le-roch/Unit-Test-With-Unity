using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace QteMiningTest
{
    
    public class qte_setup
    {
        [Test]
        public void valid_TotalTime_value_after_setup()
        {
            var qteMining = new QTEMining();
            
            qteMining.SetupQTE(1,0.5f,0.2f);
            
            Assert.AreEqual(1,qteMining.TotalTime);
        }

        [TestCase(10,8,1,9)]
        [TestCase(10,6,2,8)]
        [TestCase(10,4,3,7)]
        public void valid_MediumRange_after_setup(float totalTime,float mediumTime,float minRangeResult,float maxRangeResult)
        {
            var qteMining = new QTEMining();
            
            qteMining.SetupQTE(totalTime,mediumTime,2f);
            Assert.AreEqual(minRangeResult,qteMining.MediumRange.Min);
            Assert.AreEqual(maxRangeResult,qteMining.MediumRange.Max);
        }

        [TestCase(10,8,1,9)]
        [TestCase(10,6,2,8)]
        [TestCase(10,4,3,7)]
        public void valid_PerfectRange_after_setup(float totalTime,float perfectTime,float minRangeResult,float maxRangeResult)
        {
            var qteMining = new QTEMining();
            
            qteMining.SetupQTE(totalTime,9,perfectTime);
            Assert.AreEqual(minRangeResult,qteMining.PerfectRange.Min);
            Assert.AreEqual(maxRangeResult,qteMining.PerfectRange.Max);
        }
        
        [Test]
        public void set_IsSetup_flag_to_true_after_setup()
        {
            var qteMining = new QTEMining();
            Assert.IsFalse(qteMining.IsSetup);
            qteMining.SetupQTE(10,8,2);
            Assert.IsTrue(qteMining.IsSetup);
        }
    }
}
