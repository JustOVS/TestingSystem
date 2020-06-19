﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestingSystem.Data.DTO.DTOsForStatistics;

namespace TestingSystem.Business.Statistics
{
    public class TestStatistics
    {
        InfoForStatisticsDTO info;
        
        public TestStatistics(InfoForStatisticsDTO info)
        {
            this.info = info;
        }

        public List<int> AllResults(int id)
        {
            Dictionary<int, int> results = new Dictionary<int, int>();

            foreach (var record in info.IdInfo)
            {
                if(record.TestId == id)
                {
                    if(!results.ContainsKey(record.AttemptId))
                    {
                        int attemptId = record.AttemptId;
                        int result = info.Attempts[attemptId].UserResult;
                        results.Add(attemptId, result);
                    }
                }
            }

            return results.Keys.ToList();
        }

        public double AverageResult(int id)
        {
            List<int> results = AllResults(id);
            if (results.Count == 0)
            {
                return Double.NaN;
            }

            double sum = 0;
            foreach(int i in results)
            {
                sum += i;
            }
            double avg = sum / results.Count();
            return avg;
        }
    }
}